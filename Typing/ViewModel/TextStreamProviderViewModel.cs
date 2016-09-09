using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Typing.Model;
using Typing.DataAccess;
using System.Windows.Threading;
using System.Threading;
using Typing.Properties;

namespace Typing.ViewModel
{
    public class TextStreamProviderViewModel : ObservableObject
    {
        private string providerTitle;
        private string providerDescription;
        private readonly ITextProvider provider;
        private TextStreamItemViewModel currentTextStream;

        public TextStreamProviderViewModel(ITextProvider provider)
        {
            this.TextStreams = new ObservableCollection<TextStreamItemViewModel>();

            if (provider == null)
            {
                throw new ArgumentNullException("provider", Resources.TextStreamProviderViewModel_TextStreamProviderViewModel_provider_cannot_be_null);
            }

            this.ProviderDescription = provider.Description;
            this.ProviderTitle = provider.Title;
            provider.DataChanged += new EventHandler(provider_DataChanged);
            
            this.provider = provider;
        }

        void provider_DataChanged(object sender, EventArgs e)
        {
            ITextProvider textProvider = sender as ITextProvider;
            this.ProviderTitle = textProvider.Title;
            this.ProviderDescription = textProvider.Description;

            this.TextStreams.Clear();
            foreach (ITextSource source in textProvider.Sources)
            {
                this.TextStreams.Add(new TextStreamItemViewModel(source));
            }

            if (this.CurrentTextStream == null)
            {
                this.CurrentTextStream = this.TextStreams.First();
            }
        }

        public TextStreamItemViewModel CurrentTextStream
        {
            get
            {
                return this.currentTextStream;
            }

            set
            {
                this.currentTextStream = value;
                if (this.currentTextStream != null)
                {
                    this.currentTextStream.Refresh();
                }
                this.RaisePropertyChanged("CurrentTextStream");
            }
        }

        public void Refresh()
        {
            this.provider.Refresh();
        }

        public ObservableCollection<TextStreamItemViewModel> TextStreams
        {
            get;
            private set;
        }

        public string ProviderTitle
        {
            get
            {
                return this.providerTitle;
            }

            set
            {
                this.providerTitle = value;
                this.RaisePropertyChanged("ProviderTitle");
            }
        }

        public string ProviderDescription
        {
            get
            {
                return this.providerDescription;
            }

            set
            {
                this.providerDescription = value;
                this.RaisePropertyChanged("ProviderDescription");
            }
        }
    }
}
