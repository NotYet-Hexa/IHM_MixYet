using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Newtonsoft.Json.Linq;

namespace FacebookLogin
{
    public class FacebookProfil 
    {
        string id, name, urlpicture, age_range, devices, gender;

        public FacebookProfil(JObject jsonfile)
        {
            this.name = (string)jsonfile["name"];
            this.id = (string)jsonfile["id"];
            this.urlpicture = (string)jsonfile["picture"]["data"]["url"];
            this.age_range = (string)jsonfile["age_range"]["max"];
            this.devices = (string)jsonfile["devices"][0]["os"];
            this.gender = (string)jsonfile["gender"];
        }
        public string Devices
        {
            get
            {
             
                return devices;
            }
            set
            {
                if (devices != value)
                {
                    devices = value;
                }
            }
        }
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (gender != value)
                {
                    gender = value;
                }
            }
        }
        public string Age_Range
        {
            get
            {
                return age_range;
            }
            set
            {
                if (age_range != value)
                {
                    age_range = value;
                }
            }
        }
        public string UrlPicture
        {
            get
            {
                return urlpicture;
            }
            set
            {
                if (urlpicture != value)
                {
                    urlpicture = value;
                }
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                }
            }
        }
    }
}
