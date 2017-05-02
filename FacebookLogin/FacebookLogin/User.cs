
using System.Collections.Generic;
using SQLite;
using System.ComponentModel;
namespace FacebookLogin
{
    [Table("Users")]
    public  class User : INotifyPropertyChanged
    {
        private string timeLastPosition,timeLastVote;
        private string vote = "0";
        private string firstname,lastname,age,gender,device; // Attribut pour les infos générales
        private string latitude, longitude; // Location de l'utilisateur
        private string lastMusiqueVote; // Pour la DB 
        private SpotifyTrack musicVote;

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

        public string LastMusiqueVote
        {
            get
            {
                return lastMusiqueVote;
            }
            set
            {
                this.lastMusiqueVote = value;
                OnPropertyChanged(nameof(LastMusiqueVote));
            }
        }
        
        [NotNull]
        public string Vote
        {
            get
            {
                return vote;
            }
            set
            {
                this.vote = value;
                OnPropertyChanged(nameof(Vote));
            }
        }

        public string TimeLastPosition
        {
            get
            {
                return timeLastPosition;
            }
            set
            {
                this.timeLastPosition = value;
                OnPropertyChanged(nameof(TimeLastPosition));
            }
        }
        public string TimeLastVote
        {
            get
            {
                return timeLastVote;
            }
            set
            {
                this.timeLastVote = value;
                OnPropertyChanged(nameof(TimeLastVote));
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
        
        public void UserVote(SpotifyTrack musicVoted)
        {
            this.lastMusiqueVote = musicVoted.Name + "\n" + " de : " + musicVoted.Artiste;
            this.musicVote = musicVoted;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }

    
}
