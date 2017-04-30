using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Geolocator;
using System.Threading.Tasks;
using System.Threading;

namespace FacebookLogin
{
    public class Geoloc
    {
        public string latitude, longitude;
        private CancellationTokenSource _geolocationCancelationTokenSource = null;
        public bool boSuccess;
        public Geoloc()
        {

        }

        public async Task<bool> GetPosition() //TODO faire une Exception propre

        {
            try
            {
                _geolocationCancelationTokenSource = new CancellationTokenSource();
                CancellationToken token = _geolocationCancelationTokenSource.Token;
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                this.cancelGeolocationTimerAsync();
                Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync(timeoutMilliseconds: 15000, token: token);
                boSuccess = true;
                if (position == null)
                    return false;
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
            finally
            {
                _geolocationCancelationTokenSource = null;
            }
            return boSuccess;
        }
        public async Task getLatLon()
        {
            await GetPosition();
        }
        private async Task cancelGeolocationTimerAsync()
        {
            await Task.Delay(20000);
            if (_geolocationCancelationTokenSource != null)
            {
                _geolocationCancelationTokenSource.Cancel();
                _geolocationCancelationTokenSource = null;
            }
        }
    }
}
