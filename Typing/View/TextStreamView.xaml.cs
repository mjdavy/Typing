using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Typing.ViewModel;
using System.Diagnostics;

namespace Typing.View
{
    /// <summary>
    /// Interaction logic for TextStreamView.xaml
    /// </summary>
    public partial class TextStreamView : UserControl
    {
        public TextStreamView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TextBox.Select(0, 1);
            this.TextBox.ScrollToHome();
            this.TextBox.Focus();
        }

        /// <summary>
        /// Definitely a lot easier to handle events in code behind
        /// </summary>
        /// <param name="input"></param>
        private void HandleInput(string input)
        {
            TextStreamViewModel vm = this.DataContext as TextStreamViewModel;
            if (vm == null)
            {
                Debug.Assert(false, "Viewmodel should never be null. Check DataContext data type");
                return;
            }

            vm.ProcessInput(input);
            this.TextBox.Select(vm.CharacterIndex, 1);
            this.TextBox.Focus();
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            TextStreamViewModel vm = this.DataContext as TextStreamViewModel;
            if (vm == null)
            {
                Debug.Assert(false, "Viewmodel should never be null. Check DataContext data type");
                return;
            }

            // Special case for space. Find a more elegant way to do this - MJDTODO
            // maybe we should handle all input at the main window level and avoid focus issues.
            if (e.Key == Key.Space)
            {
                this.HandleInput(" ");
            }
        }

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            this.HandleInput(e.Text);
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void TextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void TextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            this.TextBox.Focus();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null)
            {
                Debug.Assert(false, "Incorrect type?");
                return;
            }

            Point rightEdge = new Point(ActualWidth, 0);
            Point leftEdge = new Point(0, 0);
            int lastCharIndex = tb.GetCharacterIndexFromPoint(rightEdge, true);
            int firstCharIndex = tb.GetCharacterIndexFromPoint(leftEdge, true);
            int midCharIndex = firstCharIndex + (lastCharIndex - firstCharIndex) / 2;

            // All the text is already scrolled into view
            if (lastCharIndex == tb.Text.Length - 1)
            {
                return;
            }

            if (tb.SelectionStart > midCharIndex)
            {
                Rect r1 = this.TextBox.GetRectFromCharacterIndex(tb.SelectionStart);
                Rect r2 = this.TextBox.GetRectFromCharacterIndex(tb.SelectionStart + 1); 

                double offsetIncrement = r2.X - r1.X;
                this.TextBox.ScrollToHorizontalOffset(this.TextBox.HorizontalOffset + offsetIncrement);
            }
        }
    }
}
