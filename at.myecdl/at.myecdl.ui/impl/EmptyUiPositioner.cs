using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.ui.impl {
    public class EmptyUiPositioner : IUiPositioner{
        public void PositionWindow(System.Windows.Window window) {
            window.WindowState = System.Windows.WindowState.Normal;
        }
    }
}
