using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Warframe_Alerts
{
    public class WarframeAlert : INotifyPropertyChanged
    {

        private string _title;
        private string _timeLeft;
        private Brush _brush;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public string TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                _timeLeft = value;
                NotifyPropertyChanged("TimeLeft");
            }
        }

        public Brush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
                NotifyPropertyChanged("Brush"); 
            }
        }

        public WarframeAlert(string title, TimeSpan timeLeft)
        {
            Brush foreground = Brushes.White;

            if (title.Contains("Nitain"))
                foreground = Brushes.Red;
            else if (title.Contains("Argon"))
                foreground = Brushes.Plum;
            else if (title.Contains("Orokin Cata"))
                foreground = Brushes.Gold;
            else if (title.Contains("Orokin Reactor"))
                foreground = Brushes.Gold;

            Title = title;
            TimeLeft = timeLeft.ToString(@"hh\:mm\:ss");
            Brush = foreground;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}