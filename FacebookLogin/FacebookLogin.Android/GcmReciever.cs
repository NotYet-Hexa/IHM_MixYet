using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;

namespace Android.Gms.Gmc
{
    [BroadcastReceiver(
        Name = "com.google.android.gms.gcm.GcmReceiver",
        Exported = true,
        Permission = "com.google.android.c2dm.permission.SEND")]
    [IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE", "com.google.android.c2dm.intent.REGISTRATION" },
        Categories = new[] { "@com.facebookLogin.droid@" })]
    partial class GcmReciever
    {
    }
}