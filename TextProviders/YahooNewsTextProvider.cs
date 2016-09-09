using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Typing.DataAccess
{
    [Export(typeof (ITextProvider))]
    public class YahooNewsTextProvider : ITextProvider, IAsyncProvider
    {
        private readonly AsyncTextWorker worker;

        public YahooNewsTextProvider()
        {
            this.Sources = new List<ITextSource>();
            this.BaseUri = @"http://news.yahoo.com/rss/";
            this.Title = "Yahoo News";
            this.Description = "Loading...";
            this.worker = new AsyncTextWorker(this);
        }

        private string BaseUri
        {
            get;
            set;
        }

        #region IAsyncProvider Members

        public void Initialize()
        {
        }

        public void BeginRefresh()
        {
            try
            {
                // ReSharper disable PossibleNullReferenceException
                var sourceList = this.Sources as IList<ITextSource>;
                sourceList.Clear();

                XElement rssXml = XElement.Load(this.BaseUri);
                XElement channel = rssXml.Descendants("channel").First();

                this.Title = channel.Element("title").Value.Trim();
                this.Description = channel.Element("description").Value.Trim();

                IEnumerable<ITextSource> sources = from source in channel.Descendants("item")
                                                   select new YahooNewsTextSource(
                                                       source.Element("title").Value.Trim(),
                                                       this.ExtractDate(source.Element("pubDate").Value.Trim()),
                                                       source.Element("link").Value);

                // ReSharper restore PossibleNullReferenceException
                foreach (ITextSource item in sources)
                {
                    sourceList.Add(item);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EndRefresh()
        {
            EventHandler handler = this.DataChanged;
            if (handler != null)
            {
                var args = new EventArgs();
                handler(this, args);
            }
        }

        #endregion

        #region ITextProvider Members

        public event EventHandler DataChanged;

        public void Refresh()
        {
            this.worker.Refresh(); // Async
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

        public IEnumerable<ITextSource> Sources
        {
            get;
            private set;
        }

        #endregion

        private string ExtractDate(string p)
        {
            DateTime pubdate;
            string pubDateString = string.Empty;

            if (DateTime.TryParse(p, out pubdate))
            {
                pubDateString = pubdate.ToString("F", CultureInfo.CurrentCulture);
            }

            return pubDateString;
        }
    }
}