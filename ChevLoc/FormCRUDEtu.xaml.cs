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
            Controleur.Vmodele.charger_donnees("logements");
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
                ChargerInfoEtu();
            }
            this.Show();
            ChargerListeDerou();
        }
        public void ChargerInfoEtu()
        {
            tbLogin.Text = tableEtu.Rows[id][4].ToString();
            tbMdp.Text = "";
            tbMail.Text = tableEtu.Rows[id][8].ToString();
            tbNom.Text = tableEtu.Rows[id][1].ToString();
            tbPrenom.Text = tableEtu.Rows[id][2].ToString();
            tbTel.Text = tableEtu.Rows[id][7].ToString();
        }
        public void ChargerListeDerou()
        {
            for (int i = 0; i < Controleur.Vmodele.DT[8].Rows.Count;i++ )
            {
                cbLogement.Items.Add(Controleur.Vmodele.DT[8].Rows[i][0]);
            }
            cbSexe.Items.Add("M");
            cbSexe.Items.Add("F");
        }
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
