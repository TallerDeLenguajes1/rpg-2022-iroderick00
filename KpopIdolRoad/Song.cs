using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KpopIdolRoad
{
    public class Song
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public Song() { }
        //public static Song get()
        //{
        //    const string playlistId = "03vujy1O8wJTLljbtkMVXy";
        //    var url = $@"https://api.spotify.com/v1/playlists/{playlistId}/tracks/";
        //    var request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    request.Accept = "application/json";
        //    using (WebResponse response = request.GetResponse())
        //    {
        //        using (Stream strReader = response.GetResponseStream())
        //        {
        //            if (strReader == null) return null;
        //            using (StreamReader objReader = new StreamReader(strReader))
        //            {
        //                string responseBody = objReader.ReadToEnd();
        //                var location = JsonSerializer.Deserialize<Song>(responseBody);
        //                return location;
        //            }
        //        }
        //    }
        //} 
    }
}
