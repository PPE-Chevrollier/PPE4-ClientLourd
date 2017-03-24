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
            Resize(0,0);
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
                        case "etudiants":
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

        private void dGvChevLoc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private double ValMini(double AParam, double AValue)
        {
            if (AValue > AParam)
            {
                return AValue;
            }
            else
            {
                return AParam;
            }
        }
        private void Resize(double AHeightMini, double AWidthMini)
        {
            cbTable.Margin = new System.Windows.Thickness(this.Width / 6, this.Height / 20, this.Width / 6, this.Height * 0.75);
            dGvChevLoc.Margin = new System.Windows.Thickness(this.Width / 10, this.Height / 5, this.Width / 10, this.Height / 5);
            btnModifier.Margin = new System.Windows.Thickness(this.Width / 3, this.Height * 0.8, this.Width / 3, this.Height / 10);
            btnAjouter.Margin = new System.Windows.Thickness(this.Width / 10, this.Height * 0.8, this.Width * 0.7, this.Height / 10);
            btnSupprimer.Margin = new System.Windows.Thickness(this.Width * 0.7, this.Height * 0.8, this.Width / 30, this.Height / 10);
            cbTable.Height = ValMini(this.Height / 10,10);
            cbTable.Width = ValMini(this.Width * 0.75,30);
            dGvChevLoc.Height = this.Height / 2;
            dGvChevLoc.Width = this.Width * 3 / 4;
            btnModifier.Height = ValMini(this.Height / 11,10);
            btnModifier.Width = ValMini(this.Width / 4,30);
            btnAjouter.Height = ValMini(this.Height / 11, 10);
            btnAjouter.Width = ValMini(this.Width / 4, 30);
            btnSupprimer.Height = ValMini(this.Height / 11, 10);
            btnSupprimer.Width = ValMini(this.Width / 4, 30);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Resize(0, 0);
        }
    }
}
