using Android.App;
using Android.Widget;
using Android.OS;
using System;
using GoogleMapsApp.Application;
using GoogleMapsApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace GoogleMapsApp
{
    [Activity(Label = "Google Maps App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private static IGooglePlacesServiceV1 _placesService;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            BootstrapApplication();        
            SetContentView (Resource.Layout.Main);

            var addressSearchText = FindViewById<EditText>(Resource.Id.AddressSearchText);
            var addressSearchButton = FindViewById<Button>(Resource.Id.AddressSearchButton);
 
            addressSearchButton.Click += async (object sender, EventArgs e) =>
            {
                var alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetMessage("Searching...");
                alertDialog.Show();

                Task<GooglePlacesRootObject> placesResultListTask = _placesService.GetPlacesAutoCompleteResults(addressSearchText.Text);
                var placesResultList = await placesResultListTask.ConfigureAwait(false);             

                if (placesResultList.Predictions.Count > 0)
                {
                    var firstResult = placesResultList.Predictions.FirstOrDefault();

                    alertDialog.SetMessage($"First result was {firstResult.Description}");
                    alertDialog.SetNeutralButton("Find Location?", delegate
                    {
                    });
                    alertDialog.SetNegativeButton("Cancel", delegate { });
                }
                else
                    alertDialog.SetMessage("No results found");

                alertDialog.Show();
            };
        }
        private static void BootstrapApplication()
        {
            Bootstrap.Start();
            _placesService = Bootstrap.Container.GetInstance<IGooglePlacesServiceV1>();
        }
    }
}

