using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.myecdl.model;

namespace at.myecdl.ui {
    public interface ITaskDetailUi : IUserInterface{
        void InitializeFor(ITask task);
    }
}
