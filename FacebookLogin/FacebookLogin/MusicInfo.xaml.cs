﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FacebookLogin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MusicInfo : ContentPage
	{
		public MusicInfo ( SpotifyTrack musique)
		{
            BindingContext = musique;
			InitializeComponent ();
            var tapImage1 = new TapGestureRecognizer();
            tapImage1.Tapped += ToReturn;
            return_im.GestureRecognizers.Add(tapImage1);

        }
        async void ToReturn(object sender, EventArgs e)

        {
            await Navigation.PopModalAsync();
        }
    }
}
