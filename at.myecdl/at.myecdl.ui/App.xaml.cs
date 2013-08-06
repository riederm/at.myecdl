using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Ninject;
using Ninject.Parameters;
using Ninject.Modules;
using at.myecdl.ui;
using at.myecdl.util;
using at.myecdl.model;
using at.myecdl.ui.inject;
using at.myecdl.model.inject;
using at.myecdl.ui.impl;

namespace at.myecdl.ui {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            IKernel kernel = new StandardKernel();
            kernel.Load(new INinjectModule[] { new UiModule(), new ModelModule()  });
            /*
            var testSelector = kernel.Get<ISelectTestUi>();
            testSelector.Show();*/
            var resultWindow = new TestResultWindow();

            var result = new TaskResultViewModel("myText lorm ipsum blub blub lbu fdafjd ja sfd af myText lorm ipsum ...", 2, 3);
            resultWindow.setInput(new TaskResultViewModel[] { result }.ToList());
            resultWindow.Show(); 
            
            base.OnStartup(e);
        }
    }
}
