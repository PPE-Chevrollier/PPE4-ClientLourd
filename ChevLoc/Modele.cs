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
    public class Modele
    {
        #region propriétés

        private MySqlConnection myConnection;   // objet de connexion
        private bool connopen = false;          // test si la connexion est faite
        private bool errgrave = false;          // test si erreur lors de la connexion
        private bool chargement = false;        // test si le chargement d'une requête est fait
        private List<MySqlDataAdapter> dA = new List<MySqlDataAdapter>();
        private List<DataTable> dT = new List<DataTable>();
        #endregion

        #region accesseurs

        public MySqlConnection MyConnection
        {
            get { return myConnection; }
            set { myConnection = value; }
        }

        /// <summary>
        /// test si la connexion à la BD est ouverte
        /// </summary>
        public bool Connopen
        {
            get { return connopen; }
            set { connopen = value; }
        }

        /// <summary>
        /// Accesseur de la collection des DataAdapter
        /// </summary>
        public List<MySqlDataAdapter> DA
        {
            get { return dA; }
            set { dA = value; }
        }

        /// <summary>
        /// Accesseur de la collection des DataTable
        /// </summary>
        public List<DataTable> DT
        {
            get { return dT; }
            set { dT = value; }
        }
        /// <summary>
        /// test si erreur lors de la connexion
        /// </summary>
        public bool Errgrave
        {
            get { return errgrave; }
            set { errgrave = value; }
        }

        /// <summary>
        /// test si le chargement d'une requête est fait
        /// </summary>
        public bool Chargement
        {
            get { return chargement; }
            set { chargement = value; }
        }
        #endregion

        #region Méthodes
        public Modele()
        {
            for (int i = 0; i < 1; i++)
            {
                dA.Add(new MySqlDataAdapter());
                dT.Add(new DataTable());
            }
        }
        public void seconnecter()
        {
            string myConnectionString = "Server=192.168.152.2;Database=bd_ppe_test;User Id=ppe;Password=Azerty123;";
            //Database=bd_notagame;Data Source=192.168.152.2;User Id=notagame;Password=Azerty123;
            myConnection = new MySqlConnection(myConnectionString);
            try // tentative 
            {
                myConnection.Open();
                connopen = true;
            }
            catch (Exception err)// gestion des erreurs
            {
                MessageBox.Show("Erreur ouverture BD NotaGame : " + err, "PBS connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connopen = false; errgrave = true;
            }
        }

        /// <summary>
        /// méthode sedeconnecter pour se déconnecter à la BD
        /// </summary>
        public void sedeconnecter()
        {
            if (!connopen)
                return;
            try
            {
                myConnection.Close();
                myConnection.Dispose();
                connopen = false;
            }
            catch (Exception err)
            {
                MessageBox.Show("Erreur fermeture BD NotaGame : " + err, "PBS deconnection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errgrave = true;
            }
        }
        /// <summary>
        /// Insère les données étudiant
        /// </summary>
        /// <param name="nom">nom</param>
        /// <param name="prenom">prenom</param>
        /// <param name="dnaiss">date de naissance</param>
        /// <param name="sexe">sex</param>
        /// <param name="mdp">mdp</param>
        /// <returns></returns>
        public void InsertEtudiant(string nom, string prenom, string dnaiss, string sexe, string mdp, string classe)
        {
            string sql = @"CALL addEtudiant('" + nom + "','" + prenom + "', '" + dnaiss + "', '" + sexe + "','etudiant');";
            string sql2 = @"CALL addClasseForEtu('" + classe + "');";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql,myConnection);
                cmd.ExecuteNonQuery();
                MySqlCommand cmd2 = new MySqlCommand(sql2, myConnection);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show("error " + err.Message);
            }
        }
        #endregion
    }
}
