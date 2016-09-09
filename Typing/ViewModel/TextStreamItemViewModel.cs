using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;
using Typing.DataAccess;

namespace Typing.ViewModel
{
    public class TextStreamItemViewModel : ObservableObject
    {
        private string title;
        private string description;
        private readonly ITextSource textSource;

        public TextStreamItemViewModel(ITextSource textSource)
        {
            if (textSource != null)
            {
                this.Title = textSource.Title;
                this.Description = textSource.Description;
            }
            this.textSource = textSource;
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.RaisePropertyChanged("Title");
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                this.RaisePropertyChanged("Description");
            }
        }

        public string Text
        {
            get
            {
                if (this.textSource != null)
                {
                    return this.textSource.Text;
                }

                return null;
            }
        }

        public void Refresh()
        {
            this.textSource.Refresh();
        }
    }
}
