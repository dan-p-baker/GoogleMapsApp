namespace GoogleMapsApp.Models
{
    public class GooglePlacesResultModel
    {
        public string Description { get; set; }
        public string PlaceId { get; set; }
        public GooglePlacesResultModel(string description, string placeId)
        {
            Description = description;
            PlaceId = placeId;
        }

    }
}