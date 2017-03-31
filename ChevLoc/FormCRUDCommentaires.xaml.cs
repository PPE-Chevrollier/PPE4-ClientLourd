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
        private DataTable dt;
        private Dictionary<string, string> dicoLogement, dicoEtu;
        public FormCRUDCommentaires(DataTable DT, int id = -1)
        {
            InitializeComponent();
            this.dt = DT;
            if (id==-1)
            {
                actionForm = EnumAction.Ajout;
                Controleur.Vmodele.charger_donnees("etudiants");
                if (Controleur.Vmodele.Chargement)
                {
                    for (int i = 0; i < Controleur.Vmodele.DT[14].Rows.Count; i++)
                    {
                        cb_Etudiants.Items.Add(Controleur.Vmodele.DT[14].Rows[i][4].ToString());
                    }
                }
                Controleur.Vmodele.charger_donnees("logements");
                if (Controleur.Vmodele.Chargement)
                {
                    for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count; i++)
                    {
                        cb_Logements.Items.Add(Controleur.Vmodele.DT[8].Rows[i][1].ToString());
                    }
                }
            }
            else
            {
                actionForm = EnumAction.Modification;
                this.id = id;
                Controleur.Vmodele.charger_donnees("etudiants");
                if (Controleur.Vmodele.Chargement)
                {
                    dicoEtu = new Dictionary<string, string>();
                    for (int i = 0; i < Controleur.Vmodele.DT[14].Rows.Count; i++)
                    {
                        
                        dicoEtu.Add(Controleur.Vmodele.DT[14].Rows[i][0].ToString(), Controleur.Vmodele.DT[14].Rows[i][4].ToString());
                        cb_Etudiants.Items.Add(dicoEtu.ElementAt(i).Value);
                        if (Controleur.Vmodele.DT[14].Rows[i][0].ToString() == Controleur.Vmodele.DT[3].Rows[id][0].ToString())
                        {
                            cb_Etudiants.SelectedIndex = i;
                        }
                    }
                }
                Controleur.Vmodele.charger_donnees("logements");
                if (Controleur.Vmodele.Chargement)
                {
                    dicoLogement = new Dictionary<string, string>();
                    for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count; i++)
                    {
                        dicoLogement.Add(Controleur.Vmodele.DT[8].Rows[i][0].ToString(), Controleur.Vmodele.DT[8].Rows[i][1].ToString());
                        cb_Logements.Items.Add(dicoLogement.ElementAt(i).Value);
                        if (Controleur.Vmodele.DT[8].Rows[i][0].ToString() == Controleur.Vmodele.DT[3].Rows[id][1].ToString())
                        {
                            cb_Logements.SelectedIndex = i;
                        }
                    }
                }
                dp_Date.Text = Controleur.Vmodele.DT[3].Rows[id][4].ToString();
                tb_Note.Text = (Controleur.Vmodele.DT[3].Rows[id][5].ToString());
                
            }
            this.Show();
        }

        private void btn_valider_Click(object sender, RoutedEventArgs e)
        {
            DataRow dr;
            /*string note = tb_Note.Text;
            string date = dp_Date.ToString();
            string etudiants = cb_Etudiants.ToString();
            string logements = cb_Logements.ToString();*/
            dr = Controleur.Vmodele.DT[3].NewRow();
            object[] rowArray = new object[4];
            rowArray[0] = dicoLogement.Values.ToList().IndexOf(cb_Logements.ToString());
            rowArray[1] = dicoEtu.Values.ToList().IndexOf(cb_Etudiants.ToString());
            rowArray[2] = dp_Date.ToString();
            rowArray[3] = tb_Note.Text;
            DataRow NewRow = Controleur.Vmodele.DT[3].NewRow();
            Controleur.Vmodele.DT[3].Rows[id].Delete();
            Controleur.Vmodele.DT[3].Rows.Add(NewRow);
            //Passer par dataadapter pour update
         }

        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
