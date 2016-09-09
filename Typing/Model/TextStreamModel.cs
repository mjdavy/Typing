using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Globalization;

namespace Typing.Model
{
    public class TextStreamModel
    {
        private string text;

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;

                if (!String.IsNullOrEmpty(this.text))
                {
                    this.RaiseTextExpected(this.text[0].ToString(), null);
                }
            }
        }

        public int CharacterIndex
        {
            get;
            set;
        }

        public event EventHandler<TextEventArgs> TextExpected;

        /// <summary>
        /// Raises this object's RaiseTextExpected event.
        /// </summary>
        /// <param name="expected">The expected text</param>
        /// <param name="entered">The entered text</param>
        protected void RaiseTextExpected(string expected, string entered)
        {
            EventHandler<TextEventArgs> handler = this.TextExpected;
            if (handler != null)
            {
                var e = new TextEventArgs(expected, entered);
                handler(this, e);
            }
        }

        public void ProcessInput(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return;
            }

            if (string.IsNullOrEmpty(this.Text))
            {
                return;
            }

            if (this.CharacterIndex >= this.Text.Length - 1)
            {
                return;
            }

            string expected = this.Text[this.CharacterIndex].ToString();
            string entered = inputText[0].ToString();

            if (expected == entered)
            {
                this.CharacterIndex++;
                expected = this.Text[this.CharacterIndex].ToString();
                RaiseTextExpected(expected, null);
            }
            else
            {
                RaiseTextExpected(expected, entered);
            }
        }

    }
}
