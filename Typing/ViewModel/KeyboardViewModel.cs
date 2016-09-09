using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Typing.Model;
using Typing.Properties;

namespace Typing.ViewModel
{
    public enum KeyHint
    {
        Normal,
        Expected,
        Error
    };

    public class KeyboardViewModel
    {
        private Dictionary<Key, KeyViewModel> keyMap;
        private Dictionary<string, IList<Key>> charMap;
        private IList<KeyViewModel> highlightedKeys;
        private readonly TextStreamModel textStream;
        
        public KeyboardViewModel(TextStreamModel textStream)
        {
            if (textStream == null)
            {
                throw new ArgumentNullException("textStream", Resources.KeyboardViewModel_KeyboardViewModel_This_Viewmodel_must_be_initialized_with_a_TextStreamModel);
            }

            this.textStream = textStream;
            this.textStream.TextExpected += new EventHandler<TextEventArgs>(textStream_TextExpected);
            LoadKeyboard();
            BuildKeyMap();
        }

        private void textStream_TextExpected(object sender, TextEventArgs obj)
        {
            if (obj.Entered == null || obj.Entered == obj.Expected)
            {
                this.HighlightKeys(obj.Expected, KeyHint.Expected);
            }
            else
            {
                this.HighlightKeys(obj.Expected, KeyHint.Error); // MJDTODO - highlight the wrong key they typed 
            }
        }

        public void HighlightKeys(string text, KeyHint hint)
        {
            if (this.highlightedKeys == null)
            {
                this.highlightedKeys = new List<KeyViewModel>();
            }
            else
            {
                foreach (KeyViewModel kvm in this.highlightedKeys)
                {
                    kvm.KeyState = KeyHint.Normal;
                }
                this.highlightedKeys.Clear();
            }

            if (this.charMap.ContainsKey(text))
            {
                IList<Key> keys = this.charMap[text];

                foreach (Key key in keys)
                {
                    if (this.keyMap.ContainsKey(key))
                    {
                        keyMap[key].KeyState = hint;
                        this.highlightedKeys.Add(keyMap[key]);
                    }
                }
            }
        }

        // MJDODO - need to move all this stuff out to a model when load keyboard def from file is implemented
        private void BuildKeyMap()
        {
            this.keyMap = new Dictionary<Key, KeyViewModel>();
            this.charMap = new Dictionary<string, IList<Key>>();

            for (int r = 0; r < this.KeyboardRows.Count; r++)
            {
                KeyboardRowViewModel row = KeyboardRows[r];

                for (int k = 0; k < row.Keys.Count; k++)
                {
                    KeyViewModel kvm = row.Keys[k];

                    if (keyMap.ContainsKey(kvm.Key))
                    {
                        continue;
                    }
                    keyMap[kvm.Key] = kvm;

                    string normal = kvm.NormalString();
                    string shifted = kvm.ShiftedString();

                    if (!charMap.ContainsKey(normal))
                    {
                        List<Key> keys = new List<Key>();
                        keys.Add(kvm.Key);
                        charMap[normal] = keys;
                    }

                    if (!charMap.ContainsKey(shifted))
                    {
                        List<Key> keys = new List<Key>();

                        int leftOrRight = (r < 2) ? row.Keys.Count / 2 - 1 : row.Keys.Count / 2;

                        if (k < leftOrRight)
                        {
                            keys.Add(Key.RightShift);
                        }
                        else
                        {
                            keys.Add(Key.LeftShift);
                        }
                        keys.Add(kvm.Key);
                        charMap[shifted] = keys;
                    }
                }
            }
        }

        /// <summary>
        /// Load the default virtual keyboard
        /// </summary>
        private void LoadKeyboard()
        {
            KeyboardRowViewModel row0 = new KeyboardRowViewModel("Row 0");
            row0.Keys.Add(new KeyViewModel(Key.OemTilde, "~", "`"));
            row0.Keys.Add(new KeyViewModel(Key.D1, "!", "1"));
            row0.Keys.Add(new KeyViewModel(Key.D2, "@", "2"));
            row0.Keys.Add(new KeyViewModel(Key.D3, "#", "3"));
            row0.Keys.Add(new KeyViewModel(Key.D4, "$", "4"));
            row0.Keys.Add(new KeyViewModel(Key.D5, "%", "5"));
            row0.Keys.Add(new KeyViewModel(Key.D6, "^", "6"));
            row0.Keys.Add(new KeyViewModel(Key.D7, "&", "7"));
            row0.Keys.Add(new KeyViewModel(Key.D8, "*", "8"));
            row0.Keys.Add(new KeyViewModel(Key.D9, "(", "9"));
            row0.Keys.Add(new KeyViewModel(Key.D0, ")", "0"));
            row0.Keys.Add(new KeyViewModel(Key.OemMinus, "_", "-"));
            row0.Keys.Add(new KeyViewModel(Key.OemPlus, "+", "="));
            row0.Keys.Add(new KeyViewModel(Key.Back, "Backspace", 2.0));

            KeyboardRowViewModel row1 = new KeyboardRowViewModel("Row 1");
            row1.Keys.Add(new KeyViewModel(Key.Tab, "Tab", 1.5));
            row1.Keys.Add(new KeyViewModel(Key.Q, "Q"));
            row1.Keys.Add(new KeyViewModel(Key.W, "W"));
            row1.Keys.Add(new KeyViewModel(Key.E, "E"));
            row1.Keys.Add(new KeyViewModel(Key.R, "R"));
            row1.Keys.Add(new KeyViewModel(Key.T, "T"));
            row1.Keys.Add(new KeyViewModel(Key.Y, "Y"));
            row1.Keys.Add(new KeyViewModel(Key.U, "U"));
            row1.Keys.Add(new KeyViewModel(Key.I, "I"));
            row1.Keys.Add(new KeyViewModel(Key.O, "O"));
            row1.Keys.Add(new KeyViewModel(Key.P, "P"));
            row1.Keys.Add(new KeyViewModel(Key.Oem4, "{", "["));
            row1.Keys.Add(new KeyViewModel(Key.Oem6, "}", "]"));
            row1.Keys.Add(new KeyViewModel(Key.OemPipe, "|", @"\", 1.5));

            KeyboardRowViewModel row2 = new KeyboardRowViewModel("Row 2");
            row2.Keys.Add(new KeyViewModel(Key.CapsLock, "Caps Lock", 1.5));
            row2.Keys.Add(new KeyViewModel(Key.A, "A"));
            row2.Keys.Add(new KeyViewModel(Key.S, "S"));
            row2.Keys.Add(new KeyViewModel(Key.D, "D"));
            row2.Keys.Add(new KeyViewModel(Key.F, "F"));
            row2.Keys.Add(new KeyViewModel(Key.G, "G"));
            row2.Keys.Add(new KeyViewModel(Key.H, "H"));
            row2.Keys.Add(new KeyViewModel(Key.J, "J"));
            row2.Keys.Add(new KeyViewModel(Key.K, "K"));
            row2.Keys.Add(new KeyViewModel(Key.L, "L"));
            row2.Keys.Add(new KeyViewModel(Key.OemSemicolon, ":", ";"));
            row2.Keys.Add(new KeyViewModel(Key.Oem7, "\"", "'"));
            row2.Keys.Add(new KeyViewModel(Key.Return, "Enter", 2.5));

            KeyboardRowViewModel row3 = new KeyboardRowViewModel("Row 3");
            row3.Keys.Add(new KeyViewModel(Key.LeftShift, "Shift", 2.5));
            row3.Keys.Add(new KeyViewModel(Key.Z, "Z"));
            row3.Keys.Add(new KeyViewModel(Key.X, "X"));
            row3.Keys.Add(new KeyViewModel(Key.C, "C"));
            row3.Keys.Add(new KeyViewModel(Key.V, "V"));
            row3.Keys.Add(new KeyViewModel(Key.B, "B"));
            row3.Keys.Add(new KeyViewModel(Key.N, "N"));
            row3.Keys.Add(new KeyViewModel(Key.M, "M"));
            row3.Keys.Add(new KeyViewModel(Key.OemComma, "<", ","));
            row3.Keys.Add(new KeyViewModel(Key.OemPeriod, ">", "."));
            row3.Keys.Add(new KeyViewModel(Key.Oem2, "?", "/"));
            row3.Keys.Add(new KeyViewModel(Key.RightShift, "Shift", 2.5));

            KeyboardRowViewModel row4 = new KeyboardRowViewModel("Row 4");
            row4.Keys.Add(new KeyViewModel(Key.LeftCtrl, "Ctrl", 3.0));
            row4.Keys.Add(new KeyViewModel(Key.LeftAlt, "Alt", 1.5));
            row4.Keys.Add(new KeyViewModel(Key.Space, " ", 6.35));
            row4.Keys.Add(new KeyViewModel(Key.RightAlt, "Alt", 1.5));
            row4.Keys.Add(new KeyViewModel(Key.RightCtrl, "Ctrl", 3.0));

            this.KeyboardRows = new List<KeyboardRowViewModel>();
            this.KeyboardRows.Add(row0);
            this.KeyboardRows.Add(row1);
            this.KeyboardRows.Add(row2);
            this.KeyboardRows.Add(row3);
            this.KeyboardRows.Add(row4);
        }

        public IList<KeyboardRowViewModel> KeyboardRows
        {
            get;
            private set;
        }
    }
}
