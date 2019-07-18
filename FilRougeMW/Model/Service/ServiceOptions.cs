using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using FilRougeMW.Model.Class;

namespace FilRougeMW.Model.Service
{
    class ServiceOptions : ObservableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Chargement de la liste Année dans les ComboBox
        /// </summary>
        
        //Création et encapsulation d'une liste de chargement pour récuperer les données en base de donnée
        private static List<string> listeOptions = new List<string>();
        public static List<string> ListeOptions { get => listeOptions; set => listeOptions = value; }

        // Méthode pour le chargement de la liste des options
        public static List<string> ChargeeDonneeOptions()
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;Pwd=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                connexion.Open();
                string requete = "ObtenirOptions";
                MySqlCommand action = new MySqlCommand(requete, connexion);
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                   string options = Convert.ToString(lecture["NOM_OPTION"]);
                    listeOptions.Add(options);
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
            return ListeOptions;
        }
        /// <summary>
        /// Chargement de la liste Année dans les ComboBox
        /// </summary>
        public static List<string> ChargeeDonneeOptionsByID(int? identifiant)
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection Deuxiemeconnexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                Deuxiemeconnexion.Open();
                string requete = "AfficherOptionsByVoiture";
                MySqlCommand action = new MySqlCommand(requete, Deuxiemeconnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Déclaration du paramètre à utiliser pour la procédure stockée
                action.Parameters.AddWithValue("@OPTIONS", SqlDbType.Int).Value = identifiant;
                action.ExecuteNonQuery();
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    string option = Convert.ToString(lecture["NOM_OPTION"]);

                    listeOptions.Add(option);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connection à la base de donnée ! " + ex);
            }
            finally
            {
                if (Deuxiemeconnexion.State == ConnectionState.Open)
                {
                    Deuxiemeconnexion.Close();
                }
            }
            return ListeOptions;
        }
        /// <summary>
        /// Chargement de la liste des options pour les supprimer lors de l'ouverture de la fenêtre et/ou pour la suppression de la voiture
        /// </summary>
        public static void SupprimerOptions(int? idvoiture)
        {
            string ParametreConnexionBDD = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection TroisiemeConnexion = new MySqlConnection(ParametreConnexionBDD);
            try
            {
                TroisiemeConnexion.Open();
                string requete = "SupprimerOptionsByIdVoiture";
                MySqlCommand action = new MySqlCommand(requete, TroisiemeConnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                // Déclaration du paramètre à utiliser pour la procédure stockée
                action.Parameters.AddWithValue("@IdVoiture", SqlDbType.Int).Value = idvoiture;
                action.ExecuteNonQuery();
            }
            catch (Exception erreur)
            {
                MessageBox.Show("Erreur de connexion à la base de donnée !\r\nVerifiez la base de donnée...\r\n" + erreur.ToString(), "DATABASE ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (TroisiemeConnexion.State == ConnectionState.Open)
                {
                    TroisiemeConnexion.Close();
                }
            }
        }
        /// <summary>
        /// Chargement de la liste des options pour les ajouter à une voiture lors de la modification
        /// </summary>
        public static void AjouterOptionsByVoiture(int? idvoiture , string option)
        {
            string ParametreConnexionBDD = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";

            MySqlConnection QuatriemeConnexion = new MySqlConnection(ParametreConnexionBDD);
            try
            {
                QuatriemeConnexion.Open();
                string requete = "AjouterOptionByIdVoiture";
                MySqlCommand action = new MySqlCommand(requete,QuatriemeConnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                action.Parameters.AddWithValue("@IdVoiture", SqlDbType.Int).Value = idvoiture;
                action.Parameters.AddWithValue("@Valeur",  option);
                action.ExecuteNonQuery();
            }
            catch (Exception erreur)
            {
                MessageBox.Show("Erreur de connexion à la base de donnée !\r\nVerifiez la base de donnée...\r\n" + erreur.ToString(), "DATABASE ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (QuatriemeConnexion.State == ConnectionState.Open)
                {
                    QuatriemeConnexion.Close();
                }
            }
        }
        /// <summary>
        /// Chargement de la liste des options pour ajouter des nouvelles options pour une nouvelle voiture
        /// </summary>
        public static void AjouterOptions(string option)
        {
            string ParametreConnexionBDD = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";

            MySqlConnection CinquiemeConnexion = new MySqlConnection(ParametreConnexionBDD);
            try
            {
                CinquiemeConnexion.Open();
                string requete = "AjouterOption";
                MySqlCommand action = new MySqlCommand(requete, CinquiemeConnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                action.Parameters.AddWithValue("@Options", option);
                action.ExecuteNonQuery();
            }
            catch (Exception erreur)
            {
                MessageBox.Show("Erreur de connexion à la base de donnée !\r\nVerifiez la base de donnée...\r\n" + erreur.ToString(), "DATABASE ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (CinquiemeConnexion.State == ConnectionState.Open)
                {
                    CinquiemeConnexion.Close();
                }
            }
        }
    }


}
