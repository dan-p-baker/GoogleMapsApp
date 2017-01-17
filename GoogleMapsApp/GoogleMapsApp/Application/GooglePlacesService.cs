using GoogleMapsApp.Models;
using GoogleMapsApp.Credentials;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using ModernHttpClient;

namespace GoogleMapsApp.Application
{
    public class GooglePlacesService : IGooglePlacesServiceV1
    {
        async Task<GooglePlacesResultsListModel> IGooglePlacesServiceV1.GetPlacesAutoCompleteResults(string query)
        {
            var url = ($"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={query}&types=geocode&key=demo{GoogleCredentials.ApiKey}");

            return await GetPlacesResultsAsync(url);
        }

        private async Task<GooglePlacesResultsListModel> GetPlacesResultsAsync(string url)
        {
            var httpClient = new HttpClient(new NativeMessageHandler());
            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GooglePlacesResultsListModel>(result);
            }
            else
            {
                throw new HttpRequestException();
            }
        }
    }
}