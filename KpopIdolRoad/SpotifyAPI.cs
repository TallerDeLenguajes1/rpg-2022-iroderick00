using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace KpopIdolRoad
{
    public class SpotifyAPI
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public static string GetAccessToken(SpotifyAPI element)
        {
            //SpotifyToken token = new SpotifyToken();
            string url5 = @"https://accounts.spotify.com/api/token";
            var clientid = "2bb30bc065cf47c794a043e8a0a32014";
            var clientsecret = "8314cb7d2452480caa3633360fc7571d";

            //request to get the access token
            var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientid, clientsecret)));

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);

            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization: Basic " + encode_clientid_clientsecret);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";

            var request = ("grant_type=client_credentials");
            byte[] req_bytes = Encoding.ASCII.GetBytes(request);
            webRequest.ContentLength = req_bytes.Length;

            Stream strm = webRequest.GetRequestStream();
            strm.Write(req_bytes, 0, req_bytes.Length);
            strm.Close();

            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            String json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json and parse for accesstoken
                    json = rdr.ReadToEnd();
                    element = System.Text.Json.JsonSerializer.Deserialize<SpotifyAPI>(json);
                }
            }
            return element.access_token;
            //var client = new RestClient("https://accounts.spotify.com/api/token");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", "Basic MmJiMzBiYzA2NWNmNDdjNzk0YTA0M2U4YTBhMzIwMTQ6ODMxNGNiN2QyNDUyNDgwY2FhMzYzMzM2MGZjNzU3MWQ=");
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddHeader("Cookie", "__Host-device_id=AQDWan3MDW7sx8VOr78WPBxfMfa3YGkTh-MsNfXuJakOu8XMJH37URPRq1an48i4-LzFYH4K3yL9JPgQN4sQIWreJJCFrsCX4-c");
            //request.AddParameter("grant_type", "client_credentials");
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
        }
        public static Playlist get()
        {
            var spotifyHelper = new SpotifyAPI();
            var token = GetAccessToken(spotifyHelper);
            var url = "https://api.spotify.com/v1/playlists/54aVSLPh1O46tJX6W4GoYM/tracks";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization: Bearer " + token);
            request.Accept = "application/json";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null)
                    {
                        return null;
                    }
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        var playlist = System.Text.Json.JsonSerializer.Deserialize<Playlist>(responseBody);
                        return playlist;
                    }
                }
            }
        }
    }
    //public class Album
    //{
    //    [JsonProperty("album_type")]
    //    public string AlbumType { get; set; }

    //    [JsonProperty("artists")]
    //    public List<Artist> Artists { get; set; }

    //    [JsonProperty("href")]
    //    public string Href { get; set; }

    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("release_date")]
    //    public string ReleaseDate { get; set; }

    //    [JsonProperty("release_date_precision")]
    //    public string ReleaseDatePrecision { get; set; }

    //    [JsonProperty("total_tracks")]
    //    public int TotalTracks { get; set; }

    //    [JsonProperty("type")]
    //    public string Type { get; set; }
    //}

    //public class Artist
    //{
    //    [JsonProperty("href")]
    //    public string Href { get; set; }

    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("type")]
    //    public string Type { get; set; }
    //}

    //public class Item
    //{
    //    [JsonProperty("track")]
    //    public Track Track { get; set; }

    //    [JsonProperty("primary_color")]
    //    public object PrimaryColor { get; set; }
    //}

    //public class Playlist
    //{
    //    [JsonProperty("href")]
    //    public string Href { get; set; }

    //    [JsonProperty("items")]
    //    public List<Item> Items { get; set; }

    //    [JsonProperty("total")]
    //    public int Total { get; set; }
    //}

    //public class Track
    //{
    //    [JsonProperty("album")]
    //    public Album Album { get; set; }

    //    [JsonProperty("artists")]
    //    public List<Artist> Artists { get; set; }

    //    [JsonProperty("href")]
    //    public string Href { get; set; }

    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("popularity")]
    //    public int Popularity { get; set; }

    //    [JsonProperty("track_number")]
    //    public int TrackNumber { get; set; }
    //}
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
}