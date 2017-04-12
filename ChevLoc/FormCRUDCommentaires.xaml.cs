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
    /// Logique d'interaction pour FormCRUDCommentaires.xaml
    /// </summary>
    public partial class FormCRUDCommentaires : Window
    {
        private int id;
        private EnumAction actionForm;
        private CRUD Parent;
        public FormCRUDCommentaires(CRUD Parent, int id = -1)
        {
            InitializeComponent();
            this.Parent = Parent;
            Controleur.Vmodele.charger_donnees("etudiants");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[14].Rows.Count; i++)
                {
                    cb_Etudiants.Items.Add(new cbItem(Controleur.Vmodele.DT[14].Rows[i][4].ToString(), Controleur.Vmodele.DT[14].Rows[i][0].ToString()));
                }
            }
            Controleur.Vmodele.charger_donnees("logements");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count; i++)
                {
                    cb_Logements.Items.Add(new cbItem(Controleur.Vmodele.DT[8].Rows[i][1].ToString(), Controleur.Vmodele.DT[8].Rows[i][0].ToString()));
                }
            }
            if (id==-1)
            {
                actionForm = EnumAction.Ajout;
            }
            else
            {
                actionForm = EnumAction.Modification;
                this.id = id;
                    for (int i = 0; i < Controleur.Vmodele.DT[14].Rows.Count; i++)
                    {
                        if (Controleur.Vmodele.DT[14].Rows[i][0].ToString() == Controleur.Vmodele.DT[3].Rows[id][1].ToString())
                        {
                            cb_Etudiants.SelectedIndex = i;
                        }
                    }
                    for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count; i++)
                    {
                        if (Controleur.Vmodele.DT[8].Rows[i][0].ToString() == Controleur.Vmodele.DT[3].Rows[id][0].ToString())
                        {
                            cb_Logements.SelectedIndex = i;
                        }
                    }
                dp_Date.Text = Controleur.Vmodele.DT[3].Rows[id][2].ToString();
                tb_Note.Text = (Controleur.Vmodele.DT[3].Rows[id][3].ToString());
                
            }
            this.Show();
        }

        private void btn_valider_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            string msgFinal = "Formulaire non conforme : \n\n";
            if(!ControleSaisie.Entier(tb_Note.Text,0,5,ref msg)||dp_Date.Text==""||tb_Note.Text==""||cb_Etudiants.SelectedIndex==-1||cb_Logements.SelectedIndex==-1)
            {
                if (tb_Note.Text == "")
                {
                    msgFinal += "- Vous devez saisir une note \n";
                }
                else if (!ControleSaisie.Entier(tb_Note.Text, 0, 5, ref msg))
                {
                    msgFinal += "- " + msg + "\n";
                }
                if(dp_Date.Text=="")
                    msgFinal += "- Vous devez saisir une date \n";
                if (cb_Etudiants.SelectedIndex == -1)
                    msgFinal += "- Vous devez sélectionner un étudiant \n";
                if(cb_Logements.SelectedIndex == -1)
                    msgFinal += "- Vous devez sélectionner un logement \n";
                MessageBox.Show(msgFinal);
                return;
            }
            if (actionForm == EnumAction.Modification)
            {
                Controleur.Vmodele.DT[3].Rows[id][0] = (cb_Logements.SelectedItem as cbItem).Value;
                Controleur.Vmodele.DT[3].Rows[id][1] = (cb_Etudiants.SelectedItem as cbItem).Value;
                Controleur.Vmodele.DT[3].Rows[id][2] = dp_Date.ToString();
                Controleur.Vmodele.DT[3].Rows[id][3] = tb_Note.Text;
            }
            else
            {
                DataRow NewRow = Controleur.Vmodele.DT[3].NewRow();
                NewRow[0] = (cb_Logements.SelectedItem as cbItem).Value;
                NewRow[1] = (cb_Etudiants.SelectedItem as cbItem).Value;
                NewRow[2] = dp_Date.ToString();
                NewRow[3] = tb_Note.Text;
                Controleur.Vmodele.DT[3].Rows.Add(NewRow);
            }
            Controleur.Vmodele.DA[3].Update(Controleur.Vmodele.DT[3]);
            this.Parent.ActualiserForm();
            this.Close();


            //Passer par dataadapter pour update
         }

        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
