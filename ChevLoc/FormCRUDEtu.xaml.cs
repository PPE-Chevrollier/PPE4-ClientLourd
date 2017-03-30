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
        private char actionForm;
        public FormCRUDEtu(char action='a')
        {
            InitializeComponent();
            actionForm = action;
            if (action == 'm')
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
