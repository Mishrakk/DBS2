using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;

namespace DVDRentalStore.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        [NotMapped]
        public string Plot { get { return GetMoviePlot(); } }
        public virtual ICollection<Copy> Copies { get; set; }
        public Movie(int movieId, string title, int year, List<Copy> copies = null)
        {
            MovieId = movieId;
            Title = title;
            Year = year;
            Copies = copies;
        }
        public Movie()
        {

        }
        private string GetMoviePlot()
        {
            // For this to work you need to provide your own api key
            string apikey = "XXXXXX";
            string url = $"http://www.omdbapi.com/?apikey={apikey}&t={Title}&plot=full";
            using (var httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(url);
                task.Wait();
                var result = task.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    content.Wait();
                    var jsonString = content.Result;
                    var jsonObject = JObject.Parse(jsonString);
                    if (jsonObject.ContainsKey("Plot"))
                    {
                        return jsonObject["Plot"].ToString();
                    }
                }
            }
            return "N/A";
        }
    }
}