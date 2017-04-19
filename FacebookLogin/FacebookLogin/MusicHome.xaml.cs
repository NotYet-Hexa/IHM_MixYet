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
        public MusicHome()
        {
            
            InitializeComponent();

            spot = new SpotifyAPI();
            spot.TracksLoaded += Spot_TracksLoaded;
            spot.SearchTrack("Guthrie Govan");
            search.TextChanged += Search_TextChanged;
            search.Completed += Search_Completed;
            time = DateTime.Now;
            var tapImage1 = new TapGestureRecognizer();
            tapImage1.Tapped += ToReturn;
            return_im.GestureRecognizers.Add(tapImage1);

        }
        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {
            var dataMusic = (SpotifyTrack)e.Item;
            var action = await DisplayActionSheet("Message","Voter", "Partager");
            if (action=="Voter")
            {
                //User user = new User();
                //user.ListVote.Add(dataMusic); // Met à jour la liste vote music 
                await Navigation.PushModalAsync(new MusicInfo(dataMusic));
            }

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
