using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Typing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        private int Left
        {
            get;
            set;
        }

        private int Right
        {
            get;
            set;
        }

        private int Top
        {
            get;
            set;
        }

        private int Bottom
        {
            get;
            set;
        }

        public Margins(Thickness thickness)
            : this()
        {
            this.Left = (int) thickness.Left;
            this.Right = (int) thickness.Right;
            this.Top = (int) thickness.Top;
            this.Bottom = (int) thickness.Bottom;
        }
    }

    internal class SafeNativeMethods
    {
        private SafeNativeMethods()
        {
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmIsCompositionEnabled();
    }


    internal class GlassHelper
    {
        private GlassHelper()
        {
        }

        internal static bool ExtendGlassFrame(Window window, Thickness margin)
        {
            if (!SafeNativeMethods.DwmIsCompositionEnabled())
            {
                return false;
            }

            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero)
            {
                throw new InvalidOperationException("The Window must be shown before extending glass.");
            }

            // Set the background to transparent for both the WPF and Win32 perspectives
            window.Background = Brushes.Transparent;
            HwndSource hwndSource = HwndSource.FromHwnd(hwnd);
            if (hwndSource != null && hwndSource.CompositionTarget != null)
            {
                hwndSource.CompositionTarget.BackgroundColor = Colors.Transparent;
            }

            var margins = new Margins(margin);
            SafeNativeMethods.DwmExtendFrameIntoClientArea(hwnd, ref margins);
            return true;
        }
    }
}