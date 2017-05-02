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
        private UsersDataAccess dataAccess;
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
            Tools tool = new Tools();
            try
            {
                await location.getLatLon();
                bool sucess = location.boSuccess;
                var lat = location.latitude;
                var lon = location.longitude;
                string positionTime = tool.GetCurrentTime();
                this.dataAccess = new UsersDataAccess();
                User user = dataAccess.GetUser(1);
                user.UserLocation(lat, lon);
                user.TimeLastPosition = positionTime;
                this.dataAccess.SaveCustomer(user);// met à jour la DB Location

                //var jsonData = tool.ObjectToJson(user); --> envoie la position au seveur
                //await tool.SendData(jsonData, "urlposition");

                AlertPositionFound(lat, lon);
            }
            catch (Exception ex )
            {
                await DisplayAlert("Attention", "Nous n'avons pas pu vous Géolocaliser, veuillez verifier que vous avez bien la géolocalisation d'activée ou réssayez dans un autre endroit", "OK");
            }
        }
        public async void AlertPositionFound(string lat, string lon)
        {
            await DisplayAlert("Votre position : ", "latitude : " + lat + "  " + "longitude : " + lon, "Oui c'est bien ça !");
            await Navigation.PushModalAsync(new MusicHome());
        }
        
    }
}

