using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace Typing.Model
{
    public class TypingStats : IDisposable
    {
        private readonly int maxSamples;
        private readonly Queue<double> samples;
        private readonly Stopwatch stopWatch;
        private int lastKeyCount;
        private long lastMilliseconds;
        private Timer timer;

        public TypingStats()
        {
            this.stopWatch = new Stopwatch();
            this.maxSamples = 20;
            this.samples = new Queue<double>();
            this.timer = new Timer(1000);
            this.timer.Elapsed += this.timer_Elapsed;
        }

        public int ErrorCount
        {
            get;
            set;
        }

        public int KeyCount
        {
            get;
            set;
        }

        public bool IsRunning
        {
            get
            {
                return this.timer.Enabled;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.RaiseStatsUpdated(this.GetInstantWpm(), this.ErrorCount, this.GetAccuracy());
        }

        public event EventHandler<StatsEventArgs> StatsUpdated;

        protected void RaiseStatsUpdated(int wpm, int errors, int accuracy)
        {
            EventHandler<StatsEventArgs> handler = this.StatsUpdated;
            if (handler != null)
            {
                var e = new StatsEventArgs(wpm, errors, accuracy);
                handler(this, e);
            }
        }

        public void Start()
        {
            this.KeyCount = 0;
            this.ErrorCount = 0;
            this.lastKeyCount = 0;
            this.lastMilliseconds = 0;
            this.samples.Clear();
            this.timer.Start();
            this.stopWatch.Restart();
        }

        public void Stop()
        {
            this.timer.Stop();
            this.stopWatch.Stop();
            this.RaiseStatsUpdated(this.GetAverageWpm(), this.ErrorCount, this.GetAccuracy());
        }

        private int GetAverageWpm()
        {
            long milliseconds = this.stopWatch.ElapsedMilliseconds;

            // Very unlikely
            if (milliseconds == 0)
            {
                return 0;
            }

            double keysPerMillisecond = this.KeyCount/(double) milliseconds;

            // Normalize to words per minute
            double wpm = keysPerMillisecond*60000/5;
            return Convert.ToInt32(wpm);
        }

        private int GetInstantWpm()
        {
            // Get the time interval in milliseconds since the last time we looked and reset the stopwatch
            long milliseconds = this.stopWatch.ElapsedMilliseconds;
            long millisecondsDelta = milliseconds - this.lastMilliseconds;
            this.lastMilliseconds = milliseconds;

            if (millisecondsDelta == 0)
            {
                return 0;
            }

            // Get the number of keystrokes sice last sample
            int keyCount = this.KeyCount;
            int keyCountDelta = keyCount - this.lastKeyCount;
            this.lastKeyCount = keyCount;

            // keep no more than maxSamples
            if (this.samples.Count >= this.maxSamples)
            {
                this.samples.Dequeue();
            }

            // save the number of correct keystrokes since the last sample
            this.samples.Enqueue(keyCountDelta/(double) millisecondsDelta);

            // Get the average of all samples
            double averageKeysPerMillisecond = this.samples.Average();

            if (averageKeysPerMillisecond == 0.0)
            {
                return 0;
            }

            // Normalize to words per minute
            double wpm = averageKeysPerMillisecond*60000/5;

            return Convert.ToInt32(wpm);
        }

        private int GetAccuracy()
        {
            if (this.ErrorCount == 0 || this.KeyCount == 0)
            {
                return 100;
            }

            double percent = ((this.KeyCount - this.ErrorCount)/(double) this.KeyCount)*100.0;

            return percent > 0 ? (int) percent : 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.timer != null)
                {
                    this.timer.Dispose();
                    this.timer = null;
                }
            }
        }
    }
}