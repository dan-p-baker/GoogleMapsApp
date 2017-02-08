using System.Collections.Generic;

namespace GoogleMapsApp.Models
{
    public class GooglePlacesRootObject
    {
        public List<GooglePlacesPrediction> Predictions { get; set; }
    }
}