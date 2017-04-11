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
    /// Logique d'interaction pour FormCRUDComposent.xaml
    /// </summary>
    public partial class FormCRUDComposent : Window
    {
       private int id;
        private EnumAction actionForm;
        private CRUD Parent;
        public FormCRUDComposent(CRUD Parent, int id = -1)
        {
            InitializeComponent();
            this.Parent = Parent;
            Controleur.Vmodele.charger_donnees("equipements");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[7].Rows.Count; i++)
                {
                    cbEquipement.Items.Add(new cbItem(Controleur.Vmodele.DT[7].Rows[i][1].ToString(), Controleur.Vmodele.DT[7].Rows[i][0].ToString()));
                }
            }
            Controleur.Vmodele.charger_donnees("logements");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count; i++)
                {
                    cbLogement.Items.Add(new cbItem(Controleur.Vmodele.DT[8].Rows[i][1].ToString(), Controleur.Vmodele.DT[8].Rows[i][0].ToString()));
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
                    for (int i = 0; i < Controleur.Vmodele.DT[7].Rows.Count; i++)
                    {
                        if (Controleur.Vmodele.DT[7].Rows[i][0].ToString() == Controleur.Vmodele.DT[4].Rows[id][0].ToString())
                        {
                            cbEquipement.SelectedIndex = i;
                        }
                    }
                    for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count; i++)
                    {
                        if (Controleur.Vmodele.DT[8].Rows[i][0].ToString() == Controleur.Vmodele.DT[4].Rows[id][1].ToString())
                        {
                            cbLogement.SelectedIndex = i;
                        }
                    }
                
            }
            this.Show();
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if (cbEquipement.Text == "" || cbLogement.Text == "")
            {
                MessageBox.Show("Veuillez renseigner les 2 champs");
                return;
            }
            if (actionForm == EnumAction.Modification)
            {
                Controleur.Vmodele.DT[4].Rows[id][1] = (cbLogement.SelectedItem as cbItem).Value;
                Controleur.Vmodele.DT[4].Rows[id][0] = (cbEquipement.SelectedItem as cbItem).Value;
            }
            else
            {
                DataRow NewRow = Controleur.Vmodele.DT[4].NewRow();
                NewRow[1] = (cbLogement.SelectedItem as cbItem).Value;
                NewRow[0] = (cbEquipement.SelectedItem as cbItem).Value;
                Controleur.Vmodele.DT[4].Rows.Add(NewRow);
            }
            Controleur.Vmodele.DA[4].Update(Controleur.Vmodele.DT[4]);
            Parent.ActualiserForm();
            this.Close();
        }
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
