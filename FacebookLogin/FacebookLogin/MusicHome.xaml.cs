using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace FacebookLogin
{
    public partial class MusicHome : ContentPage
    {
        SpotifyAPI spot;
        DateTime time;
        private UsersDataAccess dataAccess;
        SpotifyTrack trackvoted;
        public MusicHome()
        { 
            InitializeComponent();

            spot = new SpotifyAPI();
            var tapImage1 = new TapGestureRecognizer();

            spot.TracksLoaded += Spot_TracksLoaded;
            spot.SearchTrack("Guthrie Govan");

            search.TextChanged += Search_TextChanged;
            search.Completed += Search_Completed;

            time = DateTime.Now;
            tapImage1.Tapped += ToReturn;
            return_im.GestureRecognizers.Add(tapImage1);

        }
        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {
            var dataMusic = (SpotifyTrack)e.Item;
            var action = await DisplayActionSheet("Que voulez-vous faire ?","Annuler",null,"Infos", "Voter", "Partager");

            if (action == "Voter")
            {
                
                //User user = dataAccess.GetUser(1);
                User user = new User();
                if (user.Vote=="0")//on vérifie si l'user à le droit de voter
                {
                    var answer = await DisplayAlert("Attention",
                        "Vous allez voter pour : " + "\n" + dataMusic.Name + "\n" + "de " + dataMusic.Artiste + ".",
                        "Annuler", "Oui c'est bien ça");
                    if (answer)
                    {
                        this.dataAccess = new UsersDataAccess();
                        user.UserVote(dataMusic); // Met à jour la liste vote music 
                        user.Vote = "1";
                        this.dataAccess.SaveCustomer(user);
                        await DisplayAlert("Information", "Votre vote a bien été pris en compte", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Attention", "Vous avez déjà voté veuillez attendre un moment", "OK");
                }
            }
            else if (action=="Infos")
            {
                await Navigation.PushModalAsync(new MusicInfo(dataMusic));
            }
        }
        

        private async void shareItem( object sender , EventArgs e )
        {
            //TODO
        }
        private void Search_Completed(object sender, EventArgs e)
        {
            spot.SearchTrack((sender as Entry).Text);
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (time.AddSeconds(1) < DateTime.Now)
            {
                spot.SearchTrack(e.NewTextValue);
                time = DateTime.Now;
            }
        }
        async void ToReturn(object sender, EventArgs e)

        {
            await Navigation.PopModalAsync();
        }

        private void Spot_TracksLoaded(object sender, SpotifyEvent e)
        {
            Device.BeginInvokeOnMainThread(() => list.ItemsSource = e.tracks);
        }
    }
}
