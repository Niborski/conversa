// Licensed under the The Apache License Version 2.0, January 2004
// ---------------------------------------------------------------
// https://github.com/Windows-XAML/Template10
// ---------------------------------------------------------------

using System;

namespace Conversa.Services.KeyboardService
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-KeyboardService
    public class KeyboardService
    {
        KeyboardHelper _helper;

        public KeyboardService()
        {
            _helper = new KeyboardHelper();
            _helper.GoBackGestured = () => { AfterBackGesture?.Invoke(); };
            _helper.GoForwardGestured = () => { AfterForwardGesture?.Invoke(); };
            _helper.ControlEGestured = () => { AfterControlEGesture?.Invoke(); };
            _helper.WindowZGestured = () => { AfterWindowZGesture?.Invoke(); };
        }

        public Action AfterBackGesture { get; set; }
        public Action AfterForwardGesture { get; set; }
        public Action AfterControlEGesture { get; set; }
        public Action AfterWindowZGesture { get; set; }
    }

}
