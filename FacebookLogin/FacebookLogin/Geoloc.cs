using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Geolocator;
using System.Threading.Tasks;

namespace FacebookLogin
{
    public class Geoloc
    {
        public string latitude, longitude;
        public Geoloc()
        {

        }
        
        public  async Task GetPosition() //TODO faire une Exception propre
            
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                if (position == null)
                    return;
                else
                {
                    this.latitude = position.Latitude.ToString();
                    this.longitude = position.Longitude.ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task getLatLon()
        {
            await GetPosition();
        }
    }
}
