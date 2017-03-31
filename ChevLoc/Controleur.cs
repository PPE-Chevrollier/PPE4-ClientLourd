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
       /*
        /// <summary>
        /// permet le crud sur la table classes
        /// </summary>
        /// <param name="c">définit l'action : c:create, u update, d delete</param>
        /// <param name="indice">indice de l'élément sélectionné à modifier ou supprimer, -1 si ajout</param>
        public static void crud_classes(Char c, int indice)
        {
            if (c == 'd') // cas de la suppression
            {
                //   DialogResult rep = MessageBox.Show("Etes-vous sûr de vouloir supprimer ce constructeur "+ vmodele.DTConstructeur.Rows[indice][1].ToString()+ " ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DialogResult rep = MessageBox.Show("Etes-vous sûr de vouloir supprimer ce constructeur " + vmodele.DT[2].Rows[indice][2].ToString() + " ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rep == DialogResult.Yes)
                {
                    // on supprime l’élément du DataTable
                    vmodele.DT[1].Rows[indice].Delete();		// suppression dans le DataTable
                    vmodele.DA[1].Update(vmodele.DT[2]);			// mise à jour du DataAdapter
                }
            }
            else
            {
                // cas de l'ajout et modification
                FormCRUDClasses formCRUD = new FormCRUDClasses();  // création de la nouvelle forme
                if (c == 'c')  // mode ajout donc pas de valeur à passer à la nouvelle forme
                {
                    formCRUD.tb_classes.Clear();
                }

                if (c == 'u')   // mode update donc on récupère les champs
                {
                    // on remplit les zones par les valeurs du dataGridView correspondantes
                    formCRUD.tb_classes.Text = vmodele.DT[2].Rows[indice][2].ToString();

                }
                // on affiche la nouvelle form
                formCRUD.ShowDialog();

            }
        }

        public static void crud_valider_classes(Char c, int indice, FormCRUDClasses formCRUD) { 
            if (c == 'c') // ajout
            {
                // on crée une nouvelle ligne dans le dataView
                if (formCRUD.tb_classes.Text != "")
                {
                    DataRow NouvLigne = vmodele.DT[2].NewRow();
                    NouvLigne["libelle_classes"] = formCRUD.tb_classes.Text;
                    vmodele.DT[2].Rows.Add(NouvLigne);
                    vmodele.DA[2].Update(vmodele.DT[2]);
                }
            }

            if (c == 'u')  // modif
            {
                // on met à jour le dataTable avec les nouvelles valeurs
                vmodele.DT[2].Rows[indice]["libelle_classes"] = formCRUD.tb_classes.Text;
                vmodele.DA[2].Update(vmodele.DT[2]);

                // MessageBox.Show("OK : données enregistrées Classes");
                formCRUD.Close();  // on ferme la form
            }
        }   */
        
        #endregion
    }
}
