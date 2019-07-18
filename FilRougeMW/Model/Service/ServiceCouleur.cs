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
    class ServiceCouleur
    {
        /// <summary>
        /// Chargement de la liste Année dans les ComboBox
        /// </summary>
        
        //Création et encapsulation d'une liste de chargement pour récuperer les données en base de donnée
        
        private static List<string> listeCouleur = new List<string>();
        public static List<string> ListeCouleur { get => listeCouleur; set => listeCouleur = value; }
        // Méthode pour charger la liste de couleur
        public static List<string>ChargeeDonneeCouleur()
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none"; 
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                connexion.Open();
                string requete = "ObtenirCouleur";
                MySqlCommand action = new MySqlCommand(requete, connexion);
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    string couleur =Convert.ToString(lecture["NOM_COULEUR"]);                                                            
                    listeCouleur.Add(couleur);
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
            return ListeCouleur;
        }
    }
}
