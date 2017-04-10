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
                MessageBox.Show(id.ToString());
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
            cbLogement.Items.Add(new cbItem("",-1));
            for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count;i++ )
            {
                cbLogement.Items.Add(new cbItem(Controleur.Vmodele.DT[8].Rows[i][1].ToString(), Controleur.Vmodele.DT[8].Rows[i][0].ToString()));
            }
            cbSexe.Items.Add("M");
            cbSexe.Items.Add("F");
        }
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
