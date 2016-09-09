using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Typing.DataAccess
{
    [Export(typeof(ITextProvider))]
    public class SimpleTextProvider : ITextProvider
    {
        public event EventHandler DataChanged;

        public SimpleTextProvider()
        {
            this.Sources = new List<ITextSource>();
            this.Title = "Simple Text";
            this.Description = "Built-in text provider";
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

        public void Refresh()
        {
            var sources = this.Sources as IList<ITextSource>;

            if (sources == null)
            {
                return;
            }

            sources.Clear();
            sources.Add(new SimpleTextSource("The Selfish Giant", "Oscar Wilde", "SelfishGiant"));
            sources.Add(new SimpleTextSource("Rime of the Ancient Mariner", "Samuel Taylor Coleridge", "AncientMariner"));

            EventHandler handler = this.DataChanged;
            if (handler != null)
            {
                var args = new EventArgs();
                handler(this, args);
            }
        }
    }
}
