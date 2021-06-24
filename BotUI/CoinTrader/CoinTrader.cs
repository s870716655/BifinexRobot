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

    internal struct PriceData
    {
        DateTime m_Timestamp;
        decimal m_Price;

        internal PriceData(DateTime Timestamp, decimal Price)
        {
            m_Timestamp = Timestamp;
            m_Price = Price;
        }

        internal DateTime Timestamp
        {
            get
            {
                return m_Timestamp;
            }
        }

        internal decimal Price
        {
            get
            {
                return m_Price;
            }
        }
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

        internal BitfinexKline[] GetKlines(CoinTradeType CoinType, TimeFrame TimeUnit, DateTime StartTime, DateTime EndTime, int nCount = 120)
        {
            if (m_CoinSymbolDic.ContainsKey(CoinType) == false) {
                return new BitfinexKline[0];
            }

            return m_Client.GetKlinesAsync(TimeUnit, m_CoinSymbolDic[CoinType], startTime: StartTime, endTime: EndTime, limit: nCount).Result.Data.ToArray();
        }

        internal PriceData[] GetClosePrices(CoinTradeType CoinType, DateTime StartTime, DateTime EndTime, int nCount = 120)
        {
            if (m_CoinSymbolDic.ContainsKey(CoinType) == false) {
                return new PriceData[0];
            }

            BitfinexKline[] CoinKlines = m_Client.GetKlinesAsync(TimeFrame.OneDay, m_CoinSymbolDic[CoinType], startTime: StartTime, endTime: EndTime, limit: nCount).Result.Data.ToArray();
            PriceData[] PriceDataArray = new PriceData[CoinKlines.Length];
            for (int i = 0; i < PriceDataArray.Length; i++) {
                PriceDataArray[i] = new PriceData(CoinKlines[i].Timestamp, CoinKlines[i].Close);
            }
            return PriceDataArray;
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
