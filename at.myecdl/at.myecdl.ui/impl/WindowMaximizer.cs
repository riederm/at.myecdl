using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace at.myecdl.ui.impl {
    public class WindowMaximizer : IUiPositioner{

        public void PositionWindow(Window window) {
            window.WindowState = WindowState.Maximized;
            
        }

    }
}
