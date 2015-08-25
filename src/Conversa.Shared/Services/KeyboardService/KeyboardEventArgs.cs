// Licensed under the The Apache License Version 2.0, January 2004
// ---------------------------------------------------------------
// https://github.com/Windows-XAML/Template10
// ---------------------------------------------------------------

using System;
using Windows.System;
using Windows.UI.Core;

namespace Conversa.Services.KeyboardService
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-KeyboardService
    public class KeyboardEventArgs : EventArgs
    {
        public bool AltKey { get; set; }
        public bool ControlKey { get; set; }
        public bool ShiftKey { get; set; }
        public VirtualKey VirtualKey { get; set; }
        public AcceleratorKeyEventArgs EventArgs { get; set; }
        public char? Character { get; set; }
    }
}
