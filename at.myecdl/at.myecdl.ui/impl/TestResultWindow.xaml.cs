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
    /// Interaction logic for TestResultWindow.xaml
    /// </summary>
    public partial class TestResultWindow : Window {
        public TestResultWindow() {
            InitializeComponent();
        }

        public void setInput(List<TaskResultViewModel> results) {
            taskList.ItemsSource = results;
        }
    }
}
