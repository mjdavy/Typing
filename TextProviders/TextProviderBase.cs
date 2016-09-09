using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Net;
using System.ComponentModel.Composition;
using System.ComponentModel;

namespace Typing.DataAccess
{
    public abstract class TextProviderBase : ITextProvider
    {
        private BackgroundWorker worker;

        public event EventHandler DataChanged;

        public TextProviderBase()
        {
            this.Sources = new List<ITextSource>();
            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.EndRefresh();

            EventHandler handler = this.DataChanged;
            if (handler != null)
            {
                var args = new EventArgs();
                handler(this, args);
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.BeginRefresh();
        }

        public void Refresh()
        {
            this.Initialize();
            this.worker.RunWorkerAsync();
        }

        public string Title
        {
            get;
            set;
        }

        public string Description 
        { 
            get; 
            set; 
        }

        public IList<ITextSource> Sources
        {
            get;
            private set;
        }

        abstract protected void BeginRefresh();
        abstract protected void EndRefresh();
        abstract public void Initialize();
    }
}
