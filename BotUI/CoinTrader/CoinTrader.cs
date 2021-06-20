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

            // Initailize timer
            m_RoutineTimer = new System.Windows.Forms.Timer();
            m_RoutineTimer.Interval = m_nCoinDataPollingInterval;
            m_RoutineTimer.Tick += RoutineTimer_Tick;
            m_RoutineTimer.Enabled = true;
        }

        void RoutineTimer_Tick(object sender, EventArgs e)
        {
            // Ger all informations by client
            IEnumerable<BitfinexSymbolOverview> AllCoinInfos = m_Client.GetTickerAsync(new CancellationToken(), m_szWatchedCoinNameList.ToArray()).Result.Data;

            // Read all datas
            m_CoinInfoArray = new BitfinexSymbolOverview[AllCoinInfos.Count()];
            foreach (var CoinData in AllCoinInfos.Select((value, i) => new { value, i })) {

                int nIndex = CoinData.i;

                // Record data
                m_CoinInfoArray[nIndex] = new BitfinexSymbolOverview();
                m_CoinInfoArray[nIndex].Symbol = CoinData.value.Symbol;
                m_CoinInfoArray[nIndex].Bid = CoinData.value.Bid;
                m_CoinInfoArray[nIndex].BidSize = CoinData.value.BidSize;
                m_CoinInfoArray[nIndex].Ask = CoinData.value.Ask;
                m_CoinInfoArray[nIndex].AskSize = CoinData.value.AskSize;
                m_CoinInfoArray[nIndex].DailyChange = CoinData.value.DailyChange;
                m_CoinInfoArray[nIndex].DailyChangePercentage = CoinData.value.DailyChangePercentage;
                m_CoinInfoArray[nIndex].LastPrice = CoinData.value.LastPrice;
                m_CoinInfoArray[nIndex].Volume = CoinData.value.Volume;
                m_CoinInfoArray[nIndex].High = CoinData.value.High;
                m_CoinInfoArray[nIndex].Low = CoinData.value.Low;
            }
        }
    }
}
