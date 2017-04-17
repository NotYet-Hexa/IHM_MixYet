using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FacebookLogin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Logged : ContentPage
	{

        public string maposition;
        public Logged (FacebookProfil fbprofil)
		{
            
            InitializeComponent();
            BindingContext = fbprofil;
            var buttonMusic = this.FindByName<Button>("buttonMusic");
            buttonMusic.Clicked += AlertPosition;
        }

        public async void ButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MusicHome());
        }
        
        public async void AlertPosition(object sender, EventArgs e)
        {
            // handle the tap  
            await DisplayAlert("Attention","Nous allons vous Géolocaliser","OK");
            Geoloc location = new Geoloc();
            await  location.getLatLon();
            var lat = location.latitude;
            var lon = location.longitude;
            User user = new User();
            user.UserLocation(lat, lon); // met à jour la DB Location
            AlertPositionFound(lat, lon);
        }
        public async void AlertPositionFound(string lat, string lon)
        {
            await DisplayAlert("Votre position : ", "latitude : " + lat + "  " + "longitude : " + lon, "Oui c'est bien ça !");
            await Navigation.PushModalAsync(new MusicHome());
        }
        
    }
}

