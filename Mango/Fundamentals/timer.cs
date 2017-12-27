using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static Mango.Fundamentals.TimerEventArgs;

namespace Mango.Fundamentals
{
    public sealed class Timer:INotifyPropertyChanged
    {
        #region Fields

        private DispatcherTimer _ticker = new DispatcherTimer();
        private TimeSpan _descendingTime;
        private TimeSpan _defaultTimerInterval = new TimeSpan(0, 0, 0, 0, 100);

        private TimeSpan _timeLeft;


        /// <summary>
        /// Shows the amount of time left for the timer. The interval property determines the accuracy of
        /// this value.
        /// </summary>
        public TimeSpan TimeLeft
        {
            get { return _descendingTime; }
            private set
            {
                _timeLeft = value;
                OnPropertyChanged("TimeLeft");

            }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #endregion

        #region Properties

        /// <summary>
        /// How long the timer will run for. Default Value is 100 milliseconds.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary
        /// <para>
        /// Period of time that the before each timer update
        /// </para> 
        /// <para>Lower values give you more precise values when checking the time remaining</para>
        /// </summary>
        public TimeSpan Interval { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new timer. Its default interval is 100 milliseconds and its default duration is 30 seconds.
        /// </summary>
        public Timer()
        {
            Duration = new TimeSpan(0,0,30);
            _descendingTime = Duration;
            _ticker.Tick += _ticker_Tick;
            SetTimerInterval(_ticker);
        }

        /// <summary>
        ///  Create a new timer. Its default interval is 100 milliseconds.
        /// </summary>
        /// <param name="duration"></param>
        public Timer(TimeSpan duration)
        {
            Duration = duration;
            _descendingTime = Duration;
            _ticker.Tick += _ticker_Tick;
            SetTimerInterval(_ticker);
        }

        /// <summary>
        /// Create a new timer.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="interval"></param>
        public Timer(TimeSpan duration, TimeSpan interval)
        {
            Duration = duration;
            _descendingTime = Duration;
            _ticker.Tick += _ticker_Tick;
            SetTimerInterval(_ticker, interval);
        }


        #endregion

        #region Methods

        
        private void SetTimerInterval(DispatcherTimer ticker)
        {
            ticker.Interval = _defaultTimerInterval;
            Interval = _defaultTimerInterval;
        }

        private void SetTimerInterval(DispatcherTimer ticker, TimeSpan interval)
        {
            ticker.Interval = interval;
            Interval = interval;
        }


        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartTimer()
        {
            _ticker.Start();
        }

        /// <summary>
        /// Pauses the timer
        /// </summary>
        public void PauseTimer()
        {
            _ticker.Stop();
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void StopTimer()
        {
            _ticker = new DispatcherTimer();
            _descendingTime = Duration;
        }

        /// <summary>
        /// <para>
        /// Change how often the timer updates
        /// </para> 
        /// <para>Lower values give you more precise values when checking the time remaining</para>
        /// </summary>
        /// <param name="interval"></param>
        public void ChangeInterval(TimeSpan interval)
        {
            SetTimerInterval(_ticker, interval);
        }

        #endregion

        #region Events

        /// <summary>
        /// Event that runs when the timer has finished.
        /// </summary>
        public event TimerEventHandler TimerEnded;

        public event PropertyChangedEventHandler PropertyChanged;

        public event TimerEventHandler TimerTicked;

        #endregion


        #region Event Methods

        private void _ticker_Tick(object sender, object e)
        {
           _descendingTime = _descendingTime.Subtract(Interval);
            OnTimerTickedEvent();

            if (_descendingTime.Ticks == 0)
            {
                _ticker.Stop();
                OnRaiseTimerEndedEvent();
            }
        }

        private void OnTimerTickedEvent()
        {
            TimerTicked?.Invoke(this, new TimerEventArgs(false));
        }

        private void OnRaiseTimerEndedEvent()
        {
            TimerEnded?.Invoke(this, new TimerEventArgs(true));
        }

        #endregion

       
        }

    public sealed class TimerEventArgs
    {
        public TimerEventArgs(bool isEnded)
        {
            ended = isEnded;
        }
        private bool ended;
        public bool TimerEnded
        {
            get { return ended; }
        }

      
    }
    public delegate void TimerEventHandler(object sender, TimerEventArgs e);

}
