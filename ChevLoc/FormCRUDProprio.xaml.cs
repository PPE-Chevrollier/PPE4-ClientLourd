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
    /// Logique d'interaction pour FormCRUDProprio.xaml
    /// </summary>
    public partial class FormCRUDProprio : Window
    {
        private int id;
        private EnumAction actionForm;
        private CRUD Parent;
        public FormCRUDProprio(CRUD Parent, int id = -1)
        {
            InitializeComponent();
            this.Parent = Parent;
            Controleur.Vmodele.charger_donnees("personnes");
            ChargerListeDerou();
            if (id == -1)
            {
                actionForm = EnumAction.Ajout;
            }
            else
            {
                actionForm = EnumAction.Modification;
                this.id = id;
                ChargerInfoProprio(id);
            }
            this.Show();
        }
        public void ChargerListeDerou()
        {
            cbSexe.Items.Add("M");
            cbSexe.Items.Add("F");
        }
        public void ChargerInfoProprio(int id)
        {
            if (Controleur.Vmodele.DT[10].Rows.Find(id)[3].ToString() == "M")
            {
                cbSexe.SelectedIndex = 0;
            }
            else
            {
                cbSexe.SelectedIndex = 1;
            }
            tbNom.Text = Controleur.Vmodele.DT[10].Rows.Find(id)[1].ToString();
            tbPrenom.Text = Controleur.Vmodele.DT[10].Rows.Find(id)[2].ToString(); 
        }
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if (tbNom.Text == "" || tbPrenom.Text == "" || cbSexe.Text == "")
            {
                string msgFinal = "Formulaire non conforme :\n\n";
                if (tbNom.Text == "")
                    msgFinal += "- Vous devez renseigner un nom\n";
                if (tbPrenom.Text == "")
                    msgFinal += "- Vous devez renseigner un prénom\n";
                if (cbSexe.Text == "")
                    msgFinal += "- Vous devez renseigner le sexe (dans la liste déroulante, 'gros' n'est pas une option valable)";
                MessageBox.Show(msgFinal);
                return;
            }
            if (actionForm == EnumAction.Modification)
            {
                if (Controleur.Vmodele.DT[10].Rows.Find(id)[1].ToString() == tbNom.Text && Controleur.Vmodele.DT[10].Rows.Find(id)[2].ToString() == tbPrenom.Text)
                {
                    MessageBox.Show("Propriétaire déjà existant.");
                }
                else
                {
                    Controleur.Vmodele.DT[10].Rows.Find(id)[1] = tbNom.Text.ToLower();
                    Controleur.Vmodele.DT[10].Rows.Find(id)[2] = tbPrenom.Text.ToLower();
                    Controleur.Vmodele.DT[10].Rows.Find(id)[3] = cbSexe.SelectedValue;
                    Controleur.Vmodele.DA[10].Update(Controleur.Vmodele.DT[10]);
                }
            }
            else
            {
                DataRow NewRowPers = Controleur.Vmodele.DT[10].NewRow();
                NewRowPers[0] = Controleur.Vmodele.ReturnLastIdPersonne() + 1;
                NewRowPers[1] = tbNom.Text.ToLower();
                NewRowPers[2] = tbPrenom.Text.ToLower();
                NewRowPers[3] = cbSexe.SelectedValue;
                Controleur.Vmodele.DT[10].Rows.Add(NewRowPers);
                Controleur.Vmodele.DA[10].Update(Controleur.Vmodele.DT[10]);
                Controleur.Vmodele.charger_donnees("personnes");
                DataRow NewRowProprio = Controleur.Vmodele.DT[18].NewRow();
                NewRowProprio[0] = Controleur.Vmodele.ReturnLastIdPersonne().ToString();
                Controleur.Vmodele.DT[18].Rows.Add(NewRowProprio);
                Controleur.Vmodele.DA[18].Update(Controleur.Vmodele.DT[18]);
            }
            Parent.ActualiserForm();
            this.Close();
        }
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
