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
        private IVolumeProvider volumeProvider;


        [Inject]
        public TestRunnerImpl(ITestRunUi testUi,
                                ITaskDetailUi detailUi,
                                [Named(inject.UiModule.LOCATION_BOTTOM)] IUiPositioner positioner,
                                IVolumeProvider volumeProvider) {
            this.testUi = testUi;
            this.positioner = positioner;
            this.detailUi = detailUi;
            this.volumeProvider = volumeProvider;

            InitializeBindings(testUi);
        }

        private void InitializeBindings(ITestRunUi testUi) {
            testUi.EndClicked += new EventHandler(testUi_EndClicked);
            testUi.SkipClicked += new EventHandler(testUi_SkipClicked);
            testUi.SubmitClicked += new EventHandler(testUi_SubmitClicked);
        }

        void testUi_SubmitClicked(object sender, EventArgs e) {
            var currentTask = GetCurrentTask();
            currentTask.Evaluate();
            submittedTasks.Add(currentTask);
            MoveToNextTask();
            UpdateUiForCurrentTask();
        }

        void testUi_SkipClicked(object sender, EventArgs e) {
            MoveToNextTask();
            UpdateUiForCurrentTask();
        }

        void testUi_EndClicked(object sender, EventArgs e) {
            volumeProvider.Dispose();
            Application.Current.Shutdown();
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
                 .StartNew(() => {
                     positioner.PositionWindow(testUi.AsWindow());
                     volumeProvider.Initialize();
                 })
                 .ContinueWith(t => {
                     progress.Close();
                     var currentTask = tasks.Current;
                     UpdateUiForCurrentTask();
                 }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void UpdateUiForCurrentTask() {
            var currentTask = GetCurrentTask();
            currentTask.Setup(); //TODO make in background
            testUi.UpdateCurrentTaskDescription(currentTask.Description);
            testUi.UpdateCurrentTaskIndex(test.Tasks.IndexOf(currentTask), test.Tasks.Count);
            testUi.UpdateProgress(submittedTasks.Count, test.Tasks.Count);
            detailUi.InitializeFor(currentTask);
        }
                
        private ITask GetCurrentTask() {
            return tasks.Current;
        }

        private void MoveToNextTask() {
            tasks.MoveNext();
        }

    }
}
