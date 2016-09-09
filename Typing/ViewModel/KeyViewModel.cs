using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.Diagnostics;
using System.Globalization;

namespace Typing.ViewModel
{
    public class KeyViewModel : ObservableObject
    {
        private KeyHint keyState = 0;

        static KeyViewModel()
        {
            Scale = 40;
        }

        public KeyViewModel(Key key, IList<string> displayValues, double sizeHint)
        {
            this.Key = key;
            this.DisplayStrings = displayValues;
            this.WidthHint = sizeHint * Scale;
        }

        public KeyViewModel(Key key, string displayValue1, double sizeHint) : this(key, new List<string>(), sizeHint)
        {
            if (!String.IsNullOrEmpty(displayValue1))
            {
                this.DisplayStrings.Add(displayValue1);
            }
        }

        public KeyViewModel(Key key, string displayValue1, string displayValue2, double sizeHint)
            : this(key, displayValue1, sizeHint)
        {
            if (!String.IsNullOrEmpty(displayValue2))
            {
                this.DisplayStrings.Add(displayValue2);
            }
        }
        
        /// <summary>
        /// This overload is just to keep Microsoft warnings about default parameters away (apparently you shouldn't use them for public methods of public classes)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="displayValue1"></param>
        /// <param name="displayValue2"></param>
        public KeyViewModel(Key key, string displayValue1, string displayValue2)
            : this(key, displayValue1, displayValue2, 1.0)
        {
        }

        /// <summary>
        /// This overload is just to keep Microsoft warnings about default parameters away (apparently you shouldn't use them for public methods of public classes)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="displayValue"></param>
        public KeyViewModel(Key key, string displayValue)
            : this(key, displayValue, 1.0)
        {
        }

        public static double Scale
        {
            get;
            set;
        }

        public Key Key
        {
            get;
            set;
        }

        public KeyHint KeyState
        {
            get
            {
                return this.keyState;
            }

            set
            {
                this.keyState = value;
                this.RaisePropertyChanged("KeyState");
            }
        }

        public string NormalString()
        {
            if (this.DisplayStrings.Count == 2)
            {
                return this.DisplayStrings[1];
            }

            if (this.DisplayStrings.Count == 1)
            {
                if (DisplayStrings[0].Length == 1 && char.IsLetter(DisplayStrings[0], 0))
                {
                    return DisplayStrings[0].ToLower(CultureInfo.CurrentCulture);
                }
            }

            return DisplayStrings[0];
        }

        public string ShiftedString()
        {
            if (this.DisplayStrings.Count == 2)
            {
                return this.DisplayStrings[0];
            }

            if (this.DisplayStrings.Count == 1)
            {
                if (DisplayStrings[0].Length == 1 && char.IsLetter(DisplayStrings[0], 0))
                {
                    return DisplayStrings[0].ToUpper(CultureInfo.CurrentCulture);
                }
            }

            return DisplayStrings[0];
        }

        public IList<string> DisplayStrings
        {
            get;
            private set;
        }

        public double WidthHint
        {
            get;
            private set;
        }
    }
}
