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

using System.ComponentModel;

using at.myecdl.ui;
using at.myecdl.model;

namespace at.myecdl.ui.impl {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class QuestionaireWindow : Window, ITaskDetailUi {

        private IQuestion currentQuestion;

        public IQuestion CurrentQuestion {
            get { return currentQuestion; }
            set {
                currentQuestion = value;
                if (currentQuestion != null) {
                    DisplayQuestion(currentQuestion, currentQuestion.Answers);
                } else {
                    ClearUi();
                }
            }
        }

        private void ClearUi() {
            DisplayQuestion(null, null);
        }

        public QuestionaireWindow() {
            InitializeComponent();
        }

        private void DisplayQuestion(IQuestion question) {
            DisplayQuestion(question, question.Answers);
        }

        private void DisplayQuestion(IQuestion question, List<IAnswer> answers) {
            questionLabel.DataContext = question;
            answerBox.ItemsSource = answers;

            if (question != null) {
                answerBox.IsEnabled = !question.IsAnswered;
            }

            // show detail-descriptoin-dialogue 
            var background = Background;
            this.Background = null;
            this.Background = background;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e) {
            if (currentQuestion != null) {
                DisplayQuestion(currentQuestion);
            }
        }

        public void ShowMaximized() {
            Show();
            double delta = System.Windows.SystemParameters.WindowCaptionHeight;
            this.Height = System.Windows.SystemParameters.FullPrimaryScreenHeight + delta;
            this.Width = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            this.Left = 0;
            this.Top = 0;
        }
        
        private void AnswerSelected(object sender, RoutedEventArgs e) {
            Control sourceControl = (Control)e.Source;
            IAnswer answer = (IAnswer)sourceControl.DataContext;
            answer.Checked = true;
        }

        private void AnswerCleared(object sender, RoutedEventArgs e) {
            Control sourceControl = (Control)e.Source;
            IAnswer answer = (IAnswer)sourceControl.DataContext;
            answer.Checked = false;
        }

        private RadioButton GetRadioButtonForAnswer(IAnswer answer) {

            var currentItem = answerBox.ItemContainerGenerator.ContainerFromItem(answer);
            // Getting the ContentPresenter of myListBoxItem
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(currentItem);
            var button = myContentPresenter.TemplatedParent;
            return (RadioButton)button;
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public void InitializeFor(ITask task) {
            if (task is IQuestion) {
                CurrentQuestion = (IQuestion)task;
                ShowMaximized();
            } else {
                Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public Window AsWindow() {
            return this;
        }
    }
}
