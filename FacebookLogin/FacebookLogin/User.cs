
using System.Collections.Generic;
using SQLite;
using System.ComponentModel;

namespace FacebookLogin
{
    [Table("Users")]
    public  class User : INotifyPropertyChanged
    {
        private string firstname,lastname,age,gender,device; // Attribut pour les infos générales
        private string latitude, longitude; // Location de l'utilisateur
        private List<SpotifyTrack> listeVote = new List<SpotifyTrack>(); // liste des votes pour éventuellement proposer de meilleures musiques

        public int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public List<SpotifyTrack> ListVote
        {
            get
            {
                return listeVote;
            }
            set
            {
                this.listeVote = value;
                OnPropertyChanged(nameof(ListVote));
            }
        }

        public string Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                this.latitude = value;
                OnPropertyChanged(nameof(Latitude));
            }
        }
        public string Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                this.longitude = value;
                OnPropertyChanged(nameof(Longitude));
            }
        }


        [NotNull]
        public string Device
        {
            get
            {
                return device;
            }
            set
            {
                this.device = value;
                OnPropertyChanged(nameof(Device));
            }
        }


        [NotNull]
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                this.gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        [NotNull]
        public string Age
        {
            get
            {
                return age;
            }
            set
            {
                this.age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        [NotNull]
        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                this.lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        [NotNull]
        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                this.firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }








        public User() 
        {

        }

        public void UserInfo(FacebookProfil fbprofil)
        {
            this.age = fbprofil.Age_Range;
            var name = fbprofil.Name.Split(' ');
            this.firstname = (string)name.GetValue(0);
            this.lastname = (string)name.GetValue(name.Length - 1);
            this.gender = fbprofil.Gender;
            this.device = fbprofil.Devices;
        }

        public void UserLocation ( string Latitude, string Longitude)
        {
            this.latitude = Latitude;
            this.longitude = Longitude;
        }

        public void UserVote(SpotifyTrack VotedMusic)
        {
            this.listeVote.Add(VotedMusic);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }

    
}
