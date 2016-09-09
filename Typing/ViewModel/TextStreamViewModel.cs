using System;
using MvvmFoundation.Wpf;
using Typing.Model;
using Typing.Properties;

namespace Typing.ViewModel
{
    public class TextStreamViewModel : ObservableObject
    {
        private readonly TextStreamModel model;

        public TextStreamViewModel(TextStreamModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model", Resources.TextStreamViewModel_TextStreamViewModel_This_viewmodel_must_be_initialed_with_a_model);
            }

            this.model = model;
            model.TextExpected += this.model_TextExpected;
        }

        public TextStreamViewModel()
        {
            this.model = new TextStreamModel();
            this.model.TextExpected += this.model_TextExpected;
        }

        public string Title
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        public string Text
        {
            get
            {
                return this.model.Text;
            }
            set
            {
                this.model.Text = value;
                this.CharacterIndex = 0;
                this.RaisePropertyChanged("Text");
            }
        }

        public int CharacterIndex
        {
            get
            {
                return this.model.CharacterIndex;
            }
            set
            {
                this.model.CharacterIndex = value;
                this.RaisePropertyChanged("CharacterIndex");
            }
        }

        private void model_TextExpected(object sender, TextEventArgs obj)
        {
            this.CharacterIndex = this.model.CharacterIndex;
        }

        public void ProcessInput(string inputText)
        {
            this.model.ProcessInput(inputText);
        }
    }
}