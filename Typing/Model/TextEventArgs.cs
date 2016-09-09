using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typing.Model
{
    public class TextEventArgs : EventArgs
    {
        public TextEventArgs(string expected)
        {
            this.Expected = expected;
            this.Entered = null;
        }

        public TextEventArgs(string expected, string entered)
        {
            this.Expected = expected;
            this.Entered = entered;
        }

        public string Expected
        {
            get;
            private set;
        }

        public string Entered
        {
            get;
            private set;
        }
    }
}
