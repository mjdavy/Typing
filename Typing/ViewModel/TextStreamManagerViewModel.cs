using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;
using MvvmFoundation.Wpf;

namespace Typing.ViewModel
{
    public class TextStreamManagerViewModel : ObservableObject
    {
        private TextStreamProviderViewModel currentTextStreamProvider;

        public TextStreamManagerViewModel()
        {
            this.TextStreamProviders = new ObservableCollection<TextStreamProviderViewModel>();
        }

        public TextStreamProviderViewModel CurrentTextStreamProvider
        {
            get
            {
                return this.currentTextStreamProvider;
            }

            set
            {
                if (value != null)
                {
                    this.currentTextStreamProvider = value;
                    this.currentTextStreamProvider.Refresh();
                    this.RaisePropertyChanged("CurrentTextStreamProvider");
                }
            }
        }

        public ObservableCollection<TextStreamProviderViewModel> TextStreamProviders
        {
            get;
            private set;
        }
    }
}
