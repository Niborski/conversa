using System;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace Conversa.Services.NetworkAvailableService
{
    public class NetworkAvailableService
    {
        public NetworkAvailableService()
        {
            NetworkInformation.NetworkStatusChanged += async (s) =>
            {
                var available = await this.IsInternetAvailable();
                if (AvailabilityChanged != null)
                {
                    try { AvailabilityChanged(available); }
                    catch { }
                }
            };
        }

        public Action<bool> AvailabilityChanged { get; set; }

        public async Task<bool> IsInternetAvailable()
        {
            await Task.Delay(0);
            var profile = NetworkInformation.GetInternetConnectionProfile();
            if (profile == null)
                return false;
            var net = NetworkConnectivityLevel.InternetAccess;
            return profile.GetNetworkConnectivityLevel().Equals(net);
        }
    }
}
