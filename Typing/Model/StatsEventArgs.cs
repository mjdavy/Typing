using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typing.Model
{
    public class StatsEventArgs : EventArgs
    {
        public StatsEventArgs(int wpm, int errors, int accuracy)
        {
            this.WordsPerMinute = wpm;
            this.Errors = errors;
            this.Accuracy = accuracy;
        }

        public int WordsPerMinute
        {
            get;
            private set;
        }

        public int Errors
        {
            get;
            private set;
        }

        public int Accuracy
        {
            get;
            private set;
        }
    }
}
