using System;
using System.ComponentModel;
using Typing.DataAccess.Properties;

namespace Typing.DataAccess
{
    public class AsyncTextWorker
    {
        private readonly IAsyncProvider provider;
        private readonly BackgroundWorker worker;
        private int count = 0;

        public AsyncTextWorker(IAsyncProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider", Resources.AsyncTextWorker_AsyncTextWorker_provider_cannot_be_null);
            }

            this.worker = new BackgroundWorker();
            this.worker.DoWork += this.DoWork;
            this.worker.RunWorkerCompleted += this.RunWorkerCompleted;
            this.provider = provider;
        }

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            provider.EndRefresh();

            if (count > 0)
            {
                count--;
                Refresh();
            }
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            provider.BeginRefresh();
        }

        public void Refresh()
        {
            provider.Initialize();

            if (worker.IsBusy)
            {
                count++;
                return;
            }
            this.worker.RunWorkerAsync();
        }
    }
}
