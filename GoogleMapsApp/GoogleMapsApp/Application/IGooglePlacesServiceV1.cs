using GoogleMapsApp.Models;
using System.Threading.Tasks;

namespace GoogleMapsApp.Application
{
    public interface IGooglePlacesServiceV1
    {
        Task<GooglePlacesRootObject> GetPlacesAutoCompleteResults(string query);
    }
}