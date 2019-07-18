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
    class ServiceModele
    {

        private static ObservableCollection<Modele> listemodele = new ObservableCollection<Modele>();

        internal static ObservableCollection<Modele> Listemodele { get => listemodele; set => listemodele = value; }

        public static ObservableCollection<Modele> ChargeeDonneeModele()
        {

            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection connexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;
            try
            {
                connexion.Open();
                string requete = "ObtenirModele";
                MySqlCommand action = new MySqlCommand(requete, connexion);
                lecture = action.ExecuteReader();
                while (lecture.Read())
                {
                    Modele modele = new Modele()
                    {
                       NomModele= Convert.ToString(lecture["NOM_MODELE"]),
                       IdMarque = Convert.ToInt32(lecture["ID_MARQUE"])
                    };
                    listemodele.Add(modele);
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
            return Listemodele;
        }

        public static ObservableCollection<Modele> ChargerModeleByMarque(string marque)
        {
            string parametreConnexion = "Server=145.239.217.105;Port=3306;Database=filrouge;uid=MatMoh;password=NousAvonsToutRecommencezCarIlsM'ontSoulé;SslMode=none";
            MySqlConnection secondeconnexion = new MySqlConnection(parametreConnexion);
            MySqlDataReader lecture = null;

            try
            {
                secondeconnexion.Open();
                string requete = "ObtenirModeleByMarque";
                // la classe MySqlCommand action nous permet à la procédure stocké de s'executer 
                //on declare une commande et on l'instencie
                MySqlCommand action = new MySqlCommand(requete, secondeconnexion)
                {
                    // on définit les valeurs et spécifie la comment la commande doit être interprétée
                    CommandType = CommandType.StoredProcedure
                };
                // la méthode Parameters.AddWithValue (SqlParameterCollection.AddWithValue) ajoute une valeur à la fin de l'objet SqlParameterCollection
                // ("@MARQUE", marque) = (string ou int de la procédure stockée, objet de la connection 
                action.Parameters.AddWithValue("@MARQUE", marque);
                // la méthode ExecuteNonQuery nous permet d'exécuter une instruction sur la connexion et retourne le nombre de ligne affectées
                action.ExecuteNonQuery();
                // la class ExecuteReader envoi la procedure stockée à une connection et génére une récuprération de ces donnée
                lecture = action.ExecuteReader();
                // on efface la liste modèle avant l'exécution de la boucle While pour éviter d'avoir les modeles des marques selectionné précedement
                listemodele.Clear();
                while (lecture.Read())
                {
                    Modele modele = new Modele()
                    {
                      NomModele = Convert.ToString(lecture["NOM_MODELE"])
                    };
                    listemodele.Add(modele);
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

            return Listemodele;
        }










    }
}
