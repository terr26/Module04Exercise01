using System.Diagnostics;
using Microsoft.Maui.ApplicationModel;
using Module03Exercise01.View;
using System.Threading.Tasks;

namespace Module03Exercise01
{
    public partial class App : Application
    {
        private const string TestUrl = "https://google.com";

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            bool isConnected = Connectivity.NetworkAccess == NetworkAccess.Internet && await IsWebsiteReachable(TestUrl);

            if (isConnected)
            {
                MainPage = new NavigationPage(new LoginPage());
                Debug.WriteLine("Application Started (Online)");
            }
            else
            {
                MainPage = new OfflinePage();
                Debug.WriteLine("Application Started (Offline)");
            }
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("Application Sleeping");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("Application Resumed");
        }

        private async Task<bool> IsWebsiteReachable(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task CheckConnectivityAndNavigate()
        {
            var current = Connectivity.NetworkAccess;
            bool isWebsiteReachable = await IsWebsiteReachable(TestUrl);

            if (current == NetworkAccess.Internet && isWebsiteReachable)
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new OfflinePage();
            }
        }


    }
}
