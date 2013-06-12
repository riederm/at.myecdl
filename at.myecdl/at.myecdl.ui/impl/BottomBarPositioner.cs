using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace at.myecdl.ui.impl {
    public class BottomBarPositioner : IUiPositioner{

        public void PositionWindow(Window window) {
            window.Top = -2000; //move the window outside the screen
            window.Show();
            AppBarFunctions.SetAppBar(window, AppBarPosition.Bottom);
        }
    }
}
