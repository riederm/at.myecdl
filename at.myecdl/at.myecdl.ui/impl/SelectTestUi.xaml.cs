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
using Ninject;
using at.myecdl.model;

namespace at.myecdl.ui.impl {
    /// <summary>
    /// Interaction logic for SelectTestUi.xaml
    /// </summary>
    public partial class SelectTestUiImpl : Window, ISelectTestUi {
        private ITestProvider testProvider;
        private ITestRunner testRunner;

        [Inject]
        public SelectTestUiImpl(ITestProvider testProvider, ITestRunner testRunner) {
            this.testProvider = testProvider;
            this.testRunner = testRunner;
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            this.testList.ItemsSource = testProvider.GetTests();
        }

        public Window AsWindow() {
            return this;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void startButton_Click(object sender, RoutedEventArgs e) {
            ITest selectedTest = (ITest)testList.SelectedItem;
            startTest(selectedTest);
        }

        private void startTest(ITest selectedTest) {
            this.Close();
            testRunner.Run(selectedTest);
        }

        private void testList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void testList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && obj != testList) {
                if (obj.GetType() == typeof(ListViewItem)) {
                    startTest((ITest)((ListViewItem)obj).Content);
                    break;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }

    }
}
