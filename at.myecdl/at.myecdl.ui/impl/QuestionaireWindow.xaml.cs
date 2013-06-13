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
                DisplayQuestion(currentQuestion);
            }
        }

        public QuestionaireWindow() {

            InitializeComponent();
        }

        public void DisplayQuestion(IQuestion question) {
            questionLabel.DataContext = question;
            answerBox.ItemsSource = question.Answers;
   
            // show detail-descriptoin-dialogue 
            var background = Background;
            this.Background = null;
            this.Background = background;
        }

        public void SetCurrentQuestion(IQuestion newCurrentQuestion) {
            currentQuestion = newCurrentQuestion;
            DisplayQuestion(currentQuestion);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e) {
            if (currentQuestion != null) {
                DisplayQuestion(currentQuestion);
            }
        }

        public void ShowPresenter() {
            Show();
            double delta = System.Windows.SystemParameters.WindowCaptionHeight; 
            this.Height = System.Windows.SystemParameters.FullPrimaryScreenHeight + delta;
            this.Width = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            this.Left = 0;
            this.Top = 0;


        }

        public void HidePresenter() {
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void AnswerSelected(object sender, RoutedEventArgs e) {
            Control sourceControl = (Control)e.Source;
            IAnswer answer = (IAnswer)sourceControl.DataContext;
            //CurrentQuestion.Answer = answer;
            currentQuestion.Answers.ForEach(a => a.Checked = false);
            answer.Checked = true;
        }

        //public void showevaluation(itaskevaluation evaluation) {
        //     mark correct and incorrect answer
        //    markquestionanswers(evaluation);

        //}

        //private void markQuestionAnswers(ITaskEvaluation evaluation) {
        //    ITask currentTask = evaluation.Task;
        //    ignore if currenTask is no question
        //    if (currentTask is Question) {
        //        Question currentQuestion = (Question)currentTask;
        //        markCorrect(currentQuestion.CorrectAnswers);
        //        if (!evaluation.IsCorrect) {
        //            currently selected answer (it is wrong o_o )
        //            markIncorrect(currentQuestion.Answer);
        //        }
        //    }
        //}

        //private void markIncorrect(Answer answer) {
        //    RadioButton button = GetRadioButtonForAnswer(answer);
        //    button.Foreground = Brushes.Red;
        //    button.FontWeight = FontWeights.Bold;
        //}

        //private void markCorrect(ISet<Answer> Answers) {
        //    foreach (var answer in Answers) {
        //        RadioButton button = GetRadioButtonForAnswer(answer);
        //        if (button != null) {
        //            button.Foreground = Brushes.Green;
        //            button.FontWeight = FontWeights.Bold;
        //        }
        //    }
        //}

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
                SetCurrentQuestion((IQuestion)task);
                ShowPresenter();
            } else {
                Visibility = System.Windows.Visibility.Hidden;
            }
        }


        public Window AsWindow() {
            return this;
        }
    }
}
