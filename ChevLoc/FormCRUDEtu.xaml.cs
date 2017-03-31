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
        private DataTable tableEtu;
        private EnumAction actionForm;
        public FormCRUDEtu(DataTable DT,int id = -1)
        {
            InitializeComponent();
            tableEtu = DT;
            if (id == -1)
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
        public void ChargerInfoEtu()
        {
            
        }
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
