using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace at.myecdl.ui {
    public interface ITestRunUi : IUserInterface {
        event EventHandler SubmitClicked;

        event EventHandler SkipClicked;

        event EventHandler EndClicked;

        void UpdateCurrentTaskDescription(string text);

        void UpdateTimeLeft(TimeSpan timeLeft);

        void UpdateProgress(int currentStep, int maxSteps);

        void EnableButtons(bool submitButton, bool skipButton, bool endButton);

    }
}
