using System;
using System.Windows;
using System.Windows.Interop;

namespace Typing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : System.Windows.Window
    {
// ReSharper disable InconsistentNaming
        private const int WM_DWMCOMPOSITIONCHANGED = 0x031E;
// ReSharper restore InconsistentNaming

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Override for glass look and feel
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSourceInitialized(System.EventArgs e)
        {
            base.OnSourceInitialized(e);


            try
            {

                // This can not be done any earlier than the SourceInitialized event:
                GlassHelper.ExtendGlassFrame(this, new Thickness(-1));

                // Attach a window procedure in order to detect later enabling of desktop composition
                IntPtr hwnd = new WindowInteropHelper(this).Handle;

                var hwndSource = HwndSource.FromHwnd(hwnd);
                if (hwndSource != null)
                {
                    hwndSource.AddHook(new HwndSourceHook(WndProc));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Override for glass look and feel
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_DWMCOMPOSITIONCHANGED)
            {
                // Re-enable glass:
                GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void ShowTextProviders(object sender, RoutedEventArgs e)
        {
            this.TypingView.Visibility = Visibility.Hidden;
            this.TextStreamManagerView.Visibility = Visibility.Visible;
        }
       
    }
}