using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace at.myecdl.ui.impl {
    public class TestRunnerImpl : ITestRunner {

        private ITestRunUi testUi;
        private IUiPositioner positioner;

        [Inject]
        public TestRunnerImpl(ITestRunUi testUi, [Named(inject.UiModule.LOCATION_BOTTOM)] IUiPositioner positioner) {
            this.testUi = testUi;
            this.positioner = positioner;
        }


        public void Run(model.ITest test) {
            testUi.StartNewTestRun(test);
            positioner.PositionWindow(testUi.AsWindow());
            
        }

    }
}
