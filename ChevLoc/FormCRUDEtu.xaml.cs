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

namespace ChevLoc
{
    /// <summary>
    /// Logique d'interaction pour FormCRUDEtu.xaml
    /// </summary>
    public partial class FormCRUDEtu : Window
    {
        private int id;
        private EnumAction actionForm;
        public FormCRUDEtu(EnumAction action= EnumAction.Ajout)
        {
            InitializeComponent();
            actionForm = action;
            if (action == EnumAction.Ajout)
            {
            }
            else
            {
            }
            this.Show();
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
