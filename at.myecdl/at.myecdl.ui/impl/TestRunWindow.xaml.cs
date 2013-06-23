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
using at.myecdl.model;
using at.myecdl.util;

namespace at.myecdl.ui.impl {
    /// <summary>
    /// Interaction logic for TestRunWindow.xaml
    /// </summary>
    public partial class TestRunWindow : Window, ITestRunUi {
        private Boolean initialized = false;

        public event EventHandler SubmitClicked;

        public event EventHandler SkipClicked;

        public event EventHandler EndClicked;

        public TestRunWindow() {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e) {
            if (!initialized) {
                initialized = true;
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                //AppBarFunctions.SetAppBar(this, AppBarPosition.Bottom);
            }
        }

        private void ZoomInClicked(object sender, RoutedEventArgs e) {
            InstructionLabel.FontSize++;
        }

        private void ZoomOutClicked(object sender, RoutedEventArgs e) {
            InstructionLabel.FontSize--;
        }

        private void EndTestClicked(object sender, RoutedEventArgs e) {
            FireEvent(EndClicked);
        }

        private void FireEvent(EventHandler handler) {
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }
        
        private void SkipButton_Click(object sender, RoutedEventArgs e) {
            FireEvent(SkipClicked);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e) {
            FireEvent(SubmitClicked);
        }

        public void UpdateCurrentTaskDescription(string text) {
            string[] delimiters = { "*" };
            String[] items = text.Split(delimiters, StringSplitOptions.None);

            InstructionLabel.Inlines.Clear();
            bool bold = text.StartsWith("*");

            foreach (var item in items) {
                Inline nextInline = new Run(item);
                if (bold) {
                    //wrap into bold
                    nextInline = new Bold(nextInline);
                }
                InstructionLabel.Inlines.Add(nextInline);
                bold = !bold;
            }
        }

        public void UpdateTimeLeft(TimeSpan timeLeft) {
            TimeLabel.Dispatcher.Invoke(new Action(
                    delegate() {
                        TimeLabel.Content = timeLeft.ToString(@"mm\:ss");
                    }
                ));
        }

        public void UpdateCurrentTaskIndex(int currentStep, int maxSteps) {
            ProgressLabel.Content = String.Format("Question {0} of {1}", currentStep, maxSteps);
        }

        public void EnableButtons(bool submitButton, bool skipButton, bool endButton) {
            SubmitButton.IsEnabled = submitButton;
            SkipButton.IsEnabled = skipButton;
            EndButton.IsEnabled = endButton;
        }

        public Window AsWindow() {
            return this;
        }

        public void StartNewTestRun(ITest test) {
            
        }

        public void UpdateProgress(int currentStep, int maxSteps) {
            progressBar.Maximum = maxSteps;
            progressBar.Value = currentStep;
        }
    }
}
