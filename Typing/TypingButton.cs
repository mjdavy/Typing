using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Typing
{
    public class TypingButton : Button
    {
        private Key m_key;
  
        public Key TypingKey
        {
            get { return m_key; }
            set { m_key = value; }
        }

        public bool IsActivated
        {
            get { return (bool)GetValue(IsActivatedProperty); }
            set { SetValue(IsActivatedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActivated.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActivatedProperty =
            DependencyProperty.Register("IsActivated", typeof(bool), typeof(TypingButton), new UIPropertyMetadata(false));

        public bool IsError
        {
            get { return (bool)GetValue(IsErrorProperty); }
            set { SetValue(IsErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsError.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsErrorProperty =
            DependencyProperty.Register("IsError", typeof(bool), typeof(TypingButton), new UIPropertyMetadata(false));


        protected override void OnClick()
        {
            //MessageBox.Show("TypingButton.OnClick");
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
        }
    }
}
