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
        public CRUD()
        {
            InitializeComponent();
            CRUD_Load();
        }
        private void CRUD_Load()
        {
            dGvChevLoc.IsEnabled = false;
            Controleur.Vmodele.charger_donnees("toutes");
            if (Controleur.Vmodele.Chargement)
            {
                for (int i = 0; i < Controleur.Vmodele.DT[0].Rows.Count; i++)
                {
                    cbTable.Items.Add(Controleur.Vmodele.DT[0].Rows[i][0].ToString());
                }
            }
        }

        private void cbTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
