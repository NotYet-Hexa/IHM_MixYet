using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FacebookLogin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DisplayUserData : ContentPage
	{
        private UsersDataAccess dataAccess;
		public DisplayUserData ()
		{
			InitializeComponent ();
            this.dataAccess = new UsersDataAccess();
            var tapImage1 = new TapGestureRecognizer();
            tapImage1.Tapped += ToReturn;
            return_im.GestureRecognizers.Add(tapImage1);
            buttonLogin.Clicked += OnButtonClicked;

        }


        void OnButtonClicked(object sender, EventArgs e)
        {

            Login log = new Login();
            log.FaceLogin();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // The instance of CustomersDataAccess
            // is the data binding source
            this.BindingContext = this.dataAccess;
        }
        async void ToReturn(object sender, EventArgs e)

        {
            await Navigation.PopModalAsync();
        }
    }
}
