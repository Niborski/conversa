// Licensed under the The Apache License Version 2.0, January 2004
// ---------------------------------------------------------------
// https://github.com/Windows-XAML/Template10
// ---------------------------------------------------------------

namespace Conversa.Services.ToastService
{

    // http://msdn.microsoft.com/en-us/library/windows/apps/Hh761494.aspx

    /* In this set of templates,
    * the image element is expressed using one of these protocols:
    * http:// or https:// - a web-based image.
    * ms-appx:/// - an image included in the app package.
    * ms-appdata:///local/ - an image saved to local storage.
    * file:/// - a local image. (Only supported for desktop apps.)
    */


    public class ToastService
    {
        ToastHelper _Helper = new ToastHelper();
    }
}