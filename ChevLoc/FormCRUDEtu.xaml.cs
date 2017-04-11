using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;

namespace ChevLoc
{
    /// <summary>
    /// Logique d'interaction pour FormCRUDEtu.xaml
    /// </summary>
    public partial class FormCRUDEtu : Window
    {
        private int id;
        private EnumAction actionForm;
        private CRUD Parent;
        public FormCRUDEtu(CRUD Parent,int id = -1)
        {
            Controleur.Vmodele.charger_donnees("logements");
            Controleur.Vmodele.charger_donnees("personnes");
            InitializeComponent();
            this.Parent = Parent;
            ChargerListeDerou();
            if (id == -1)
            {
                actionForm = EnumAction.Ajout;
            }
            else          
            {
                actionForm = EnumAction.Modification;
                this.id = id;
                ChargerInfoEtu();
            }
            this.Show();
        }
        public void ChargerInfoEtu()
        {
            tbMail.Text = Controleur.Vmodele.DT[14].Rows.Find(id)[8].ToString();
            tbNom.Text = Controleur.Vmodele.DT[14].Rows.Find(id)[1].ToString();
            tbPrenom.Text = Controleur.Vmodele.DT[14].Rows.Find(id)[2].ToString();
            tbTel.Text = Controleur.Vmodele.DT[14].Rows.Find(id)[7].ToString();
            dpDateNaiss.SelectedDate = (System.DateTime)Controleur.Vmodele.DT[14].Rows.Find(id)[9];
            if (Controleur.Vmodele.DT[14].Rows.Find(id)[3].ToString() == "M")
            {
                cbSexe.SelectedIndex = 0;
            }
            else
            {
                cbSexe.SelectedIndex = 1;
            }
            if (Controleur.Vmodele.DT[14].Rows.Find(id)[6].ToString()!="")
            {
                for (int i = 0; i < cbLogement.Items.Count - 1; i++)
                {
                    if ((cbLogement.Items[i] as cbItem).GetValueAsInt() == Convert.ToInt16(Controleur.Vmodele.DT[14].Rows.Find(id)[6]))
                    {
                        cbLogement.SelectedIndex = i;
                    }
                }
            }
            else
            {
                cbLogement.SelectedIndex = 0;
            }
        }
        public void ChargerListeDerou()
        {
            cbLogement.Items.Add(new cbItem("",DBNull.Value));
            cbLogement.SelectedIndex = 0;
            for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count;i++ )
            {
                cbLogement.Items.Add(new cbItem(Controleur.Vmodele.DT[8].Rows[i][1].ToString(), Controleur.Vmodele.DT[8].Rows[i][0].ToString()));
            }
            cbSexe.Items.Add("M");
            cbSexe.Items.Add("F");
        }
        private string GenerateLogin(string prenom, string nom, int length = 1)
        {
            string login = (prenom.Substring(0,length) + nom).ToLower();
            for(int i =0; i<Controleur.Vmodele.DT[19].Rows.Count;i++)
            {
                if (login == Controleur.Vmodele.DT[19].Rows[i][0].ToString())
                {
                    length += 1;
                    return GenerateLogin(prenom, nom, length);
                }
            }
            return login.ToLower();
        }
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            string msgFinal = "";
            if (!ControleSaisie.Tel(tbTel.Text, ref msg) || !ControleSaisie.Mail(tbMail.Text, ref msg) || cbSexe.Text == "" || dpDateNaiss.Text == "" || tbNom.Text=="" || tbPrenom.Text=="")
            {
                msgFinal += "Formulaire non conforme : \n\n";
                if (tbNom.Text == "")
                    msgFinal += "- Le nom doit être renseigné \n";
                if (tbPrenom.Text == "")
                    msgFinal += "- Le prénom doit être renseigné \n";
                if(!ControleSaisie.Tel(tbTel.Text, ref msg))
                    msgFinal += "- "+msg +"\n";
                if(!ControleSaisie.Mail(tbMail.Text, ref msg))
                    msgFinal += "- "+msg +"\n";
                if (cbSexe.Text == "")
                    msgFinal += "- Le genre doit être renseigné \n";
                if (dpDateNaiss.Text == "")
                    msgFinal += "- La date de naissance doit être renseignée \n";
                MessageBox.Show(msgFinal);
                return;
            } 
            if (actionForm == EnumAction.Modification)
            {
                Controleur.Vmodele.DT[10].Rows.Find(id)[1] = tbNom.Text.ToLower();
                Controleur.Vmodele.DT[10].Rows.Find(id)[2] = tbPrenom.Text.ToLower();
                Controleur.Vmodele.DT[10].Rows.Find(id)[3] = cbSexe.SelectedValue;
                Controleur.Vmodele.DA[10].Update(Controleur.Vmodele.DT[10]);
                Controleur.Vmodele.DT[17].Rows.Find(id)[3] = (cbLogement.SelectedItem as cbItem).Value;
                Controleur.Vmodele.DT[17].Rows.Find(id)[4] = tbTel.Text;
                Controleur.Vmodele.DT[17].Rows.Find(id)[5] = tbMail.Text;
                Controleur.Vmodele.DT[17].Rows.Find(id)[7] = dpDateNaiss.SelectedDate;
                Controleur.Vmodele.DA[17].Update(Controleur.Vmodele.DT[17]);
            }
            else
            {
                DataRow NewRowPers = Controleur.Vmodele.DT[10].NewRow();
                NewRowPers[0] = Controleur.Vmodele.ReturnLastIdPersonne() + 1;
                NewRowPers[1] = tbNom.Text.ToLower();
                NewRowPers[2] = tbPrenom.Text.ToLower();
                NewRowPers[3] = cbSexe.SelectedValue;
                Controleur.Vmodele.DT[10].Rows.Add(NewRowPers) ;
                Controleur.Vmodele.DA[10].Update(Controleur.Vmodele.DT[10]);
                Controleur.Vmodele.charger_donnees("personnes");
                DataRow NewRowEtu = Controleur.Vmodele.DT[17].NewRow();
                NewRowEtu[0] = Controleur.Vmodele.ReturnLastIdPersonne().ToString();
                NewRowEtu[1] = GenerateLogin(tbPrenom.Text, tbNom.Text);
                NewRowEtu[2] = Parent.GenerateMdp();
                NewRowEtu[3] = (cbLogement.SelectedItem as cbItem).Value;
                NewRowEtu[4] = tbTel.Text;
                NewRowEtu[5] = tbMail.Text;
                NewRowEtu[7] = dpDateNaiss.SelectedDate;
                Controleur.Vmodele.DT[17].Rows.Add(NewRowEtu);
                Controleur.Vmodele.DA[17].Update(Controleur.Vmodele.DT[17]);
            }
            Parent.ActualiserForm();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
