using MAFIA.Infrastructure.Commands;
using MAFIA.Models;
using MAFIA.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MAFIA.ViewModels
{
    class TimerViewModel : ViewModel
    {
        
        private string _TimeLabel;

        public string TimeLabel
        {
            get => _TimeLabel;
            set => Set(ref _TimeLabel, value);
        }

        DispatcherTimer _timer;
        TimeSpan _time;

        public TimerViewModel()
        {

            _time = TimeSpan.FromSeconds(60);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimeLabel = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            
            _timer.Start();
        }

        //public TimerViewModel()
        //{
        //    MinuteTimer = new DispatcherTimer();
        //    MinuteTimer.Tick += MinuteTimer_Tick;
        //    MinuteTimer.Interval = new TimeSpan(0, 0, 1);
        //    var StartTime = DateTime.UtcNow;
        //    EndTime = StartTime.AddSeconds(time);
        //    MinuteTimer.Start();
        //}

        //void MinuteTimer_Tick(object sender, EventArgs e)
        //{
        //    var now = DateTime.UtcNow;
        //    TimeSpan left = TimeSpan.FromSeconds(EndTime - now);                

        //    time--;
        //    if (left >= TimeSpan.Zero)
        //    {
        //        TimeLabel = left.ToString("c");




        //    }
        //    else
        //        MinuteTimer.Stop();
        //}

    }
}
