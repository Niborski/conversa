// Licensed under the The Apache License Version 2.0, January 2004
// ---------------------------------------------------------------
// https://github.com/Windows-XAML/Template10
// ---------------------------------------------------------------

using Conversa.Common;
using Conversa.Services.NavigationService;
using Conversa.W10.Common;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Conversa.Mvvm
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-MVVM
    public abstract class ViewModelBase : BindableBase, INavigable
    {
        public string Identifier { get; set; }

        public virtual void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state) { /* nothing by default */ }
        public virtual Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending) { return Task.FromResult<object>(null); }
        public virtual void OnNavigatingFrom(NavigatingEventArgs args) { /* nothing by default */ }

        public NavigationService NavigationService { get; set; }
    }
}