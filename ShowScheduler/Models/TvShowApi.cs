using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShowScheduler.Models
{
   public class TvShowApi
    {
        public static async Task<RootObject> GetShowDetails(String name)
        {
            var http = new HttpClient();
            var url = String.Format("http://api.tvmaze.com/singlesearch/shows?q={0}", name);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);
            return data;
        }
    }

    [DataContract]
    public class Schedule
    {
        [DataMember]
        public string time { get; set; }
        [DataMember]
        public List<string> days { get; set; }
    }
    [DataContract]
    public class Rating
    {
        [DataMember]
        public double average { get; set; }
    }
    [DataContract]
    public class Country
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string timezone { get; set; }
    }
    [DataContract]
    public class Network
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Country country { get; set; }
    }
    [DataContract]
    public class Externals
    {
        [DataMember]
        public int tvrage { get; set; }
        [DataMember]
        public int thetvdb { get; set; }
        [DataMember]
        public string imdb { get; set; }
    }
    [DataContract]
    public class Image
    {
        [DataMember]
        public string medium { get; set; }
        [DataMember]
        public string original { get; set; }
    }
    [DataContract]
    public class Self
    {
        [DataMember]
        public string href { get; set; }
    }
    [DataContract]
    public class Previousepisode
    {
        [DataMember]
        public string href { get; set; }
    }
    [DataContract]
    public class Nextepisode
    {
        [DataMember]
        public string href { get; set; }
    }
    [DataContract]
    public class Links
    {
        [DataMember]
        public Self self { get; set; }
        [DataMember]
        public Previousepisode previousepisode { get; set; }
        [DataMember]
        public Nextepisode nextepisode { get; set; }
    }
    [DataContract]
    public class RootObject
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string language { get; set; }
        [DataMember]
        public List<string> genres { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public int runtime { get; set; }
        [DataMember]
        public string premiered { get; set; }
        [DataMember]
        public Schedule schedule { get; set; }
        [DataMember]
        public Rating rating { get; set; }
        [DataMember]
        public int weight { get; set; }
        [DataMember]
        public Network network { get; set; }
        [DataMember]
        public object webChannel { get; set; }
        [DataMember]
        public Externals externals { get; set; }
        [DataMember]
        public Image image { get; set; }
        [DataMember]
        public string summary { get; set; }
        [DataMember]
        public int updated { get; set; }
        [DataMember]
        public Links _links { get; set; }
    }

}
