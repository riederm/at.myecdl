using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.myecdl.ui.impl {
    public class TaskResultViewModel {

        public TaskResultViewModel() {
        }

        public TaskResultViewModel(string text, double points, double maxPoints) {
            this.Text = text;
            this.Points = points;
            this.MaxPoints = maxPoints;
        }

        public double Points { get; set; }

        public double MaxPoints { get; set; }

        public int Percentage {
            get {
                return Math.Min((int)((Points / MaxPoints)*100), 100);
            }
        }

        public string Text { get; set; }


    }
}
