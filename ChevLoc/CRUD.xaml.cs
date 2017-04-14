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
using System.Security.Cryptography;
using System.Windows.Controls.Primitives;

namespace ChevLoc
{
    public partial class CRUD : Window
    {
        private System.Windows.Forms.BindingSource bindingSource1;

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
                    if (Controleur.Vmodele.DT[0].Rows[i][0].ToString() != "motifs")
                    {
                        cbTable.Items.Add(Controleur.Vmodele.DT[0].Rows[i][0].ToString());
                    }
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
            ActualiserForm();
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
        private void OuvrirForm(int CRUD=-1)
        {
            switch (cbTable.Text)
             {
                 case "etudiants" :
                     FormCRUDEtu FCE;
                     if (CRUD == -1)
                     {
                         FCE = new FormCRUDEtu(this);
                     }
                     else
                     {
                         FCE = new FormCRUDEtu(this, Convert.ToInt16(Controleur.Vmodele.DT[14].Rows[CRUD][0]));
                     }
                     break;
                case "composent" :
                     FormCRUDComposent FCCo = new FormCRUDComposent(this, CRUD);
                        break;
                case "proprietaires":
                        FormCRUDProprio FCP;
                        if (CRUD == -1)
                        {
                            FCP = new FormCRUDProprio(this);
                        }
                        else
                        {
                            FCP = new FormCRUDProprio(this, Convert.ToInt16(Controleur.Vmodele.DT[21].Rows[CRUD][0]));
                        }
                        break;
                case "commentaires":
                     FormCRUDCommentaires FCC = new FormCRUDCommentaires(this,CRUD);
                     break;
                case "equipements":
                    FormCRUDEquipements FCQ = new FormCRUDEquipements(this, CRUD);
                    break;
                case "appartenir":
                    FormCRUDAppartenir FCA = new FormCRUDAppartenir(this, CRUD);
                    break;

            }
        }
        public void AfficherButton(bool b)
        {
            if (b)
            {
                btnAjouter.IsEnabled = true;
                btnModifier.IsEnabled = true;
                btnSupprimer.IsEnabled = true;
            }
            else
            {
                btnAjouter.IsEnabled = false;
                btnModifier.IsEnabled = false;
                btnSupprimer.IsEnabled = false;
            }
        }
        public void ActualiserForm()
        {
            if (cbTable.SelectedIndex != -1)
            {
                string table = cbTable.SelectedItem.ToString(); // récupération de la table sélectionnée
                Controleur.Vmodele.charger_donnees(table);      // chargement des données de la table sélectionné dans le DT correspondant
                if (Controleur.Vmodele.Chargement)
                {
                    // un DT par table
                    bindingSource1 = new System.Windows.Forms.BindingSource();

                    switch (table)
                    {
                        case "appartenir":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[1];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(true);
                            break;
                        case "classes":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[2];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "commentaires":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[16];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(true);
                            break;
                        case "composent":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[20];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(true);
                            break;
                        case "correspondre":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[5];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "dates":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[6];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "equipements":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[7];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(true);
                            break;
                        case "logements":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[8];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "motifs":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[9];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "personnes":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[10];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "photos":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[11];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "propositions":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[12];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "villes":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[13];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "etudiants":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[14];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(true);
                            break;
                        case "proprietaires":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[21];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(true);
                            break;
                        case "appartements":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[22];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "chambresHabitant":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[23];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "illustrer":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[24];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "messages":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[25];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                        case "studios":
                            bindingSource1.DataSource = Controleur.Vmodele.DT[26];
                            dGvChevLoc.ItemsSource = bindingSource1;
                            AfficherButton(false);
                            break;
                    }

                    // mise à jour du dataGridView via le bindingSource rempli par le DataTable
                    dGvChevLoc.Items.Refresh();
                    dGvChevLoc.Visibility = Visibility.Visible;
                }
            }
        }
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            OuvrirForm();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (dGvChevLoc.SelectedIndex != -1)
            {
                OuvrirForm(dGvChevLoc.SelectedIndex);
            }
            else
            {
                System.Windows.MessageBox.Show("Sélectionnez une ligne à modifier.");
            }
        }
        public string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }
        public string GenerateMdp()
        {
            string caracteres = "azertyuiopqsdfghjklmwxcvbn1234567890";
            Random selAlea = new Random();


            string sel = "";
            for (int i = 0; i < 8; i++) // 8 caracteres
            {
                int majOrMin = selAlea.Next(2);
                string carac = caracteres[selAlea.Next(0, caracteres.Length)].ToString();
                if (majOrMin == 0)
                {
                    sel += carac.ToUpper(); // Maj
                }
                else
                {
                    sel += carac.ToLower(); //Min
                }
            }
            return sel;
        }
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dGvChevLoc.SelectedIndex != -1)
            {
                switch (cbTable.Text)
                {
                    case "etudiants":
                        Controleur.Vmodele.charger_donnees("personnes");
                        Mail.CreateMessage(Controleur.Vmodele.DT[17].Rows[dGvChevLoc.SelectedIndex][5].ToString(), "Adieu !", Controleur.Vmodele.DT[10].Rows.Find(Controleur.Vmodele.DT[17].Rows[dGvChevLoc.SelectedIndex][0])[2] + ",\n\nVous êtes le maillon faible ! \n Malheureusement (selon le point de vue), nous avons été contraints (!) de vous supprimer de notre base de données.\nNous espérons sincèrement (?) que vous vous en remettrez. Pour nous, ça va.\n\nL'équipe Chevloc");
                        Controleur.Vmodele.DT[17].Rows[dGvChevLoc.SelectedIndex][3] = DBNull.Value;
                        Controleur.Vmodele.DA[17].Update(Controleur.Vmodele.DT[17]);
                        Controleur.Vmodele.DT[10].Rows.Find(Controleur.Vmodele.DT[17].Rows[dGvChevLoc.SelectedIndex][0]).Delete();
                        Controleur.Vmodele.DA[10].Update(Controleur.Vmodele.DT[10]);
                        break;
                    case "proprietaires":
                        Controleur.Vmodele.charger_donnees("personnes");
                        Controleur.Vmodele.DT[10].Rows.Find(Controleur.Vmodele.DT[18].Rows[dGvChevLoc.SelectedIndex][0]).Delete();
                        Controleur.Vmodele.DA[10].Update(Controleur.Vmodele.DT[10]);
                        break;
                    case "composent":
                        Controleur.Vmodele.DT[4].Rows[dGvChevLoc.SelectedIndex].Delete();
                        Controleur.Vmodele.DA[4].Update(Controleur.Vmodele.DT[4]);
                        break;
                    case "commentaires":
                        Controleur.Vmodele.DT[3].Rows[dGvChevLoc.SelectedIndex].Delete();
                        Controleur.Vmodele.DA[3].Update(Controleur.Vmodele.DT[3]);
                        break;
                    case "equipements":
                        Controleur.Vmodele.DT[7].Rows[dGvChevLoc.SelectedIndex].Delete();
                        Controleur.Vmodele.DA[7].Update(Controleur.Vmodele.DT[7]);
                        ActualiserForm();
                        break;
                    case "appartenir":
                        Controleur.Vmodele.DT[1].Rows[dGvChevLoc.SelectedIndex].Delete();
                        Controleur.Vmodele.DA[1].Update(Controleur.Vmodele.DT[1]);
                        ActualiserForm();
                        break;
                }
                ActualiserForm();
            }
            else
            {
                System.Windows.MessageBox.Show("Sélectionnez une ligne à supprimer.");
            }
        }
    }
}
