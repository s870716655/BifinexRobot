using System;
using System.Windows.Forms;

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

            // Get BTCUSD price
            PriceData[] BTCUSDPriceArray = CoinTrader.Instance.GetClosePrices(CoinTradeType.BTC_USD, DateTime.Today.AddYears(-1), DateTime.Today.AddDays(-1), 300);
            for (int i = 0; i < BTCUSDPriceArray.Length; i++) {
                m_ZoomChart.AddData(BTCUSDPriceArray[i].Timestamp, Convert.ToDouble(BTCUSDPriceArray[i].Price));
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
