using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Gcm;
using Android.Gms.Common;
using Android.Content;

namespace FacebookLogin.Droid
{

    [Activity(Label = "FacebookLogin", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private bool playservice;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.playservice=IsPlayServicesAvailable();
            if (this.playservice)
            {
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }
            LoadApplication(new FacebookLogin.App());
            
        }
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                //msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                {
                    return false;
                }
                else
                {
                    Finish();
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}