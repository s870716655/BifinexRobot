using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Bitfinex.Net.Objects;

namespace BotUI
{
    // Public members
    internal partial class MainForm : Form
    {
        internal MainForm()
        {
            // Init UI
            InitUI();

            // Log in
            m_isLoginSuccess = CoinTrader.Instance.UserLog("CHIENLI");
            if (m_isLoginSuccess == false) {
                MessageBox.Show("Log in failed");
            }

            // Initailize timer
            m_UIUpdateTimer = new System.Windows.Forms.Timer();
            m_UIUpdateTimer.Interval = 500;
            m_UIUpdateTimer.Tick += UIUpdateTimer_Tick;
            m_UIUpdateTimer.Enabled = true;

            // Add coin to wated list
            AddWatchedCoin(CoinTradeType.BTC_USD);
            AddWatchedCoin(CoinTradeType.ETH_USD);
            AddWatchedCoin(CoinTradeType.DOGE_USD);

            // Initailize coin watching function
            InitCoinWatchFunc();

            // Initailize the time setting of price data
            m_PriceDataInfo = new PriceDataInfo();
            m_PriceDataInfo.EndTime = DateTime.Today.AddDays(-1);
            m_PriceDataInfo.StartTime = m_PriceDataInfo.EndTime.AddMonths(-3);
            m_PriceDataInfo.TimeUnit = TimeFrame.OneDay;

            // Get all coins analize result
            RefreshAllDataAnalyzer();

           // Draw BTCUSD prices
            m_ZoomChart.AddPriceDatas(m_CointPriceAnalyzerDic[CoinTradeType.BTC_USD].PriceData, System.Windows.Media.Colors.Black);
            m_ZoomChart.AddPriceDatas(m_CointPriceAnalyzerDic[CoinTradeType.BTC_USD].MA, System.Windows.Media.Colors.Blue);
            m_ZoomChart.AddPriceDatas(m_CointPriceAnalyzerDic[CoinTradeType.BTC_USD].BBands_Upper, System.Windows.Media.Colors.Green);
            m_ZoomChart.AddPriceDatas(m_CointPriceAnalyzerDic[CoinTradeType.BTC_USD].BBands_Lower, System.Windows.Media.Colors.Red);
        }

        // Private members
        bool m_isLoginSuccess = false;
        System.Windows.Forms.Timer m_UIUpdateTimer;
        Dictionary<CoinTradeType, DataAnalyzer> m_CointPriceAnalyzerDic;
        List<CoinTradeType> m_CoinList;
        PriceDataInfo m_PriceDataInfo;

        struct PriceDataInfo
        {
           internal TimeFrame TimeUnit
            {
                get;
                set;
            }

            internal DateTime StartTime
            {
                get;
                set;
            }

            internal DateTime EndTime
            {
                get;
                set;
            }

            internal int DataCount
            {
                get
                {
                    if (StartTime == null || EndTime == null) {
                        return 0;
                    }

                    return CalTimeFrameCount(EndTime - StartTime, TimeUnit);
                }
            }

            int CalTimeFrameCount(TimeSpan TimeDiffInput, TimeFrame TimeUnit)
            {
                TimeSpan TimeDiff = TimeDiffInput.Add(TimeSpan.FromDays(1));
                int nCount;
                switch (TimeUnit) {
                    case TimeFrame.OneMonth:
                        nCount = (int)(Math.Ceiling(TimeDiff.TotalDays / 30));
                        break;
                    case TimeFrame.SevenDay:
                        nCount = (int)(Math.Ceiling(TimeDiff.TotalDays / 7));
                        break;
                    case TimeFrame.OneDay:
                        nCount = (int)(Math.Ceiling(TimeDiff.TotalDays));
                        break;
                    case TimeFrame.OneHour:
                        nCount = (int)(Math.Ceiling(TimeDiff.TotalHours));
                        break;
                    case TimeFrame.FiveMinute:
                        nCount = (int)(Math.Ceiling(TimeDiff.TotalMinutes) / 5);
                        break;
                    case TimeFrame.OneMinute:
                    default:
                        nCount = (int)(Math.Ceiling(TimeDiff.TotalMinutes));
                        break;
                }

                return nCount;
            }
        }

        void AddWatchedCoin(CoinTradeType CoinType)
        {
            // Initailize history data analizer
            if (m_CoinList == null) {
                m_CoinList = new List<CoinTradeType>();
            }
            m_CoinList.Add(CoinType);
        }

        void InitCoinWatchFunc()
        {
            for (int i = 0; i < m_CoinList.Count; i++) {
                // Initailize real-time watcher
                CoinTrader.Instance.AddWatchedCoin(m_CoinList[i]);

                // Initailize history data analizer
                if (m_CointPriceAnalyzerDic == null) {
                    m_CointPriceAnalyzerDic = new Dictionary<CoinTradeType, DataAnalyzer>();
                }
                m_CointPriceAnalyzerDic.Add(m_CoinList[i], new DataAnalyzer());
            }
        }

        void RefreshAllDataAnalyzer()
        {
            for (int i = 0; i < m_CoinList.Count; i++) {
                if (m_CointPriceAnalyzerDic.ContainsKey(m_CoinList[i]) == false) {
                    continue;
                }

                m_CointPriceAnalyzerDic[m_CoinList[i]].PriceData = CoinTrader.Instance.GetClosePrices(m_CoinList[i], m_PriceDataInfo.StartTime, m_PriceDataInfo.EndTime, m_PriceDataInfo.DataCount);
            }
        }

        void InitUI()
        {
            InitializeComponent();
        }

        void UIUpdateTimer_Tick(object sender, EventArgs e)
        {
            m_CoinInfoTable.DataSource = CoinTrader.Instance.WatchedCoinInfos;
            m_ActiveOrderTable.DataSource = CoinTrader.Instance.ActiveOrders;
        }
    }
}
