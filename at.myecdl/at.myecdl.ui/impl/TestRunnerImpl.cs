using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using at.myecdl.model;
using at.myecdl.util;
using System.Windows;
using System.ComponentModel;
using System.Threading.Tasks;

namespace at.myecdl.ui.impl {
    public class TestRunnerImpl : ITestRunner {

        private ITestRunUi testUi;
        private IUiPositioner positioner;
        private IEnumerator<ITask> tasks;
        private HashSet<ITask> submittedTasks = new HashSet<ITask>();
        private ITaskDetailUi detailUi;
        private ITest test;

        [Inject]
        public TestRunnerImpl(ITestRunUi testUi, ITaskDetailUi detailUi, [Named(inject.UiModule.LOCATION_BOTTOM)] IUiPositioner positioner) {
            this.testUi = testUi;
            this.positioner = positioner;
            this.detailUi = detailUi;

            InitializeBindings(testUi);
        }

        private void InitializeBindings(ITestRunUi testUi) {
            testUi.EndClicked += new EventHandler(testUi_EndClicked);
            testUi.SkipClicked += new EventHandler(testUi_SkipClicked);
            testUi.SubmitClicked += new EventHandler(testUi_SubmitClicked);
        }

        void testUi_SubmitClicked(object sender, EventArgs e) {
            if (tasks.MoveNext()) {
                var currentTask = tasks.Current;

            }
        }

        void testUi_SkipClicked(object sender, EventArgs e) {
            if (tasks.MoveNext()) {
                UpdateUiForTask(test, tasks.Current);
            }
        }

        void testUi_EndClicked(object sender, EventArgs e) {
            
        }

        public void Run(model.ITest test) {
            submittedTasks.Clear();
            this.test = test;
            tasks = new CircularEnumerator<ITask>(test.Tasks);
            tasks.MoveNext();

            var progress = new ProgressDialogImpl();
            progress.Begin("Lade Test...");
            progress.Show();
            Task.Factory
                 .StartNew(() => positioner.PositionWindow(testUi.AsWindow()))
                 .ContinueWith(t => {
                     progress.Close();
                     var currentTask = tasks.Current;
                     UpdateUiForTask(test, currentTask);
                 }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void UpdateUiForTask(model.ITest test, ITask currentTask) {
            testUi.UpdateCurrentTaskDescription(currentTask.Description);
            testUi.UpdateProgress(GetNumberOfSubmittedTasks(test.Tasks), test.Tasks.Count);
            detailUi.InitializeFor(currentTask);
        }
        
        private int GetNumberOfSubmittedTasks(List<ITask> list) {
            return list.Sum<ITask>(t => submittedTasks.Contains(t) ? 1 : 0);
        }

    }
}
