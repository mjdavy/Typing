using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.IO;
using System.Text.RegularExpressions;

namespace Typing.DataAccess
{
    public class SimpleTextSource : ITextSource
    {
        private readonly string textResource;

        public event EventHandler DataChanged;

        public SimpleTextSource(string title, string description, string textResource)
        {
            this.Description = description;
            this.Title = title;
            this.textResource = textResource;
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


        private static string CleanupText(string text)
        {
            text = Regex.Replace(text, @"\n", " ");
            text = Regex.Replace(text, @"\r", " ");
            text = Regex.Replace(text, @"\s+", " ");
            return text;
        }

        public void Refresh()
        {
            try
            {
                string rawText = Properties.Resources.ResourceManager.GetString(this.textResource);
                this.Text = CleanupText(rawText);
                this.EndRefresh();
            }
            catch (Exception ex)
            {
                this.Text = "Unable to load text. " + ex.Message;
            }
        }

        private void EndRefresh()
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
