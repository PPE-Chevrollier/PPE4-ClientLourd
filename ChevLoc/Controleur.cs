using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;


namespace ChevLoc
{
    public static class Controleur
    {
        #region propriétés
        private static Modele vmodele;
        #endregion

        #region accesseurs
        /// <summary>
        /// propriété Vmodele
        /// </summary>
        public static Modele Vmodele
        {
            get { return vmodele; }
            set { vmodele = value; }
        }
        #endregion

        #region méthodes
        public static void init()
        {
            Vmodele = new Modele();
        }
        /// <summary>
        /// permet le crud sur la table Classes
        /// </summary>
        /// <param name="c">définit l'action : c:create, u update, d delete</param>
        /// <param name="indice">indice de l'élément sélectionné à modifier ou supprimer, -1 si ajout</param>
        /*public static void crud_classes(Char c, int indice)
        {
            if (c == 'd') // cas de la suppression
            {
                //   DialogResult rep = MessageBox.Show("Etes-vous sûr de vouloir supprimer ce constructeur "+ vmodele.DTConstructeur.Rows[indice][1].ToString()+ " ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DialogResult rep = MessageBox.Show("Etes-vous sûr de vouloir supprimer ce constructeur " + vmodele.DT[2].Rows[indice][2].ToString() + " ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rep == DialogResult.Yes)
                {
                    // on supprime l’élément du DataTable
                    vmodele.DT[2].Rows[indice].Delete();		// suppression dans le DataTable
                    vmodele.DA[2].Update(vmodele.DT[2]);			// mise à jour du DataAdapter
                }
            }
            else
            {
                // cas de l'ajout et modification
                FormCRUDClasses FormCRUD = new FormCRUDClasses();  // création de la nouvelle forme
                if (c == 'c')  // mode ajout donc pas de valeur à passer à la nouvelle forme
                {
                    FormCRUD.TbNom.Clear();
                }

                if (c == 'u')   // mode update donc on récupère les champs
                {
                    // on remplit les zones par les valeurs du dataGridView correspondantes
                    FormCRUD.TbNom.Text = vmodele.DT[2].Rows[indice][2].ToString();

                }
                // on affiche la nouvelle form
                FormCRUD.ShowDialog();

                /* // si l’utilisateur clique sur OK
                 if (FormCRUD.DialogResult = DialogResult.OK)
                 {
                     if (c == 'c') // ajout
                     {
                         // on crée une nouvelle ligne dans le dataView
                         if (FormCRUD.TbNom.Text != "")
                         {
                             DataRow NouvLigne = vmodele.DT[1].NewRow();
                             NouvLigne["NomC"] = FormCRUD.TbNom.Text;
                             vmodele.DT[2].Rows.Add(NouvLigne);
                             vmodele.DA[2].Update(vmodele.DT[2]);
                         }
                     }

                     if (c == 'u')  // modif
                     {
                         // on met à jour le dataTable avec les nouvelles valeurs
                         vmodele.DT[2].Rows[indice]["NomC"] = FormCRUD.TbNom.Text;
                         vmodele.DA[2].Update(vmodele.DT[2]);
                     }

                     // MessageBox.Show("OK : données enregistrées Constructeur");
                     FormCRUD.Close();  // on ferme la form
                 }
                 else
                 {
                     MessageBox.Show("Annulation : aucune donnée enregistrée");
                     FormCRUD.Close();
                 }
             }
         }*/

                #endregion
    }
}
