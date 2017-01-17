using Android.App;
using Android.Widget;
using Android.OS;
using System;
using GoogleMapsApp.Application;
using System.Linq;

namespace GoogleMapsApp
{
    [Activity(Label = "Location Finder", MainLauncher = true, Icon = "@drawable/icon")]
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
           
            addressSearchButton.Enabled = false;

            addressSearchText.Click += (object sender, EventArgs e) =>
            {
                if (string.IsNullOrWhiteSpace(addressSearchText.Text))
                {
                    addressSearchButton.Enabled = false;
                }
                else
                {
                    addressSearchButton.Enabled = true;
                }
            };

            addressSearchButton.Click += async (object sender, EventArgs e) =>
            {
                var alertDialog = new AlertDialog.Builder(this);

                var placesResultList = await _placesService.GetPlacesAutoCompleteResults(addressSearchText.Text);
                var firstResult = placesResultList.GooglePlacesResultsList.FirstOrDefault();

                alertDialog.SetMessage($"First result was {firstResult.Description}");
                alertDialog.SetNeutralButton("Find Location?", delegate
                {
                    //// Create intent to dial phone
                    //var callIntent = new Intent(Intent.ActionCall);
                    //callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                    //StartActivity(callIntent);
                });
                alertDialog.SetNegativeButton("Cancel", delegate { });

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

