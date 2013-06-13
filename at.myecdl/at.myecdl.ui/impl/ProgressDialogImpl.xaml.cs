using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace at.myecdl.ui.impl {
    /// <summary>
    /// Interaction logic for ProgressDialogImpl.xaml
    /// </summary>
    public partial class ProgressDialogImpl : Window {

        private int maxTicks = 0;

        public ProgressDialogImpl() {
            InitializeComponent();
        }

        public void Begin(string title, int ticks) {
            this.maxTicks = ticks;
            progressBox.Title = title;
            progressBox.Value = 0;
            progressBox.Maximum = ticks;
            Title = title;
        }

        public void Begin(string title) {
            progressBox.Maximum = -1;
            progressBox.Value = 0;
            progressBox.Title = title;
        }

        public void Worked(int ticks) {
            int value = (int)progressBox.Value;
            int nextValue = Math.Min(value + ticks, maxTicks);
            progressBox.Value = nextValue;
        }
        
    }

}
