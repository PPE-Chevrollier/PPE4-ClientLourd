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
        public FormCRUDCommentaires(DataTable DT, int id = -1)
        {
            InitializeComponent();
            this.dt = DT;
            if (id==-1)
            {
                actionForm = EnumAction.Ajout;
            }
            else
            {
                actionForm = EnumAction.Modification;
                this.id = id;
            }
            this.Show();
        }

        private void btn_valider_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {

        }

        /*
        private void FormCRUDCommentaires_Load()
        {
            Controleur.Vmodele.charger_donnees("CRUDCommentairesEtudiants");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[14].Rows.Count; i++)
                {
                    cb_Etudiants.Items.Add(Controleur.Vmodele.DT[14].Rows[i][14].ToString());
                }
            }
            Controleur.Vmodele.charger_donnees("CRUDCommentairesLogements");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[15].Rows.Count; i++)
                {
                    cb_Logements.Items.Add(Controleur.Vmodele.DT[15].Rows[i][15].ToString());
                }
            }
        }*/
    }
}
