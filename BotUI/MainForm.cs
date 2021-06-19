﻿using System;
using System.Windows.Forms;
using Bitfinex.Net;
using Bitfinex.Net.Objects.RestV1Objects;
using CryptoExchange.Net.Objects;
using Bitfinex.Net.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using BotUI.UserMgr;

namespace BTCTrandBot
{
    // Public members
    internal partial class MainForm : Form
    {
        internal MainForm()
        {
            InitializeComponent();

            // Init Bitfinex client
            InitClient();

            // Initailize timer
            m_Timer_1000ms = new System.Windows.Forms.Timer();
            m_Timer_1000ms.Interval = 1000;
            m_Timer_1000ms.Tick += Timer_1000ms_Tick;
            m_Timer_1000ms.Enabled = true;

            m_Timer_500ms = new System.Windows.Forms.Timer();
            m_Timer_500ms.Interval = 500;
            m_Timer_500ms.Tick += Timer_500ms_Tick;
            m_Timer_500ms.Enabled = true;
        }

        BitfinexClient m_Client;
        System.Windows.Forms.Timer m_Timer_1000ms;
        System.Windows.Forms.Timer m_Timer_500ms;

        void InitClient()
        {
            m_Client = new BitfinexClient();

            // Log in
            bool isLoginSuccess = LogIn("CHIENLI");
            if (isLoginSuccess == false) {
                MessageBox.Show("Log in failed");
                return;
            }

            WebCallResult<IEnumerable<BitfinexSymbolOverview>> BTCUSDInfo = m_Client.GetTickerAsync(new CancellationToken(), "tBTCUSD").Result;
            //WebCallResult<IEnumerable<BitfinexCurrency>> CoinType = m_Client.GetCurrenciesAsync().Result;
            //Task <WebCallResult<IEnumerable<BitfinexCurrency>>> GetCoinTypeTask = m_Client.GetCurrenciesAsync();
            //GetCoinTypeTask.Start
            //WebCallResult<IEnumerable<BitfinexOrder>> Orders = m_Client.GetActiveOrders();
        }

        bool LogIn(string szUserName)
        {
            UserInfo User = UserMgr.Instance.GetUserInfo(szUserName);
            if (User.Key.Length == 0 || User.PassWord.Length == 0) {
                return false;
            }

            // Log in
            m_Client.SetApiCredentials(User.Key, User.PassWord);
            return true;
        }

        void Timer_1000ms_Tick(object sender, EventArgs e)
        {
            m_Client.GetTickerAsync(new CancellationToken(), "tBTCUSD");
        }

        void Timer_500ms_Tick(object sender, EventArgs e)
        {
        }
    }
}