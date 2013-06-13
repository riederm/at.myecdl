using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using at.myecdl.ui.impl;

namespace at.myecdl.ui.inject {
    public class UiModule : NinjectModule {

        public const string LOCATION_BOTTOM = "window.position.bottom";
        public const string LOCATION_MAXIMIZED = "window.position.max";
        public const string LOCATION_NORMAL = "window.position.normal";

        public override string Name {
            get {
                return GetType().Namespace;
            }
        }

        public override void Load() {

            Bind<ITestRunner>().To<TestRunnerImpl>();
            Bind<ITestRunUi>().To<TestRunWindow>();
            Bind<ISelectTestUi>().To<SelectTestUiImpl>();
            Bind<ITaskDetailUi>().To<QuestionaireWindow>();

            Bind<IUiPositioner>().To<BottomBarPositioner>().Named(LOCATION_BOTTOM);
            Bind<IUiPositioner>().To<WindowMaximizer>().Named(LOCATION_MAXIMIZED);
            Bind<IUiPositioner>().To<EmptyUiPositioner>().Named(LOCATION_NORMAL);

            
        }
    }
}
