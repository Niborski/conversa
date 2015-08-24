using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Conversa.Services.NavigationService
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-NavigationService
    public interface INavigable
    {
        void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state);
        Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending);
        void OnNavigatingFrom(NavigatingEventArgs args);
        NavigationService NavigationService { get; set; }

        /// <summary>
        /// Used by NavigationService when NavigationCacheMode is Enabled, will load state when possible.
        /// </summary>
        string Identifier { get; set; }
    }
}