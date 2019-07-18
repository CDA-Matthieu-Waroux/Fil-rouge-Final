using FilRougeMW.Model.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilRougeMW.Model.Service
{
    class ServiceUtilisateur
    { /// <summary>
      /// Chargement de la liste des infos des utilisateurs
      /// </summary>
        //Création et encapsulation d'une liste de chargement pour récuperer les données en base de donnée
        private static ObservableCollection<string> listeUtilisateur = new ObservableCollection<string>();
        private static ObservableCollection<Utilisateur> listeUtilisateurUn = new ObservableCollection<Utilisateur>();
        public static ObservableCollection<Utilisateur> ListeUtilisateurUn { get => listeUtilisateurUn; set => listeUtilisateurUn = value; }
        public static ObservableCollection<string> ListeUtilisateur { get => listeUtilisateur; set => listeUtilisateur = value; }

        public static ObservableCollection<Utilisateur> ObtenirMotPasse(string NomUtilisateur)
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                connexion.Open();
                string requete = "ObtenirInfoUtilisateur";
                MySqlCommand action = new MySqlCommand(requete, connexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                // Déclaration du paramètre à utiliser pour la procédure stockée
                action.Parameters.AddWithValue("@login", NomUtilisateur);
                lecture = action.ExecuteReader();
                // MessageBox.Show("Chargement des données éffectués avec succés !","Connexion à la base de donnée",MessageBoxButton.OK,MessageBoxImage.Information);
                while (lecture.Read())
                {
                    Utilisateur utilisateur = new Utilisateur
                    {
                        NomUtilisateur = Convert.ToString(lecture["NOM_UTILISATEUR"]),
                        MotDePasseUtilisateur = Convert.ToString(lecture["MOT_PASSE_UTILISATEUR"]),
                        IdGrade = Convert.ToInt32(lecture["ID_GRADE_UTILISATEUR"]),
                    };
                    // Ajout notre Utilisateurs dans une Observable Collection.
                    listeUtilisateurUn.Add(utilisateur);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connection à la base de donnée ! " + ex);
                Environment.Exit(0);
            }
            finally
            {
                if (connexion.State == ConnectionState.Open)
                {
                    connexion.Close();
                }
            }
            return ListeUtilisateurUn;
        }
        /// <summary>
        /// Chargement de la liste des données des utilisateurs
        /// </summary>
        public static ObservableCollection<string> ChargeeDonnee()
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection Deuxiemeconnexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                Deuxiemeconnexion.Open();

                string requete = "AfficherUtilisateur";
                MySqlCommand action = new MySqlCommand(requete, Deuxiemeconnexion);

                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    string nomUtilisateur = Convert.ToString(lecture["NOM_UTILISATEUR"]);

                    listeUtilisateur.Add(nomUtilisateur);
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
            return ListeUtilisateur;
        }
        /// <summary>
        /// Chargement de la liste des utilisateurs pour ajouter un nouveau utilisateur
        /// </summary> 
        public static void Ajouterutilisateur(Utilisateur utilisateur)
        {
            string ParametreConnexionBDD = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection TroisiemeConnexion = new MySqlConnection(ParametreConnexionBDD);
            try
            {
                TroisiemeConnexion.Open();
                string requete = "AjouterUtilisateur";
                MySqlCommand action = new MySqlCommand(requete, TroisiemeConnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                action.Parameters.AddWithValue("@PASSWORDS", utilisateur.MotDePasseUtilisateur);
                action.Parameters.AddWithValue("@NOM", utilisateur.NomUtilisateur);
                action.Parameters.AddWithValue("@GRADE", utilisateur.NomGrade);
                action.Parameters.AddWithValue("@CLEEMPLOYE", utilisateur.Cleemploye);
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
        // Pour le cryptage du mot de passe
        public static String EncryypterSha512(string valeur)
        {
            StringBuilder resultat = new StringBuilder();
            using (SHA512 hash = SHA512Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] convertion = hash.ComputeHash(enc.GetBytes(valeur));

                foreach (Byte b in convertion)
                    resultat.Append(b.ToString("x2"));
            }
            return resultat.ToString();
        }
        /// <summary>
        /// Chargement de la liste des utilisateurs avec leurs clé employé
        /// </summary>
        public static ObservableCollection<Utilisateur> ObtenirUtilisateur()
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection Quatriemeconnexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                Quatriemeconnexion.Open();
                string requete = "ObtenirUtilisateur";
                MySqlCommand action = new MySqlCommand(requete, Quatriemeconnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                lecture = action.ExecuteReader();
                // MessageBox.Show("Chargement des données éffectués avec succés !","Connexion à la base de donnée",MessageBoxButton.OK,MessageBoxImage.Information);
                while (lecture.Read())
                {
                    Utilisateur utilisateur = new Utilisateur
                    {
                        NomUtilisateur = Convert.ToString(lecture["NOM_UTILISATEUR"]),
                        Cleemploye = Convert.ToString(lecture["CLE_EMPLOYE"])
                    };
                    // Ajout notre Utilisateurs dans une Observable Collection.
                    listeUtilisateurUn.Add(utilisateur);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connection à la base de donnée ! " + ex);

            }
            finally
            {
                if (Quatriemeconnexion.State == ConnectionState.Open)
                {
                    Quatriemeconnexion.Close();
                }
            }
            return ListeUtilisateurUn;
        }
    }
}

