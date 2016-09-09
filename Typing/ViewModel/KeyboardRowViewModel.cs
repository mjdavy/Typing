using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typing.ViewModel
{
    public class KeyboardRowViewModel
    {
        public KeyboardRowViewModel()
        {
            this.Keys = new List<KeyViewModel>();
        }

        public KeyboardRowViewModel(string name) : this()
        {
            this.Name = name;
        }

        public IList<KeyViewModel> Keys
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
