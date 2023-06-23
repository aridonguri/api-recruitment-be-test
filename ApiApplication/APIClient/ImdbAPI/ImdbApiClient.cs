using System.Text.Json.Serialization;
using System;
using Newtonsoft.Json;
using ApiApplication.Database.Entities;
using RestSharp;
using ApiApplication.ViewModels.Shared;

namespace ApiApplication.APIClient.ImdbAPI
{
    public class ImdbApiClient
    {
        private readonly string baseUrl = "https://imdb-api.com/{language}/API/Title/{apiKey}/";

        public MovieEntity GetMovieDetails(string movieId)
        {
            string apiUrl = baseUrl + "{movieId}";
            string apiKey = "k_cqkd7133";
            string language = "en";

            var client = new RestClient(apiUrl);
            var request = new RestRequest("GET");
            request.AddUrlSegment("language", language);
            request.AddUrlSegment("apiKey", apiKey);
            request.AddUrlSegment("movieId", movieId);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content);

                MovieEntity movie = new MovieEntity
                {
                    Title = myDeserializedClass.title,
                    ImdbId = movieId,
                    ReleaseDate = DateTime.Parse(myDeserializedClass.releaseDate),
                    Stars = myDeserializedClass.stars
                };

                return movie;
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return null;
            }
        }
    }
}
