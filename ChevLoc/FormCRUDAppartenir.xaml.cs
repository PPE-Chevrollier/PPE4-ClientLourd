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
    /// Logique d'interaction pour FormCRUDAppartenir.xaml
    /// </summary>
    public partial class FormCRUDAppartenir : Window
    {
        private int id;
        private EnumAction actionForm;
        private CRUD Parent;
        public FormCRUDAppartenir(CRUD Parent, int id = -1)
        {
            InitializeComponent();
            this.Parent = Parent;
            Controleur.Vmodele.charger_donnees("classes");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[2].Rows.Count; i++)
                {
                    cb_classesAppartenir.Items.Add(new cbItem(Controleur.Vmodele.DT[2].Rows[i][1].ToString(), Controleur.Vmodele.DT[2].Rows[i][0].ToString()));
                }
            }
            Controleur.Vmodele.charger_donnees("etudiants");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[14].Rows.Count; i++)
                {
                    cb_etudiantsAppartenir.Items.Add(new cbItem(Controleur.Vmodele.DT[14].Rows[i][1].ToString(), Controleur.Vmodele.DT[14].Rows[i][0].ToString()));
                }
            }


            if (id == -1)
            {
                actionForm = EnumAction.Ajout;
            }
            else
            {
                actionForm = EnumAction.Modification;
                this.id = id;
                //charger combo
                for (int i = 0; i < Controleur.Vmodele.DT[14].Rows.Count; i++)
                {
                    if (Controleur.Vmodele.DT[14].Rows[i][0].ToString() == Controleur.Vmodele.DT[1].Rows[id][1].ToString())
                    {
                        cb_etudiantsAppartenir.SelectedIndex = i;
                    }
                }
                for (int i = 0; i < Controleur.Vmodele.DT[2].Rows.Count; i++)
                {
                    if (Controleur.Vmodele.DT[2].Rows[i][0].ToString() == Controleur.Vmodele.DT[1].Rows[id][0].ToString())
                    {
                        cb_classesAppartenir.SelectedIndex = i;
                    }
                }


                tb_anneeApartenir.Text = (Controleur.Vmodele.DT[1].Rows[id][2].ToString());
            }
            this.Show();
        }

        private void btn_validerApartenir_Click(object sender, RoutedEventArgs e)
        {
            string msg ="";
            string msgFinal ="Formulaire non conforme : \n\n";
            if(cb_classesAppartenir.Text == "" || cb_etudiantsAppartenir.Text == "" || !ControleSaisie.Entier(tb_anneeApartenir.Text,1900,Convert.ToInt32(DateTime.Today.Year)+1,ref msg))
            {
                if (cb_classesAppartenir.Text == "")
                    msgFinal += "- Vous devez sélectionner une classe\n";
                if (cb_etudiantsAppartenir.Text == "")
                    msgFinal += "- Vous devez sélectionner un étudiant\n";
                if(!ControleSaisie.Entier(tb_anneeApartenir.Text,1900,Convert.ToInt32(DateTime.Today.Year)+1,ref msg))
                    msgFinal += msg;
                MessageBox.Show(msgFinal);
                return;
            }
            if (actionForm == EnumAction.Modification)
            {
                Controleur.Vmodele.DT[1].Rows[id][0] = (cb_classesAppartenir.SelectedItem as cbItem).Value;
                Controleur.Vmodele.DT[1].Rows[id][1] = (cb_etudiantsAppartenir.SelectedItem as cbItem).Value;
                Controleur.Vmodele.DT[1].Rows[id][2] = tb_anneeApartenir.Text;
            }
            else
            {
                DataRow NewRow = Controleur.Vmodele.DT[1].NewRow();
                NewRow[0] = (cb_classesAppartenir.SelectedItem as cbItem).Value;
                NewRow[1] = (cb_etudiantsAppartenir.SelectedItem as cbItem).Value;
                NewRow[2] = tb_anneeApartenir.Text;
                Controleur.Vmodele.DT[1].Rows.Add(NewRow);
            }
            Controleur.Vmodele.DA[1].Update(Controleur.Vmodele.DT[1]);
            this.Parent.ActualiserForm();
            this.Close();
        }

        private void btn_annulerAppartenir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
