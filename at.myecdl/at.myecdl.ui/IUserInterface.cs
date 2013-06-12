using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace at.myecdl.ui {
    public interface IUserInterface {
        void Show();

        void Hide();

        Window AsWindow();
    }
}
