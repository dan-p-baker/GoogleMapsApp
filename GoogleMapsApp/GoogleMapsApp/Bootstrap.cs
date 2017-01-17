using SimpleInjector;
using GoogleMapsApp.Application;

namespace GoogleMapsApp
{
    internal class Bootstrap
    {
        public static Container Container;

        public static void Start()
        {
            Container = new Container();

            Container.Register<IGooglePlacesServiceV1, GooglePlacesService>(Lifestyle.Singleton);
        }
    }
}