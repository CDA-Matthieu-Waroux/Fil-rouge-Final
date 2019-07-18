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
    class ServiceCategorie
    {
        /// <summary>
        /// Chargement de la liste Catégorie dans les ComboBox
        /// </summary>
        
        //Création et encapsulation d'une liste de chargement pour récuperer les données en base de donnée
        // on encapsule les listes en utilisant la propriété
        private static List<string> listeCategorie = new List<string>();
        public static List<string> ListeCategorie { get => listeCategorie; set => listeCategorie = value; }
        // Méthode pour Charger la liste des catégories
        public static List<string> ChargeeDonneeCategorie()
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none"; 
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                connexion.Open();
                string requete = "ObtenirCategorie";
                MySqlCommand action = new MySqlCommand(requete, connexion);
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    string categorie = Convert.ToString(lecture["NOM_CATEGORIE"]);                    
                    listeCategorie.Add(categorie);
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
            return ListeCategorie;
        }
    }
}
