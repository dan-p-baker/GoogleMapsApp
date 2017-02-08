using GoogleMapsApp.Models;
using GoogleMapsApp.Credentials;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace GoogleMapsApp.Application
{
    public class GooglePlacesService : IGooglePlacesServiceV1
    {
        async Task<GooglePlacesRootObject> IGooglePlacesServiceV1.GetPlacesAutoCompleteResults(string query)
        {
            var url = ($"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={query}&types=geocode&key={GoogleCredentials.ApiKey}");

            var httpClient = new HttpClient();
     
            Task<string> responseTask = httpClient.GetStringAsync(url);
            var response = await responseTask.ConfigureAwait(false);

            return JsonConvert.DeserializeObject<GooglePlacesRootObject>(response);  

        }
    }
}