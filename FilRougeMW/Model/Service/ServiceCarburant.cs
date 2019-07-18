using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FilRougeMW.Model.Class;

namespace FilRougeMW.Model.Service
{
    class ServiceCarburant
    {
        /// <summary>
        /// Chargement de la liste Caburant dans les ComboBox
        /// </summary>
        //Création et encapsulation d'une liste de chargement pour récuperer les données en base de donnée
        // on encapsule les listes en utilisant la propriété
        private static List<string> listeCarburant = new List<string>();
        public static List<string> ListeCarburant { get => listeCarburant; set => listeCarburant = value; }
        // méthode pour charger la liste des carburant
        public static List<string> ChargeeDonneeCarburant()
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                connexion.Open();
                string requete = "ObtenirCarburant";
                MySqlCommand action = new MySqlCommand(requete, connexion);
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    string carburant = Convert.ToString(lecture["NOM_CARBURANT"]);
                   
                    listeCarburant.Add(carburant);
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



            return ListeCarburant;
        }
    }
}
