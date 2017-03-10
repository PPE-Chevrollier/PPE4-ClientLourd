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

        /// <summary>
        /// les DataAdapter et DataTable seront gérés dans des collection avec pour chaque un indice correspondant :
        /// indice 0 : récupération des noms des tables
        /// indice 1 : Table appartenir
        /// indice 2 : Table classes
        /// indice 3 : Table commentaire
        /// indice 4 : Table composent 
        /// indice 5 : Table correspondre
        /// indice 6 : Table date
        /// indice 7 : Table equipement
        /// indice 8 : Table logement
        /// indice 9 : Table motif
        /// indice 10 : Table personnes
        /// indice 11 : Table photo 
        /// indice 12 : Table proposition
        /// collection de DataAdapter
        /// </summary>
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
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);
                cmd.ExecuteNonQuery();
                MySqlCommand cmd2 = new MySqlCommand(sql2, myConnection);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show("error " + err.Message);
            }
        }


        /// <summary>
        /// les DataAdapter et DataTable seront gérés dans des collection avec pour chaque un indice correspondant :
        /// indice 0 : récupération des noms des tables
        /// indice 1 : Table appartenir
        /// indice 2 : Table classes
        /// indice 3 : Table commentaire
        /// indice 4 : Table composent 
        /// indice 5 : Table correspondre
        /// indice 6 : Table date
        /// indice 7 : Table equipement
        /// indice 8 : Table logement
        /// indice 9 : Table motif
        /// indice 10 : Table personnes
        /// indice 11 : Table photo 
        /// indice 12 : Table proposition
        /// collection de DataAdapter
        /// </summary>
        public Modele()
        {

            for (int i = 0; i < 16; i++)
            {
                dA.Add(new MySqlDataAdapter());
                dT.Add(new DataTable());
            }
        }
        /// <summary>
        /// méthode seconnecter permettant la connexion à la BD : bd_ppe3_notagame
        /// </summary>
        public void seconnecter()
        {
            string myConnectionString = "Database=bd_ppe_test;Data Source=192.168.152.2;User Id=ppe;Password=Azerty123;";
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
        /// méthode générique privée pour charger le résultat d'une requête dans un dataTable via un dataAdapter
        /// Méthode appelée par charger_donnees(string table)
        /// </summary>
        /// <param name="requete">requete à charger</param>
        /// <param name="DT">dataTable</param>
        /// <param name="DA">dataAdapter</param>
        private void charger(string requete, DataTable DT, MySqlDataAdapter DA)
        {
            DA.SelectCommand = new MySqlCommand(requete, myConnection);

            // pour spécifier les instructions de mise à jour (insert, delete, update)
            MySqlCommandBuilder CB1 = new MySqlCommandBuilder(DA);
            try
            {
                DT.Clear();
                DA.Fill(DT);
                chargement = true;
            }
            catch (Exception err)
            {
                MessageBox.Show("Erreur chargement dataTable : " + err, "PBS table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errgrave = true;
            }
        }

        /// <summary>
        /// charge dans un DT les données de la table passée en paramètre
        /// </summary>
        /// <param name="table">nom de la table à requêter</param>
        public void charger_donnees(string table)
        {
            chargement = false;
            if (!connopen) return;		// pour vérifier que la BD est bien ouverte

            switch (table)
            {
                case "toutes":
                    charger("show tables;", dT[0], dA[0]);
                    break;
                case "APPARTENIR":
                    charger("select * from APPARTENIR;", dT[1], dA[1]);
                    break;
                case "CLASSES":
                    charger("select * from CLASSES;", dT[2], dA[2]);
                    break;
                case "COMMENTAIRES":
                    charger("select * from COMMENTAIRES;", dT[3], dA[3]);
                    break;
                case "COMPOSENT":
                    charger("select * from COMPOSENT;", dT[4], dA[4]);
                    break;
                case "CORRESPONDRE":
                    charger("select * from CORRESPONDRE;", dT[5], dA[5]);
                    break;
                case "DATES":
                    charger("select * from DATES;", dT[6], dA[6]);
                    break;
                case "EQUIPEMENTS":
                    charger("select * from EQUIPEMENTS;", dT[7], dA[7]);
                    break;
                case "LOGEMENTS":
                    charger("select * from LOGEMENTS;", dT[8], dA[8]);
                    break;
                case "MOTIFS":
                    charger("select * from MOTIFS ;", dT[9], dA[9]);
                    break;
                case "PERSONNES":
                    charger("select * from PERSONNES ;", dT[10], dA[10]);
                    break;
                case "PHOTOS":
                    charger("select * from PHOTOS ;", dT[11], dA[11]);
                    break;
                case "PROPOSITIONS":
                    charger("select * from PROPOSITIONS ;", dT[12], dA[12]);
                    break;
            }
        }
        #endregion
    }
}