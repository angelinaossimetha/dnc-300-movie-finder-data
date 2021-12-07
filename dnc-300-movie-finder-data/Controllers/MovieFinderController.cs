using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace dnc_300_movie_finder_data.Controllers
{
    public class MovieFinderController : Controller
    {
        private readonly string API_KEY = "e6addce6"; 
        public string FindMovie(string parameter, string details)
        {
            if (CacheModel.Contains(details))
            {
                Console.WriteLine(CacheModel.Get(details));
                return CacheModel.Get(details).Title; 
            }
            string url = "";
           
            switch (parameter)
            { 
                case "i":
                    url = $"https://omdbapi.com/?i={details}&apikey={API_KEY}";
                    break;
                case "t":
                    url = $"https://omdbapi.com/?t={details}&apikey={API_KEY}";
                    break;
                default:
                    throw new Exception("Please provide a valid URL parameter either i or t"); 
            }
            Console.WriteLine(url);
            return this.getMovie(url, details).Title;
           
        }

        private Movie getMovie(String url, string details)
        {
            string responseBody = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    
                    client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"
                   
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                       
                        response.EnsureSuccessStatusCode();
                        responseBody = response.Content.ReadAsStringAsync().Result;
                       
                    }
                }
            } catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            Console.WriteLine(responseBody);
            Movie movie = JsonConvert.DeserializeObject<Movie>(responseBody);
            CacheModel.Add(details, movie);
            return movie;
        }
    }
}
