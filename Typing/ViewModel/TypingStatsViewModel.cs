using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;
using System.Windows.Input;
using Typing.Model;
using System.Timers;
using System.Globalization;

namespace Typing.ViewModel
{
    public class TypingStatsViewModel : ObservableObject, IDisposable
    {
        private RelayCommand startStopCommand;
        private TypingStats typingStats;
        private string wordsPerMinute;
        private string errors;
        private string accuracy;
        private bool isRunning;
        
        public TypingStatsViewModel()
        {
            this.Reset();
            this.typingStats = new TypingStats();
            this.typingStats.StatsUpdated += new EventHandler<StatsEventArgs>(typingStats_StatsUpdated);
        }

        private void typingStats_StatsUpdated(object sender, StatsEventArgs obj)
        {
            this.Accuracy = string.Concat(obj.Accuracy + "%");
            this.Errors = obj.Errors.ToString(CultureInfo.CurrentCulture);
            this.WordsPerMinute = obj.WordsPerMinute.ToString(CultureInfo.CurrentCulture);
        }

        public void OnTextExpected(object sender, TextEventArgs obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj","TextEventArgs argument must be non-null");
            }

            if (obj.Entered == null || obj.Entered == obj.Expected)
            {
                this.typingStats.KeyCount++;
            }
            else
            {
                this.typingStats.ErrorCount++;
            }
        }

        public string Accuracy
        {
            get
            {
                return this.accuracy;
            }

            set
            {
                this.accuracy = value;
                this.RaisePropertyChanged("Accuracy");
            }
        }

        public string WordsPerMinute
        {
            get
            {
                return this.wordsPerMinute;
            }

            set
            {
                this.wordsPerMinute = value;
                this.RaisePropertyChanged("WordsPerMinute");
            }
        }

        public string Errors
        {
            get
            {
                return this.errors;
            }

            set
            {
                this.errors = value;
                this.RaisePropertyChanged("Errors");
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                this.isRunning = value;
                this.RaisePropertyChanged("IsRunning");
            }
        }

        public ICommand StartStopCommand
        {
            get
            {
                if (startStopCommand == null)
                {
                    startStopCommand = new RelayCommand(this.StartStop, this.CanStartStop);
                }
                return this.startStopCommand;
            }
        }

        public void StartStop()
        {
            if (this.typingStats.IsRunning)
            {
                this.typingStats.Stop();
            }
            else
            {
                this.Reset();
                this.typingStats.Start();
            }

            this.IsRunning = this.typingStats.IsRunning;
        }

        private bool CanStartStop()
        {
            return true;
        }

        private void Reset()
        {
            if (this.IsRunning == false)
            {
                this.Accuracy = "100%";
                this.WordsPerMinute = "0";
                this.Errors = "0";
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.typingStats != null)
                {
                    this.typingStats.Dispose();
                    this.typingStats = null;
                }
            }
        }
    }
}
