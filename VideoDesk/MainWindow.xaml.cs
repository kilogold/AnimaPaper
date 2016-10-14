using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using FirstFloor.ModernUI.Windows.Controls;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace VideoDesk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public static String file;
        public static Uri fileMedia;
        public static Window win2;
        public static MediaElement media = null;

        public static List<Window> windowList;
        public static List<System.Drawing.Rectangle> ScreenList;
        public static List<System.Windows.Controls.Button> ButtonList;


        public static IntPtr workerw;
        public static IntPtr workerwHidden;


        public static MediaPlayer player; //for thumbnails

        public static bool soundOrNot;
        public static bool currentlyPlaying;
        System.Windows.Forms.NotifyIcon ni;
        public MainWindow()
        {
            InitializeComponent();

            media = null;
            soundOrNot = false;
            currentlyPlaying = false;
            player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };
            //this.WindowStyle = WindowStyle.SingleBorderWindow;

            ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("./AnimePaper.ico");

            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };



            windowList = new List<Window>();
            ScreenList = new List<System.Drawing.Rectangle>();
            ButtonList = new List<System.Windows.Controls.Button>();
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                ScreenList.Add(Screen.AllScreens[i].WorkingArea);
            }
            for (int i = 0; i < MainWindow.ScreenList.Count; i++)
            {
                windowList.Add(new Window());
            }

            for (int i = 0; i < MainWindow.ScreenList.Count; i++)
            {
                ButtonList.Add(new System.Windows.Controls.Button());
                ButtonList[i].Name = "Screen" + i.ToString();
                ButtonList[i].Content = "Screen " + i.ToString();
                
            }
        }

        private void Anime_Paper_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                ni.BalloonTipTitle = "Anime Paper";
                ni.BalloonTipText = "Anime Paper is minimized";
                ni.Visible = true;
                ni.ShowBalloonTip(500);
                this.Hide();
            }
            else if (WindowState.Normal == this.WindowState)
            {
                ni.Visible = false;
            }

            base.OnStateChanged(e);
        }

        public void closeAll()
        {
            if (MainWindow.media != null)
            {
                MainWindow.media.Stop();
                MainWindow.media = null;
                for (int i = 0; i < MainWindow.windowList.Count; i++)
                {
                    MainWindow.windowList[i].Close();
                }

            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {

            ni.Visible = false;

            if (MainWindow.media != null)
            {
                MainWindow.media.Stop();
                MainWindow.media = null;
                for (int i = 0; i < MainWindow.windowList.Count; i++)
                {
                    MainWindow.windowList[i].Close();
                }

            }
            closeAll();
            base.OnClosing(e);
        }
        public static void findWorker()
        {
            // Fetch the Progman window
            IntPtr progman = W32.FindWindow("Progman", null);

            IntPtr result = IntPtr.Zero;

            W32.SendMessageTimeout(progman,
                                   0x052C,
                                   new IntPtr(0),
                                   IntPtr.Zero,
                                   W32.SendMessageTimeoutFlags.SMTO_NORMAL,
                                   1000,
                                   out result);

            workerw = IntPtr.Zero;

            W32.EnumWindows(new W32.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                IntPtr p = W32.FindWindowEx(tophandle,
                                            IntPtr.Zero,
                                            "SHELLDLL_DefView",
                                            IntPtr.Zero);

                if (p != IntPtr.Zero)
                {
                    // Gets the WorkerW Window after the current one.
                    workerw = W32.FindWindowEx(IntPtr.Zero,
                                               tophandle,
                                               "WorkerW",
                                               IntPtr.Zero);
                    
                }

                
                return true;
            }), IntPtr.Zero);


            W32.EnumWindows(new W32.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                IntPtr p = W32.FindWindowEx(tophandle,
                                            IntPtr.Zero,
                                            "Progman",
                                            IntPtr.Zero);

                if (p != IntPtr.Zero)
                {
                    // Gets the WorkerW Window after the current one.
                    workerwHidden = W32.FindWindowEx(IntPtr.Zero,
                                               tophandle,
                                               "WorkerW",
                                               IntPtr.Zero);

                }

                return true;
            }), IntPtr.Zero);


        }

    }
}
