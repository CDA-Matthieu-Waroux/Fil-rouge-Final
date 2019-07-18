using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FilRougeMW.Model.Class;
using FilRougeMW.ViewModel;
using MySql.Data.MySqlClient;



namespace FilRougeMW.Model.Service
{
    class ServiceVoiture : ObservableObject,INotifyPropertyChanged
    {
        /// <summary>
        /// Chargement de la liste voiture de la basse de donnée dans le DataGrid
        /// </summary>

        // on crée une deuxième liste Voiture en observable Collection pour permettre d'actualiser la liste quand des élèments sont ajoutés, modifié ou supprimer 
        //=> L'ObservableCollection permet de rendre dynamique une liste
        private static ObservableCollection<Voiture> listeVoiture = new ObservableCollection<Voiture>();
        private static ObservableCollection<Voiture> listeVoiture2 = new ObservableCollection<Voiture>();

        // on encapsule les listes en utilisant la propriété
        public static ObservableCollection<Voiture> ListeVoiture { get => listeVoiture; set => listeVoiture = value; }     
        public static ObservableCollection<Voiture> ListeVoiture2 { get => listeVoiture2; set => listeVoiture2 = value; }

        // on crée la méthode pour charger les données Marque
        public static ObservableCollection<Voiture> ChargeeDonneeVoiture()
        {
            // on crée un string pour la connection au serveur
            // la classe MySqlConnection nous permet de nous connecter au serveur qui stocke notre base de donnée
            // la classe MySqlDataReader nous permet de récuperer les données de la basse de donnée
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;Pwd=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;

            // on crée une boucle try/Catch/Finally pour gerer les erreur qui peuvent se produire lors de l'exécution
            try
            {
                // ouvre une connection de la base de donnée
                connexion.Open();
                //on appelle la procédure stocké
                string requete = "ObtenirInfosVoiture";
                // la classe MySqlCommand action permet à la procédure stocké de s'executer
                MySqlCommand action = new MySqlCommand(requete, connexion);
                // la class ExecuteReader envoi la procedure stockée à une connection et génére une récuprération de ces données
                lecture = action.ExecuteReader();

                // boucle While pour que la console "MySqlDataReader.Read" ajoute les éléments de la table
                // vrai s'il existe des lignes suplémentaire; sinon faux
                while (lecture.Read())
                {
                    //on crée une classe Voiture dans la boucle et on l'instancie
                    Voiture voiture = new Voiture
                    {
                        IdVoiture = Convert.ToInt32(lecture["ID_VOITURE"]),                        
                        Puissance = Convert.ToInt32(lecture["PUISSANCE"]),
                        PrixAchat = Convert.ToInt32(lecture["PRIX_ACHAT"]),
                        CoteArgus = Convert.ToInt32(lecture["COTE_ARGUS"]),                       
                        NombrePlace = Convert.ToInt32(lecture["NOMBRE_PLACE"]),
                        Carburant = Convert.ToString(lecture["NOM_CARBURANT"]),
                        Modele = Convert.ToString(lecture["NOM_MODELE"]),
                        Marque = Convert.ToString(lecture["NOM_MARQUE"]),
                        Couleur = Convert.ToString(lecture["NOM_COULEUR"]),
                        AnneeFabrication=Convert.ToInt32(lecture["ANNEE"]), 
                        Categorie = Convert.ToString(lecture["NOM_CATEGORIE"]),
                        Photo = Convert.ToString(lecture["PHOTO"])
                    };
                    // on ajoute chaque voiture dans un observable Collection
                    ListeVoiture.Add(voiture);
                }
            }
            // la classe "Exception" permet d'iniatiliser une nouvelle instance de la classe avec un message d'erreur spécifié
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connection à la base de donnée ! " + ex);
            }
            finally
            {
                //la propriéte connexion.State nous indique l'état de la connection lors de la dernière opération réseau sur la connection
                // si la connection est ouverte donc on ferme la connection à la base de donnée
                if (connexion.State == ConnectionState.Open)
                {
                    // methode pour fermer la connection
                    connexion.Close();
                }
            }
            //retour à la listMarque de l'Observable collection
            return ListeVoiture;
        }
        /// <summary>
        /// Chargement de les éléments de la voiture par son Id
        /// </summary>
        public static ObservableCollection<Voiture> ChargeeDonneeVoitureByID(int? indice)
        {
            
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection secondeconnexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;

            try
            {
                secondeconnexion.Open();
                string requete = "AfficherVoitureByID";
                MySqlCommand action = new MySqlCommand(requete, secondeconnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                action.Parameters.AddWithValue("@VOITURE", SqlDbType.Int).Value = indice;
                action.ExecuteNonQuery();
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    Voiture voiture = new Voiture
                    {
                        IdVoiture = Convert.ToInt32(lecture["ID_VOITURE"]),                       
                        Puissance = Convert.ToInt32(lecture["PUISSANCE"]),
                        PrixAchat = Convert.ToInt32(lecture["PRIX_ACHAT"]),
                        CoteArgus = Convert.ToInt32(lecture["COTE_ARGUS"]),
                        NombrePlace = Convert.ToInt32(lecture["NOMBRE_PLACE"]),
                        Carburant = Convert.ToString(lecture["NOM_CARBURANT"]),
                        Modele = Convert.ToString(lecture["NOM_MODELE"]),
                        Marque = Convert.ToString(lecture["NOM_MARQUE"]),
                        Couleur = Convert.ToString(lecture["NOM_COULEUR"]),
                        AnneeFabrication = Convert.ToInt32(lecture["ANNEE"]),
                        Categorie = Convert.ToString(lecture["NOM_CATEGORIE"])
                    };
                    ListeVoiture2.Add(voiture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connection à la base de donnée ! " + ex);
            }
            finally
            {
                if (secondeconnexion.State == ConnectionState.Open)
                {
                    secondeconnexion.Close();
                }
            }
            return ListeVoiture2;
        }
        /// <summary>
        /// Chargement de la liste voiture par son Id pour supprimer une voiture
        /// </summary>
        public static void SupprimerVoiture(int? identifiant)
        {
            string ParametreConnexionBDD = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection TroisiemeConnexion = new MySqlConnection(ParametreConnexionBDD);

            try
            {
                TroisiemeConnexion.Open();
                string requete = "SupprimerVoitureByID";
                // la classe MySqlCommand action nous permet à la procédure stocké de s'executer 
                //on declare une commande et on l'instencie
                MySqlCommand action = new MySqlCommand(requete, TroisiemeConnexion)
                {
                    // on définit les valeurs et spécifie la comment la commande doit être interprétée
                    CommandType = CommandType.StoredProcedure
                };
                // la méthode Parameters.AddWithValue (SqlParameterCollection.AddWithValue) ajoute une valeur à la fin de l'objet SqlParameterCollection
                // ("@MARQUE", marque) = (string ou int de la procédure stockée, objet de la connection 
                // Déclaration du paramètre à utiliser pour la procédure stockée
                action.Parameters.AddWithValue("@Valeur", SqlDbType.Int).Value = identifiant;
                // la méthode ExecuteNonQuery nous permet d'exécuter une instruction sur la connexion et retourne le nombre de ligne affectées
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
        /// Chargement de la liste voiture par son Id pour modifier une voiture
        /// </summary>
        public static void ModifierVoiture(Voiture voiture)
        {

            string ParametreConnexionBDD = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection QuatriemeConnexion = new MySqlConnection(ParametreConnexionBDD);

            try
            {
                QuatriemeConnexion.Open();
                string requete = "ModifierVoitureById";

                MySqlCommand action = new MySqlCommand(requete, QuatriemeConnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                // Déclaration du paramètre à utiliser pour la procédure stockée
                action.Parameters.AddWithValue("@Modele", voiture.Modele) ;
                action.Parameters.AddWithValue("@Categorie", voiture.Categorie);
                action.Parameters.AddWithValue("@Puissance",  voiture.Puissance);
                action.Parameters.AddWithValue("@Prix",  voiture.PrixAchat);
                action.Parameters.AddWithValue("@Place", voiture.NombrePlace);
                action.Parameters.AddWithValue("@Annee",  voiture.AnneeFabrication);
                action.Parameters.AddWithValue("@Carburant", voiture.Carburant);
                action.Parameters.AddWithValue("@Couleur",  voiture.Couleur);
                action.Parameters.AddWithValue("@CoteArgus", voiture.CoteArgus);
                action.Parameters.AddWithValue("@IdVoiture", voiture.IdVoiture);
                action.Parameters.AddWithValue("@Photo", voiture.Photo);
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
        /// Chargement de la liste option à supprimer par l'Id de la voiture 
        /// </summary>
        public static void SupprimerOptionsByVoiture(int idVoiture)
        {
            string ParametreConnexionBDD = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection CinquièmeConnexion = new MySqlConnection(ParametreConnexionBDD);

            try
            {
                CinquièmeConnexion.Open();
                string requete = "SupprimerOptionsByIdVoiture";
                MySqlCommand action = new MySqlCommand(requete, CinquièmeConnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                // Déclaration du paramètre à utiliser pour la procédure stockée
                action.Parameters.AddWithValue("@IdVoiture", SqlDbType.Int).Value = idVoiture;
                action.ExecuteNonQuery();
            }
            catch (Exception erreur)
            {
                MessageBox.Show("Erreur de connexion à la base de donnée !\r\nVerifiez la base de donnée...\r\n" + erreur.ToString(), "DATABASE ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (CinquièmeConnexion.State == ConnectionState.Open)
                {
                    CinquièmeConnexion.Close();
                }
            }
        }
        /// <summary>
        /// Chargement de la liste voiture pour ajouter une voiture
        /// </summary>
        public static void AjouterVoiture(Voiture voiture)
        {
            string ParametreConnexionBDD = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";

            MySqlConnection CinquiemeConnexion = new MySqlConnection(ParametreConnexionBDD);
            try
            {
                CinquiemeConnexion.Open();
                string requete = "AjouterVoiture";
                MySqlCommand action = new MySqlCommand(requete, CinquiemeConnexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                action.Parameters.AddWithValue("@ANNEE", voiture.AnneeFabrication);
                action.Parameters.AddWithValue("@CARBURANT", voiture.Carburant);
                action.Parameters.AddWithValue("@CATEGORIE", voiture.Categorie);
                action.Parameters.AddWithValue("@COTE", voiture.CoteArgus);
                action.Parameters.AddWithValue("@COULEUR", voiture.Couleur);
                action.Parameters.AddWithValue("@MODELE", voiture.Modele);
                action.Parameters.AddWithValue("@PLACE", voiture.NombrePlace);
                action.Parameters.AddWithValue("@PRIX", voiture.PrixAchat);
                action.Parameters.AddWithValue("@PUISSANCE", voiture.Puissance);
                action.Parameters.AddWithValue("@PHOTO", voiture.Photo);
                action.ExecuteNonQuery();
            }
            catch (Exception erreur)
            {
                MessageBox.Show("Erreur ! \n Veuillez vérifier vos valeurs !" ,"Erreur" , MessageBoxButton.OK, MessageBoxImage.Error);
                AjouterViewModel.Boule = false;
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
