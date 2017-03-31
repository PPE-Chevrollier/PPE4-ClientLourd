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
        public int InsertEtudiant(string nom, string prenom, string dnaiss, string sexe, string mdp,string classe, string email)
        {
            string sql = @"CALL addetudiant('" + nom + "','" + prenom + "', '" + dnaiss + "', '" + sexe + "','" + mdp +"','" + classe +"','" + email +"');";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, myConnection);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                return 0;
            }
        }
        public DataTable ReturnLoginEmailLastId()
        {
            DataTable dT = new DataTable();
            MySqlDataAdapter dA = new MySqlDataAdapter();
            string sql = "SELECT login_etudiants, email_etudiants, prenom_personnes FROM etudiants INNER JOIN personnes ON personnes.id_personnes = etudiants.id_etudiants ORDER BY id_etudiants DESC LIMIT 1;";
            charger(sql,dT,dA);
            return dT;
        }
        public int ChangePassword(string login, string mdp)
        {
           string sql = "UPDATE etudiants SET mdp_etudiants = '" + mdp + "' WHERE login_etudiants = '" + login + "';";
           try
           {
               MySqlCommand cmd = new MySqlCommand(sql, myConnection);
               return cmd.ExecuteNonQuery();
           }
           catch (Exception err)
           {
               return 0;
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
            string myConnectionString = "Database=bd_ppe;Data Source=192.168.152.2;User Id=ppe;Password=Azerty123;";
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
        public void charger(string requete, DataTable DT, MySqlDataAdapter DA)
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
                    charger("show tables FROM bd_ppe WHERE tables_in_bd_ppe NOT LIKE 'vue_%';", dT[0], dA[0]);
                    break;
                case "appartenir":
                    charger("select * from appartenir;", dT[1], dA[1]);
                    break;
                case "classes":
                    charger("select * from classes;", dT[2], dA[2]);
                    break;
                case "commentaires":
                    charger("select login_etudiants,rue_logements,date_commentaires,note_commentaires from commentaires as c inner join etudiants as e on e.id_etudiants=c.id_etudiants inner join logements as l on l.id_logements=c.id_logements;", dT[3], dA[3]);
                    break;
                case "composent":
                    charger("select * from composent;", dT[4], dA[4]);
                    break;
                case "correspondre":
                    charger("select * from correspondre;", dT[5], dA[5]);
                    break;
                case "dates":
                    charger("select * from dates;", dT[6], dA[6]);
                    break;
                case "equipements":
                    charger("select * from equipements;", dT[7], dA[7]);
                    break;
                case "logements":
                    charger("select * from logements;", dT[8], dA[8]);
                    break;
                case "motifs":
                    charger("select * from motifs ;", dT[9], dA[9]);
                    break;
                case "personnes":
                    charger("select * from personnes ;", dT[10], dA[10]);
                    break;
                case "photos":
                    charger("select * from photos ;", dT[11], dA[11]);
                    break;
                case "propositions":
                    charger("select * from propositions ;", dT[12], dA[12]);
                    break;
                case "villes":
                    charger("select * from villes;", dT[13], dA[13]);
                    break;
                case "etudiants":
                    charger("select p.id_personnes,p.nom_personnes,p.prenom_personnes,p.sexe_personnes,e.login_etudiants,e.mdp_etudiants,e.id_logements_etudiants,e.tel_etudiants,e.email_etudiants,e.datenaiss_etudiants from etudiants e inner join personnes p ON e.id_etudiants = p.id_personnes;", dT[14], dA[14]);
                    break;
            }
        }

        #endregion
    }
}