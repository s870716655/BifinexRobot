﻿using System;
using System.Windows.Forms;

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

            // Initailize watched coin 
            InitWatchedCoin();

            // Initailize timer
            m_UIUpdateTimer = new System.Windows.Forms.Timer();
            m_UIUpdateTimer.Interval = 500;
            m_UIUpdateTimer.Tick += UIUpdateTimer_Tick;
            m_UIUpdateTimer.Enabled = true;

            BitfinexKline[] CoinKlines = CoinTrader.Instance.GetKlines(CoinTradeType.BTC_USD, TimeFrame.OneDay, DateTime.Today.AddYears(-1), DateTime.Today.AddDays(-1), 300);
            for (int i = 0; i < CoinKlines.Length; i++) {
                m_ZoomChart.AddData(CoinKlines[i].Timestamp, Convert.ToDouble(CoinKlines[i].Close));
            }
        }

        // Private members
        bool m_isLoginSuccess = false;
        System.Windows.Forms.Timer m_UIUpdateTimer;

        void InitWatchedCoin()
        {
            CoinTrader.Instance.AddWatchedCoin(CoinTradeType.BTC_USD);
            CoinTrader.Instance.AddWatchedCoin(CoinTradeType.ETH_USD);
            CoinTrader.Instance.AddWatchedCoin(CoinTradeType.DOGE_USD);
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
