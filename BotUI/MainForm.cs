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
            DreaAllChart();

            // Subscribe event
            m_TimeUnitComboBox.SelectedIndexChanged += TimeUnitComboBox_SelectedIndexChanged;
            m_StartTimePicker.CloseUp += TimePicker_CloseUp;
            m_EndTimePicker.CloseUp += TimePicker_CloseUp;

            // Update UI
            UpdateTimePicker();
        }

        private void TimePicker_CloseUp(object sender, EventArgs e)
        {
            // Value check
            if (m_StartTimePicker.Value >= m_EndTimePicker.Value) {
                m_StartTimePicker.Value = m_EndTimePicker.Value.AddDays(-1);
            }

            // Change start and end time
            m_PriceDataInfo.StartTime = m_StartTimePicker.Value;
            m_PriceDataInfo.EndTime = m_EndTimePicker.Value;

            // Refresh datas
            RefreshAllDataAnalyzer();

            // Draw chart
            DreaAllChart();
        }

        void TimeUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change time unit
            m_PriceDataInfo.TimeUnit = (TimeFrame)Enum.Parse(typeof(TimeFrame), m_TimeUnitComboBox.SelectedItem.ToString());

            // If time unit equal to one hour, only show 2 days data
            if (m_PriceDataInfo.TimeUnit == TimeFrame.OneHour) {
                m_PriceDataInfo.EndTime = DateTime.Now.AddHours(-3);
                m_PriceDataInfo.StartTime = m_PriceDataInfo.EndTime.AddDays(-2);

                // Update UI
                UpdateTimePicker();
            }

            // Refresh datas
            RefreshAllDataAnalyzer();

            // Draw chart
            DreaAllChart();
        }

        void DreaAllChart()
        {
            DrawPriceChart(m_LabChartName1, m_ZoomChart1, CoinTradeType.BTC_USD);
            DrawPriceChart(m_LabChartName2, m_ZoomChart2, CoinTradeType.ETH_USD);
        }

        void DrawPriceChart(Label ChartLab, ZoomChart Chart, CoinTradeType CoinType)
        {
            Chart.ClearPriceDatas();
            Chart.AddPriceDatas(m_CointPriceAnalyzerDic[CoinType].PriceData, System.Windows.Media.Colors.Black);
            Chart.AddPriceDatas(m_CointPriceAnalyzerDic[CoinType].MA, System.Windows.Media.Colors.Blue);
            Chart.AddPriceDatas(m_CointPriceAnalyzerDic[CoinType].BBands_Upper, System.Windows.Media.Colors.Green);
            Chart.AddPriceDatas(m_CointPriceAnalyzerDic[CoinType].BBands_Lower, System.Windows.Media.Colors.Red);
            ChartLab.Text = CoinType.ToString();
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

                // Bifinex API spec
                if (nCount > 5000) {
                    nCount = 5000;
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

                m_CointPriceAnalyzerDic[m_CoinList[i]].PriceData = CoinTrader.Instance.GetClosePrices(m_CoinList[i], m_PriceDataInfo.TimeUnit, m_PriceDataInfo.StartTime, m_PriceDataInfo.EndTime, m_PriceDataInfo.DataCount);
            }
        }

        void InitUI()
        {
            InitializeComponent();

            // Init time unit select box
            m_TimeUnitComboBox.Items.Add(TimeFrame.OneMonth.ToString());
            m_TimeUnitComboBox.Items.Add(TimeFrame.SevenDay.ToString());
            m_TimeUnitComboBox.Items.Add(TimeFrame.OneDay.ToString());
            m_TimeUnitComboBox.Items.Add(TimeFrame.OneHour.ToString());
        }

        void UIUpdateTimer_Tick(object sender, EventArgs e)
        {
            m_CoinInfoTable.DataSource = CoinTrader.Instance.WatchedCoinInfos;
            m_ActiveOrderTable.DataSource = CoinTrader.Instance.ActiveOrders;
        }

        void UpdateTimePicker()
        {
            m_TimeUnitComboBox.SelectedItem = m_PriceDataInfo.TimeUnit.ToString();
            m_StartTimePicker.Value = m_PriceDataInfo.StartTime;
            m_EndTimePicker.Value = m_PriceDataInfo.EndTime;
        }
    }
}
