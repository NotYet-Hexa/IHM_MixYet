﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace FacebookLogin
{
	
	public partial class Login : ContentPage
	{
        public bool connection = false;
        public string ClientId = "837022399788381";
        private UsersDataAccess dataAccess;
        public Login ()
		{
			InitializeComponent ();
            var buttonLogin = this.FindByName<Button>("buttonLogin");
            buttonLogin.Clicked += OnButtonClicked;

        }


        void OnButtonClicked(object sender, EventArgs e)
        {
            FaceLogin();

        }
        private void FaceLogin()
        {
            var apiRequest =
                   "https://www.facebook.com/v2.8/dialog/oauth?client_id="
                   + ClientId
                   + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;
            Content = webView;
        }
        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var accessToken = ExtractAccessTokenFromUrl(e.Url);
            if (accessToken != "")
            {
                await GetFacebookProfileAsync(accessToken);
            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token", "");


                if (Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.Windows)
                {
                    at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));
                return accessToken;

            }
            return string.Empty;

        }

        private async Task GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/"
                + "?fields=id,name,picture,age_range,devices,gender"
                + "&access_token"
                + accessToken;

            var httpClient = new HttpClient();

            connection = true;
            var userJson =  await httpClient.GetStringAsync(requestUrl);
            FacebookProfil fbprofil = new FacebookProfil(JObject.Parse(userJson));

            this.dataAccess = new UsersDataAccess();
            User user = new User();
            user.UserInfo(fbprofil); // Met à jour la DB profil 
            user.Id = 1;
            this.dataAccess.SaveCustomer(user);
            await Navigation.PushModalAsync(new Logged(fbprofil));
        }
    }
}

