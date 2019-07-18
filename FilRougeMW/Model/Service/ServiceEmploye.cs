using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilRougeMW.Model.Service
{
    class ServiceEmploye
    {
        /// <summary>
        /// Chargement de la liste des employés
        /// </summary>
        
            //Création et encapsulation d'une liste de chargement pour récuperer les données en base de donnée
        private static ObservableCollection<string> listeClefEmploye = new ObservableCollection<string>();
        public static ObservableCollection<string> ListeClefEmploye { get => listeClefEmploye; set => listeClefEmploye = value; }
        // méthode pour le chargement des clé des employé 
        public static ObservableCollection<string> ObtenirClefEmploye()
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            ListeClefEmploye.Clear();
            try
            {
                connexion.Open();
                string requete = "ObtenirCleEmploye";
                MySqlCommand action = new MySqlCommand(requete, connexion);
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    string clefEmploye = Convert.ToString(lecture["CLE_EMPLOYE"]);
                    listeClefEmploye.Add(clefEmploye);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connection à la base de donnée ! " + ex);
            }
            finally
            {
                if (connexion.State == ConnectionState.Open)
                {
                    connexion.Close();
                }
            }
            return ListeClefEmploye;
        }
    }
}
