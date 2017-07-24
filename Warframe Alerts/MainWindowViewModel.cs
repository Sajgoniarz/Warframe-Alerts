using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warframe_Alerts
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<WarframeAlert> _feedList;
        private WarframeAlertRSSManager rssManager;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<WarframeAlert> FeedList {
            get { return _feedList; }
            set
            {
                _feedList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FeedList"));
            }
        }

        public MainWindowViewModel()
        {
            rssManager = new WarframeAlertRSSManager();
            FeedList = rssManager.FeedList;
        }
    }
}
