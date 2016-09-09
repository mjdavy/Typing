using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Media;

namespace Typing
{
    public class TypingTextBox : TextBox
    {
        private int m_currentPos = 0;
        private TypingButton m_lastButton;

        public TypingTextBox()
        {
            IsReadOnly = true;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            ProcessInputText(e.Text);
            e.Handled = true;
        }

        protected override void  OnKeyDown(KeyEventArgs e)
        {
            if (MainWindow.keyButtonMap.Keys.Contains(e.Key))
            {
                TypingButton b = MainWindow.keyButtonMap[e.Key];

                if (m_lastButton != null)
                {
                    m_lastButton.IsError = false;
                }

                m_lastButton = b;
                b.IsActivated = true;
            }

 	        base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (MainWindow.keyButtonMap.Keys.Contains(e.Key))
            {
                TypingButton b = MainWindow.keyButtonMap[e.Key];
                b.IsActivated = false;
            }

            String strTarget = Text.Substring(m_currentPos, 1);

            if (e.Key == Key.Space)
            {
                ProcessInputText(" ");
            }
            base.OnKeyUp(e);
        }

        private void ProcessInputText(String inputText)
        {
            try
            {
                if (m_currentPos >= Text.Length - 1)
                {
                    // MJDTODO - What happens when you get to the end?
                    return;
                }

                String strTarget = Text.Substring(m_currentPos, 1);

                if (strTarget != inputText)
                {
                    SystemSounds.Beep.Play();
                    if (m_lastButton != null)
                    {
                        m_lastButton.IsError = true;
                    }

                    return;
                }

                m_currentPos++;
                Select(m_currentPos, 1);
                Focus();

                Point rightEdge = new Point(ActualWidth, 0);
                Point leftEdge = new Point(0, 0);
                int lastCharIndex = GetCharacterIndexFromPoint(rightEdge, true);
                int firstCharIndex = GetCharacterIndexFromPoint(leftEdge, true);
                int midCharIndex = firstCharIndex + (lastCharIndex - firstCharIndex) / 2;


                if (m_currentPos > midCharIndex)
                {
                   Rect r1 = GetRectFromCharacterIndex(m_currentPos);
                   Rect r2 = GetRectFromCharacterIndex(m_currentPos + 1); // MJDTODO - handle boundary condition
                
                   double offsetIncrement = r2.X - r1.X;
                   ScrollToHorizontalOffset(HorizontalOffset + offsetIncrement);
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
