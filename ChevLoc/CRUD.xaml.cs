using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChevLoc
{
    /// <summary>
    /// Logique d'interaction pour CRUD.xaml
    /// </summary>
    public partial class CRUD : Window
    {
        private BindingSource bindingSource1;

        public CRUD()
        {
            InitializeComponent();
            CRUD_Load();
        }
        private void CRUD_Load()
        {
            dGvChevLoc.Visibility = Visibility.Hidden;
            Controleur.Vmodele.charger_donnees("toutes");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[0].Rows.Count; i++)
                {
                    cbTable.Items.Add(Controleur.Vmodele.DT[0].Rows[i][0].ToString());
                }
            }
        }

        /// <summary>
        /// évènement SelectedIndexChanged : à la sélection d'une table, on charge les données de la BD correspondantes dans le dataGridView 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTable_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cbTable.SelectedIndex != -1)
            {
                string table = cbTable.SelectedItem.ToString(); // récupération de la table sélectionnée
                Controleur.Vmodele.charger_donnees(table);      // chargement des données de la table sélectionné dans le DT correspondant
                if (Controleur.Vmodele.Chargement)
                {
                    // un DT par table
                    bindingSource1 = new BindingSource();

                    switch (table)
                    {
                        case "appartenir":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[1];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "classes":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[2];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "commentaires":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[3];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "composent":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[4];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "correspondre":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[5];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "dates":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[6];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "equipements":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[7];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "logements":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[8];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "motifs":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[9];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "personnes":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[10];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "photos":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[11];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "propositions":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[12];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                        case "villes":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[13];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            break;
                    }

                    // mise à jour du dataGridView via le bindingSource rempli par le DataTable
                    dGvChevLoc.Items.Refresh();
                    dGvChevLoc.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
