using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Bitfinex.Net;
using Bitfinex.Net.Objects;

namespace BotUI
{
    internal enum CoinTradeType
    {
        BTC_USD = 0,
        ETH_USD = 1,
        DOGE_USD = 2,
    }

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

        internal void AddWatchedCoin(CoinTradeType CoinType)
        {
            if (m_CoinSymbolDic.ContainsKey(CoinType) == false) {
                return;
            }

            m_szWatchedCoinNameList.Add(m_CoinSymbolDic[CoinType]);
        }

        internal BitfinexSymbolOverview[] WatchedCoinInfos
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
        Dictionary<CoinTradeType, string> m_CoinSymbolDic;
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

            // Init all supported coins
            InitCoinSymbol();
        }

        void RoutineTimer_Tick(object sender, EventArgs e)
        {
            // Read all coin informations
            m_CoinInfoArray = m_Client.GetTickerAsync(new CancellationToken(), m_szWatchedCoinNameList.ToArray()).Result.Data.ToArray();

            // Read all active orders
            m_ActiveOrderArray = m_Client.GetActiveOrdersAsync().Result.Data.ToArray();

            // Read k line of coin
            //IEnumerable<BitfinexKline> aaa = m_Client.GetKlinesAsync(TimeFrame.OneDay, "tBTCUSD", startTime: new DateTime(2020, 1, 1), endTime: new DateTime(2020, 12, 13)).Result.Data;
        }

        void InitCoinSymbol()
        {
            m_CoinSymbolDic = new Dictionary<CoinTradeType, string>();
            m_CoinSymbolDic.Add(CoinTradeType.BTC_USD, "tBTCUSD");
            m_CoinSymbolDic.Add(CoinTradeType.ETH_USD, "tETHUSD");
            m_CoinSymbolDic.Add(CoinTradeType.DOGE_USD, "tDOGE:USD");
        }
    }
}
