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
    class ServiceMarque
    {
        /// <summary>
        /// Chargement de la liste marque de la basse de donnée dans un combobox
        /// </summary>

        // on crée une première liste marque  en liste string 
        // on crée une deuxième liste marque en observable Collection pour permettre d'actualiser la liste quand des élèments sont ajoutés, modifié ou supprimer 
        //=> L'ObservableCollection permet de rendre dynamique une liste
        private static  List<string> listeMarque = new List<string>();
        private static ObservableCollection<Marque> listeMarque2= new ObservableCollection<Marque>();

        // on encapsule les listes en utilisant la propriété
        public static  List<string> ListeMarque { get => listeMarque; set => listeMarque = value; }
        public static ObservableCollection<Marque> ListeMarque2 { get => listeMarque2; set => listeMarque2 = value; }

        // on crée la méthode pour charger les données Marque
        public static ObservableCollection<Marque> ChargeeDonneeMarque()
        {
            // on crée un string pour la connection au serveur
            // la classe MySqlConnection nous permet de nous connecter au serveur qui stocke notre base de donnée
            // la classe MySqlDataReader nous permet de récuperer les données de la basse de donnée
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            
            // on crée une boucle try/Catch/Finally pour gerer les erreur qui peuvent se produire lors de l'exécution
            try
            {
                // ouvre une connection de la base de donnée
                connexion.Open();
                //on appelle la procédure stocké
                string requete = "ObtenirMarque";
                // la classe MySqlCommand action permet à la procédure stocké de s'executer
                MySqlCommand action = new MySqlCommand(requete, connexion);
                // la class ExecuteReader envoi la procedure stockée à une connection et génére une récuprération de ces données
                lecture = action.ExecuteReader();

                // boucle While pour que la console "MySqlDataReader.Read" ajoute les éléments de la table
                // vrai s'il existe des lignes suplémentaire; sinon faux
                while (lecture.Read())
                {
                    //on crée une classe marque dans la boucle et on l'instancie
                    Marque marque = new Marque()
                    {
                        NomMarque = Convert.ToString(lecture["NOM_MARQUE"]),
                        IdMarque=Convert.ToInt32(lecture["ID_MARQUE"])
                    };
                    // on ajoute chaque marque dans un observable Collection
                    listeMarque2.Add(marque);
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
            return ListeMarque2;
        }



        

    }
}
