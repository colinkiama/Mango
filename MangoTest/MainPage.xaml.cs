using Mango.Fundamentals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MangoTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Timer myTimer = new Timer();
        string damn = "Boom";
      
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Timer myTimer = new Timer();
            myTimer.TimerEnded += MyTimer_TimerEnded;
            myTimer.StartTimer();
            await Task.Delay(5000); 
            Debug.WriteLine(myTimer.TimeLeft.ToString());

        }

        private void MyTimer_TimerEnded(object sender, TimerEventArgs e)
        {
            ShowContentDialog("Timer is done!");

        }

        private async void ShowContentDialog(string content)
        {
            await new ContentDialog { Content = content, CloseButtonText = "Close" }.ShowAsync();
        }


    }

    public class somethingDifferent
    {
        public string bindToThis = "Beach";
    }
}
