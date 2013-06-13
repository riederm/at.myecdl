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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace at.myecdl.ui.impl {
    /// <summary>
    /// Interaction logic for ProgressBox.xaml
    /// </summary>
    public partial class ProgressBox : UserControl {
        public ProgressBox() {
            InitializeComponent();
        }

        public string Title {
            get { return titleLabel.Content.ToString(); }
            set { titleLabel.Content = value; }
        }

        public string Task {
            get { return this.taskLabel.Content.ToString(); }
            set { taskLabel.Content = value; }
        }

        public int Maximum {
            get { return (int)progress.Maximum; }
            set {
                if (value >= 0) {
                    progress.Maximum = value;
                } else {
                    progress.IsIndeterminate = true;
                }
            }
        }

        public int Value{
            get { return (int)progress.Value; }
            set { progress.Value = value; }
        }
        
    }

}
