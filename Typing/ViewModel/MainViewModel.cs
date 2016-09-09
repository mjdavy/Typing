using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;
using Typing.Model;
using System.Windows.Input;
using Typing.DataAccess;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;

namespace Typing.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private ICommand showKeyboardCommand;
        private ICommand showTextStreamManagerCommand;
        private bool keyBoardVisible = true;

        private CompositionContainer container;

        [ImportMany(typeof(ITextProvider))]
        public IEnumerable<ITextProvider> TextProviders
        {
            get;
            set;
        }

        private void Compose()
        {
            // An aggregate catalog that combines multiple catalogs
            using (var catalog = new AggregateCatalog())
            {

                // Adds all the parts found in the same assembly as the DataProvider class
                catalog.Catalogs.Add(new AssemblyCatalog(typeof(Typing.DataAccess.SimpleTextProvider).Assembly));

                // MJDTODO - figure out where to find extensions
                //catalog.Catalogs.Add(new DirectoryCatalog(@"C:\Work\RND\SimpleCalculator\CS\SimpleCalculator3\Extensions"));

                //Create the CompositionContainer with the parts in the catalog
                using (this.container = new CompositionContainer(catalog))
                {
                    //Fill the imports of this object
                    try
                    {
                        this.container.ComposeParts(this);
                    }
                    catch (CompositionException compositionException)
                    {
                        Console.WriteLine(compositionException.ToString());
                    }
                }
            }
        }

        public MainViewModel()
        {
            this.Compose();
            TextStreamModel textStreamModel = new TextStreamModel();
            this.KeyboardViewModel = new KeyboardViewModel(textStreamModel);
            this.TextStreamViewModel = new TextStreamViewModel(textStreamModel);
            this.TypingStatsViewModel = new TypingStatsViewModel();
            this.TextStreamManagerViewModel = new TextStreamManagerViewModel();
            textStreamModel.TextExpected += new EventHandler<TextEventArgs>(this.TypingStatsViewModel.OnTextExpected);

            ITextProvider defaultProvider = null;
            foreach (ITextProvider provider in this.TextProviders)
            {
                TextStreamProviderViewModel providerViewmodel = new TextStreamProviderViewModel(provider);
                this.TextStreamManagerViewModel.TextStreamProviders.Add(providerViewmodel);
                if (provider is SimpleTextProvider)
                {
                    defaultProvider = provider;
                }
            }
            this.TextStreamManagerViewModel.CurrentTextStreamProvider = this.TextStreamManagerViewModel.TextStreamProviders.First();

            defaultProvider.Refresh();
            ITextSource defaultTextSource = defaultProvider.Sources.First();

            if (defaultTextSource != null)
            {
                this.TextStreamViewModel.Text = defaultTextSource.Text;
            }
        }

        public ICommand ShowTextStreamManager
        {
            get
            {
                if (this.showTextStreamManagerCommand == null)
                {
                    this.showTextStreamManagerCommand = new RelayCommand(this.FlipView, this.CanFlipView);
                }
                return this.showTextStreamManagerCommand;
            }
        }

        public ICommand ShowKeyboard
        {
            get
            {
                if (this.showKeyboardCommand == null)
                {
                    this.showKeyboardCommand = new RelayCommand(this.FlipView, this.CanFlipView);
                }
                return this.showKeyboardCommand;
            }
        }

        public TypingStatsViewModel TypingStatsViewModel
        {
            get;
            set;
        }

        public TextStreamManagerViewModel TextStreamManagerViewModel
        {
            get;
            set;
        }

        public TextStreamViewModel TextStreamViewModel
        {
            get;
            set;
        }

        public KeyboardViewModel KeyboardViewModel
        {
            get;
            set;
        }

        public bool IsKeyboardVisible
        {
            get
            {
                return this.keyBoardVisible;
            }
        }

        public bool IsTextStreamManagerVisible
        {
            get
            {
                return !this.keyBoardVisible;
            }
        }

        private void SetTextStream()
        {
            if (this.keyBoardVisible)
            {
                TextStreamProviderViewModel currentTextStreamProvider = this.TextStreamManagerViewModel.CurrentTextStreamProvider;
                if (currentTextStreamProvider != null && currentTextStreamProvider.CurrentTextStream != null)
                {
                    this.TextStreamViewModel.Text = currentTextStreamProvider.CurrentTextStream.Text;
                }
            }
        }

        private void FlipView()
        {
            this.keyBoardVisible = !keyBoardVisible;
            this.RaisePropertyChanged("IsKeyboardVisible");
            this.RaisePropertyChanged("IsTextStreamManagerVisible");
            this.SetTextStream();
        }

        private bool CanFlipView()
        {
            return true;
        }
    }
}
