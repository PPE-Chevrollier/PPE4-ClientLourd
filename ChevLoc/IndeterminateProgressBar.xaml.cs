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
using System.Threading;

namespace ChevLoc
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class IndeterminateProgressBar : Window
    {
        public IndeterminateProgressBar()
        {
            InitializeComponent();
        }
        public IndeterminateProgressBar(int AWidth, int AHeight, string ALbl)
        {
            InitializeComponent();
            this.Visibility = Visibility.Visible;
            this.Height = AHeight;
            this.Width = AWidth;
            this.lblTitle.Content = ALbl;
        }
        public IndeterminateProgressBar(string ALbl)
        {
            InitializeComponent();
            this.Focus();
            this.Visibility = Visibility.Visible;
            this.lblTitle.Content = ALbl;
        }
    }
}
