using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Threading;
using System.Xml;

namespace Warframe_Alerts
{
    public class WarframeAlertRSSManager
    {
        public ObservableCollection<WarframeAlert> FeedList { get; private set; }

        private const string feedURL = "http://content.warframe.com/dynamic/rss.php";

        private DispatcherTimer viewUpdaterTimer = new DispatcherTimer();
        private DispatcherTimer dataUpdaterTimer = new DispatcherTimer();
        private XmlDocument warframeFeed = new XmlDocument();

        public WarframeAlertRSSManager()
        {
            FeedList = new ObservableCollection<WarframeAlert>();

            dataUpdaterTimer.Interval = TimeSpan.FromMinutes(2);
            viewUpdaterTimer.Interval = TimeSpan.FromSeconds(1);

            dataUpdaterTimer.Tick += new EventHandler(getFeed);
            viewUpdaterTimer.Tick += new EventHandler(populateFeedList);

            getFeed();
            populateFeedList();

            dataUpdaterTimer.Start();
            viewUpdaterTimer.Start();
        }

        private void populateFeedList(object sender = null, EventArgs e = null)
        {
            XmlNodeList feedItemList = warframeFeed.GetElementsByTagName("item");

            if (feedItemList.Count > 0)
            {
                FeedList.Clear();

                foreach (XmlElement feedItem in feedItemList)
                    if (feedItem["author"].InnerText == "Alert")
                    {
                        TimeSpan timeLeft = getTimeLeft(feedItem);

                        if (timeLeft.TotalMilliseconds > 0)
                            FeedList.Add(new WarframeAlert(feedItem["title"].InnerText, timeLeft));
                    }
            }
        }

        private void getFeed(object sender = null, EventArgs e = null)
        {
            warframeFeed.Load(feedURL);
        }

        private TimeSpan getTimeLeft(XmlElement item)
        {
            DateTime expirationDate = DateTime.Parse(item["wf:expiry"].InnerText);
            DateTime now = DateTime.Now;

            return expirationDate.Subtract(now);
        }
    }
}