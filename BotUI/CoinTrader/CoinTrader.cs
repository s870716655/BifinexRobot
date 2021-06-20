using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Bitfinex.Net;
using Bitfinex.Net.Objects;

namespace BotUI
{
    // Public members
    internal partial class CoinTrader
    {
        internal static CoinTrader Instance
        {
            get
            {
                if (m_Trader == null) {
                    m_Trader = new CoinTrader();
                }
                return m_Trader;
            }
        }

        internal bool UserLog(string szUserName)
        {
            return LogIn(szUserName);
        }

        internal void AddWatchedCoin(string szCoinName)
        {
            m_szWatchedCoinNameList.Add(szCoinName);
        }

        internal BitfinexSymbolOverview[] CoinInfos
        {
            get
            {
                return m_CoinInfoArray;
            }
        }

        internal BitfinexOrder[] ActiveOrders
        {
            get
            {
                return m_ActiveOrderArray;
            }
        }
    }

    // Protected members
    internal partial class CoinTrader
    {
    }

    // Private members
    internal partial class CoinTrader
    {
        static CoinTrader m_Trader;
        BitfinexClient m_Client;
        List<string> m_szWatchedCoinNameList;
        BitfinexSymbolOverview[] m_CoinInfoArray;
        BitfinexOrder[] m_ActiveOrderArray;
        System.Windows.Forms.Timer m_RoutineTimer;

        // Settings
        const int m_nCoinDataPollingInterval = 5000;

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

        CoinTrader()
        {
            // Initailize variables
            m_Client = new BitfinexClient();
            m_szWatchedCoinNameList = new List<string>();
            m_CoinInfoArray = new BitfinexSymbolOverview[0];
            m_ActiveOrderArray = new BitfinexOrder[0];

            // Initailize timer
            m_RoutineTimer = new System.Windows.Forms.Timer();
            m_RoutineTimer.Interval = m_nCoinDataPollingInterval;
            m_RoutineTimer.Tick += RoutineTimer_Tick;
            m_RoutineTimer.Enabled = true;
        }

        void RoutineTimer_Tick(object sender, EventArgs e)
        {
            // Read all coin informations
            IEnumerable<BitfinexSymbolOverview> AllCoinInfos = m_Client.GetTickerAsync(new CancellationToken(), m_szWatchedCoinNameList.ToArray()).Result.Data;
            m_CoinInfoArray = new BitfinexSymbolOverview[AllCoinInfos.Count()];
            foreach (var CoinData in AllCoinInfos.Select((value, i) => new { value, i })) {
                // Record data
                m_CoinInfoArray[CoinData.i] = CoinData.value;
            }

            // Read all active orders
            IEnumerable<BitfinexOrder> AllActiveOrders = m_Client.GetActiveOrdersAsync().Result.Data;
            m_ActiveOrderArray = new BitfinexOrder[AllActiveOrders.Count()];
            foreach (var ActiveOrder in AllActiveOrders.Select((value, i) => new { value, i })) {
                // Record data
                m_ActiveOrderArray[ActiveOrder.i] = ActiveOrder.value;
            }
        }
    }
}
