using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FacebookLogin
{
    class SpotifyAPI
    {
        const string address_base = "https://api.spotify.com/";
        const string search_base = "v1/search?";
        const string query_base = "q=";
        const string type_base = "type=";
        const string offset_base = "offset=0&limit=10";

        public delegate void SpotifyEventHandler(object sender, SpotifyEvent e);

        public event SpotifyEventHandler TracksLoaded;

        public void SearchTrack(string query)
        {
            if (String.IsNullOrWhiteSpace(query))
                return;
            query = Uri.EscapeUriString(query);
            string url_query = address_base + search_base + query_base + query + "&" + type_base + SpotifyType.track.ToString()
                + "&" + offset_base;
            WebRequest request = WebRequest.CreateHttp(url_query);
            var response = request.BeginGetResponse(new AsyncCallback(CallbackTrack), request);
        }

        private void CallbackTrack(IAsyncResult result)
        {
            WebRequest request = (WebRequest)result.AsyncState;
            var response = request.EndGetResponse(result);
            var stream = response.GetResponseStream();
            var tracks = DataToTrack(stream);
            TracksLoaded?.Invoke(this, new SpotifyEvent(tracks));
        }

        private IEnumerable<SpotifyTrack> DataToTrack(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            JObject obj = JObject.Parse(reader.ReadToEnd());
            var items = obj["tracks"]["items"].Children(); //.ToString(Formatting.None);

            List<SpotifyTrack> tracks = new List<SpotifyTrack>();

            foreach (var item in items)
            {
                SpotifyTrack track = item.ToObject<SpotifyTrack>();
                track.Thumbnail = item["album"]["images"].Last["url"].ToString();
                track.Album = item["album"]["name"].ToString();

                List<string> artistes = new List<string>();
                foreach (var artist in item["artists"])
                {
                    artistes.Add(artist["name"].ToString());
                }

                track.Artiste = String.Join(", ", artistes);
                tracks.Add(track);
            }
            return tracks;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class SpotifyTrack
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Href { get; set; }

        public string Thumbnail { get; set; }
        public string Album { get; set; }
        public string Artiste { get; set; }

        public bool voted;
    }

    public class SpotifyEvent : EventArgs
    {
        public IEnumerable<SpotifyTrack> tracks { get; set; }

        public SpotifyEvent(IEnumerable<SpotifyTrack> t)
        {
            tracks = t;
        }
    }

    enum SpotifyType
    {
        album,
        artist,
        playlist,
        track
    }
}
