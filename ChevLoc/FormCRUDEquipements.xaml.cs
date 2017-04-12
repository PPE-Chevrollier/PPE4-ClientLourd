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
    /// Logique d'interaction pour FormCRUDEquipements.xaml
    /// </summary>
    public partial class FormCRUDEquipements : Window
    {
        private int id;
        private EnumAction actionForm;
        private DataTable dt;
        private CRUD Parent;
        public FormCRUDEquipements(DataTable DT, CRUD Parent, int id = -1)
        {
            InitializeComponent();
            this.dt = DT;
            this.Parent = Parent;

            if (id == -1)
            {
                actionForm = EnumAction.Ajout;
            }
            else
            {
                actionForm = EnumAction.Modification;
                this.id = id;
                tb_libelleEquipements.Text = (Controleur.Vmodele.DT[7].Rows[id][1].ToString());
            }

            this.Show();
        }

        private void btn_validerEquipements_Click(object sender, RoutedEventArgs e)
        {
            DataRow dr;
            if (actionForm == EnumAction.Modification)
            {
                Controleur.Vmodele.DT[7].Rows[id][1] = tb_libelleEquipements.Text;
            }
            else
            {
                DataRow NewRow = Controleur.Vmodele.DT[7].NewRow();
                NewRow[1] = tb_libelleEquipements.Text;
                Controleur.Vmodele.DT[7].Rows.Add(NewRow);
            }
            Controleur.Vmodele.DA[7].Update(Controleur.Vmodele.DT[7]);
            this.Parent.ActualiserForm();
            this.Close();

        }

        private void btn_AnnulerEquipements_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
