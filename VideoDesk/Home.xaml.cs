using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoDesk
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : System.Windows.Controls.UserControl
    {

        bool onlyOne;
        bool winSeven;

        // For 1 video per monitor *not yet in*
        public static List<String> FileLoadList;
        public static List<int> FileLoadListSort;
        public static List<Uri> FileLoadListUri;

        public Home()
        {
            InitializeComponent();
            PlayButton.IsEnabled = false;
            winSeven = false;
            FileLoadList = new List<String>();
            FileLoadListSort = new List<int>();
            FileLoadListUri = new List<Uri>();
            onlyOne = true;
            Each.Visibility = Visibility.Hidden;
            /*
            //Multiple monitor thing (dynamic button creation)
            if (MainWindow.ScreenList.Count > 1)
            {
                Each.Visibility = Visibility.Visible;

                onlyOne = false;
                for (int i = 0; i < MainWindow.ButtonList.Count; i++)
                {
                    MainWindow.ButtonList[i].Click += new RoutedEventHandler(DynaButton_Click);
                    FileLoadList.Add("");
                   // FileLoadListUri.Add(new Uri(""));

                    ms.Children.Add(MainWindow.ButtonList[i]);
                }
            }
            if (onlyOne)
                Multiple.IsEnabled = false;
            else
                Multiple.IsEnabled = true;
                */
        }

        //Multiple monitors
        //Not working yet, but will be used to load and prepare the video that are going to be played on each monitor
        private void DynaButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            // Set filter for file extension and default file extension 
            fileDialog.Filter = "All files (*.*)|*.*";

            int t = Convert.ToInt32(Tag);
            Console.WriteLine("Value = " + t);
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    FileLoadList.Add(fileDialog.FileName);
                    FileLoadListUri.Add(new Uri(fileDialog.FileName));
                    PlayButton.IsEnabled = true;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    FileSelected.Text = "nothing";
                    PlayButton.IsEnabled = false;

                    break;
            }
        }

        //One monitor or all the same
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            // Set filter for file extension and default file extension 
            fileDialog.Filter = "All files (*.*)|*.*";


            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    MainWindow.file = fileDialog.FileName;
                    MainWindow.fileMedia = new Uri(fileDialog.FileName);
                    FileSelected.Text = MainWindow.file;
                    PlayButton.IsEnabled = true;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    FileSelected.Text = "nothing";
                    PlayButton.IsEnabled = false;

                    break;
            }
        }



        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.findWorker();

            if (winSeven)
            {
                Window sev = new Window();
                IntPtr windowSev = new WindowInteropHelper(sev).Handle;

                W32.SetParent(windowSev, MainWindow.workerw);

            }
            for (int i = 0; i < MainWindow.windowList.Count; i++)
            {
                MainWindow.windowList[i] = new Window();

                MainWindow.windowList[i].WindowStyle = WindowStyle.None;
                MainWindow.windowList[i].AllowsTransparency = true;

                MainWindow.windowList[i].Top = MainWindow.ScreenList[i].Top;
                MainWindow.windowList[i].Left = MainWindow.ScreenList[i].Left;
                MainWindow.windowList[i].Width = MainWindow.ScreenList[i].Width;
                MainWindow.windowList[i].Height = MainWindow.ScreenList[i].Height;

                MainWindow.windowList[i].Initialized += new EventHandler((s, ea) =>
                {
                    
                    MainWindow.media = new MediaElement();
                    Grid grid = new Grid();
                    MainWindow.windowList[i].Content = grid;
                    grid.Children.Add(MainWindow.media);


                    MainWindow.media.Source = MainWindow.fileMedia;
                    MainWindow.media.LoadedBehavior = MediaState.Manual;

                    if (MainWindow.soundOrNot)
                        MainWindow.media.IsMuted = false;
                    else
                        MainWindow.media.IsMuted = true;
                    MainWindow.media.Volume = 0;
                    MainWindow.media.IsEnabled = true;
                    MainWindow.media.Visibility = Visibility.Visible;
                  // MainWindow.media.MediaEnded += new RoutedEventHandler(m_MediaEnded);

                    MainWindow.media.MediaEnded += (send, eArgs) =>
                    {
                        MainWindow.media.Position = new TimeSpan(0, 0, 1);
                        MainWindow.media.Play();
                    };
                    MainWindow.media.Play();
                    MainWindow.currentlyPlaying = true;
                    MainWindow.media.Stretch = Stretch.Fill;
                    

                    /*
                    Grid grid = new Grid();
                    MainWindow.windowList[i].Content = grid;
                    if (alreadyBind == false)
                    {
                        grid.Children.Add(MainWindow.mediaList[i]);
                    }


                    MainWindow.mediaList[i].Source = MainWindow.fileMedia;
                    MainWindow.mediaList[i].LoadedBehavior = MediaState.Manual;

                    if (MainWindow.soundOrNot)
                        MainWindow.mediaList[i].IsMuted = false;
                    else
                        MainWindow.mediaList[i].IsMuted = true;
                    MainWindow.mediaList[i].Volume = 0;
                    MainWindow.mediaList[i].IsEnabled = true;
                    MainWindow.mediaList[i].Visibility = Visibility.Visible;

                    MainWindow.mediaList[i].Play();
                    MainWindow.currentlyPlaying = true;
                    MainWindow.mediaList[i].Stretch = Stretch.Fill;
                    MainWindow.mediaList[i].MediaEnded += (send, eArgs) =>
                    {
                        MainWindow.mediaList[i].Position = new TimeSpan(0, 0, 1);
                        MainWindow.mediaList[i].Play();
                    };
                    */
                    IntPtr windowHandle = new WindowInteropHelper(MainWindow.windowList[i]).Handle;
                    if (winSeven)
                        W32.SetParent(windowHandle, MainWindow.workerwHidden);
                    else
                        W32.SetParent(windowHandle, MainWindow.workerw);

                });
                MainWindow.windowList[i].UpdateLayout();

                MainWindow.windowList[i].Show();
            }

        }

        
        public static void m_MediaEnded(object sender, RoutedEventArgs e)
        {
            MainWindow.media.Position = new TimeSpan(0, 0, 1);
            //MainWindow.media.Position = TimeSpan.FromSeconds(0);
            MainWindow.media.Play();
        }
        
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.currentlyPlaying == true)
            {
                MainWindow.media.Stop();
                MainWindow.media = null;
                for (int i = 0; i < MainWindow.windowList.Count; i++)
                {
                    //MainWindow.mediaList[i].Stop();
                    //MainWindow.mediaList[i] = null;
                    //MainWindow.mediaList[i] = new MediaElement();
                    MainWindow.windowList[i].Close();
                }

                MainWindow.currentlyPlaying = false;
            }
        }

        private void Sound_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.soundOrNot = true;

            if (MainWindow.currentlyPlaying == true)
                MainWindow.media.IsMuted = false;
            Sound.IsChecked = true;
            Volume.IsEnabled = true;
        }

        private void Sound_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.soundOrNot = false;
            if (MainWindow.currentlyPlaying == true)
                MainWindow.media.IsMuted = true;
            Sound.IsChecked = false;
            Volume.IsEnabled = false;
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.currentlyPlaying == true)
                MainWindow.media.Volume = Volume.Value;
        }

        private void Multiple_Unchecked(object sender, RoutedEventArgs e)
        {
            MonoScreen.IsEnabled = true;

            for (int i = 0; i < MainWindow.ButtonList.Count; i++)
            {
                MainWindow.ButtonList[i].IsEnabled = false;
            }
        }

        private void Multiple_Checked(object sender, RoutedEventArgs e)
        {
            MonoScreen.IsEnabled = false;
            for (int i = 0; i < MainWindow.ButtonList.Count; i++)
            {
                MainWindow.ButtonList[i].IsEnabled = true;
            }

        }

        private void Windows_Checked(object sender, RoutedEventArgs e)
        {
            winSeven = true;
        }

        private void Windows_Unchecked(object sender, RoutedEventArgs e)
        {
            winSeven = false;
        }
    }
}
