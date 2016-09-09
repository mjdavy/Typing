using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Typing.DataAccess
{
    public class YahooNewsTextSource : ITextSource, IAsyncProvider
    {
        private readonly string link;
        private readonly AsyncTextWorker worker;

        public event EventHandler DataChanged;

        public YahooNewsTextSource(string title, string description, string link)
        {
            this.Title = title;
            this.Description = description;
            this.link = link;
            this.worker = new AsyncTextWorker(this);
        }


        private static string ExtractStoryBody(String html)
        {
            if (String.IsNullOrEmpty(html))
            {
                return string.Empty;
            }

            // Extract the body with markup
            var rgxBody = new Regex("<div class=\"yom-mod yom-art-content {ctx.media.modules.article.article_body.fontsize}\">(?<Body>.+?)</div>", RegexOptions.Singleline);
            Match m = rgxBody.Match(html);
            if (m.Success)
            {
                String xmlText = m.Groups["Body"].Value;

                // Strip out any markup and other crap
                string plain = Regex.Replace(xmlText, "<.+?>|\n|\t", "", RegexOptions.Singleline);
                plain = RssTextHelper.ReplaceSpecialChars(plain);
                return plain;
            }

            return "Unable to parse HTML";
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

        public string Text
        {
            get;
            set;
        }

        public void Refresh()
        {
            this.worker.Refresh(); // Async
        }

        public void Initialize()
        {
           
        }

        public void BeginRefresh()
        {
            string htmlText = RssTextHelper.ExtractHtmlText(new Uri(link));
            this.Text = ExtractStoryBody(htmlText);
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
    }
}
