using Mango.Fundamentals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        Timer myTimer = new Timer(new TimeSpan(0, 0,10));

       

       
        

      
        public MainPage()
        {
            this.InitializeComponent();
            myTimer.TimerTicked += MyTimer_TimerTicked;
            myTimer.TimerEnded += MyTimer_TimerEnded;
        }

        private void displayTimeLeft()
        {
            Debug.WriteLine(myTimer.TimeLeft);
        }

        private void MyTimer_TimerEnded(object sender, TimerEventArgs e)
        {
            
        }

        private void MyTimer_TimerTicked(object sender, TimerEventArgs e)
        {
            displayTimeLeft();
        }


        private void StartTimerButton_Click(object sender, RoutedEventArgs e)
        {
            myTimer.StartTimer();
            displayTimeLeft();
        }

       
        private void RestartTimerButton_Click(object sender, RoutedEventArgs e)
        {
            myTimer.ResetTimer();
            displayTimeLeft();
        }

        private void PauseTimerButton_Click(object sender, RoutedEventArgs e)
        {
            myTimer.PauseTimer();
        }
    }
}
