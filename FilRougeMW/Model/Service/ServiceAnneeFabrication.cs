using FilRougeMW.Model.Class;
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
    class ServiceAnneeFabrication
    {
        /// <summary>
        /// Chargement de la liste Année dans les ComboBox
        /// </summary>
        //Création et encapsulation d'une liste de chargement pour récuperer les données en base de donnée
        // on encapsule les listes en utilisant la propriété
        private static List<int> listeAnneFabrication = new List<int>();
        public static List<int> ListeAnneFabrication { get => listeAnneFabrication; set => listeAnneFabrication = value; }

        //Méthode pour charger les données de la table Année       
        public static List<int> ChargeeDonneeAnneeFabrication()
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                connexion.Open();
                string requete = "ObtenirAnnee";
                MySqlCommand action = new MySqlCommand(requete, connexion);
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    int annee = Convert.ToInt32(lecture["ANNEE"]);
                   
                    listeAnneFabrication.Add(annee);
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
            return ListeAnneFabrication;
        }
    }
}
