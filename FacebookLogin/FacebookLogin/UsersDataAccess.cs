using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace FacebookLogin
{
    public class UsersDataAccess
    {
        private SQLiteConnection database;//stocke la chaine de connexion

        private static object collisionLock = new object();//crée des verrous sur les opérations de données
        public ObservableCollection<User> Users { get; set; }// expose les donnée pour XAML

        public UsersDataAccess()
        {
            database =
              DependencyService.Get<IDatabaseConnection>().
              DbConnection();// Méthode générique qui retourne une implémentation en rapport avec l'OS => injection de dépendance
            database.CreateTable<User>();
            this.Users =
              new ObservableCollection<User>(database.Table<User>());
            // If the table is empty, initialize the collection
            if (!database.Table<User>().Any())
            {
                AddNewUser();
            }
        }
        public void AddNewUser()
        {
            this.Users.
              Add(new User
              {
                  Firstname = "Prénom",
                  Lastname = "Nom",
                  Device = "Android",
                  Gender = "Both",
                  Age = "20",
                  Latitude = "unknow",
                  Longitude = "unknow",
                  Vote="0"
              });
        }


        public int SaveCustomer(User userInstance) // met à jour l'user dans la DB 
        {
            lock (collisionLock)
            {
                if (userInstance.Id != 0)
                {
                    database.Update(userInstance);
                    return userInstance.Id;
                }
                else
                {
                    database.Insert(userInstance);
                    return userInstance.Id;
                }
            }
        }
        public User GetUser(int id)
        {
            lock (collisionLock)
            {
                return database.Table<User>().
                  FirstOrDefault(customer => customer.Id == id);
            }
        }

        public IEnumerable<User> GetLastUser()
        {
            lock (collisionLock)
            {
                return database.Query<User>(
                  "SELECT LAST(USER)  FROM Item").AsEnumerable();
            }
        }

    }
}
