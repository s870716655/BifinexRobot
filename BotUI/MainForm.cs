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
            PriceData[] BTCUSDPriceDataArray = CoinTrader.Instance.GetClosePrices(CoinTradeType.BTC_USD, DateTime.Today.AddYears(-1), DateTime.Today.AddDays(-1), 365);
            m_ZoomChart.AddPriceDatas(BTCUSDPriceDataArray, System.Windows.Media.Colors.Black);

            // Get BTCUSD price in 20MA
            PriceData[] BTCUSD20MAPriceDataArray = GetMAPrice(BTCUSDPriceDataArray, 20);
            m_ZoomChart.AddPriceDatas(BTCUSD20MAPriceDataArray, System.Windows.Media.Colors.Red);

            // Get BTCUSD price in 10MA
            PriceData[] BTCUSD10MAPriceDataArray = GetMAPrice(BTCUSDPriceDataArray, 10);
            m_ZoomChart.AddPriceDatas(BTCUSD10MAPriceDataArray, System.Windows.Media.Colors.DarkOrange);

            // Get BTCUSD price in 5MA
            PriceData[] BTCUSD5MAPriceDataArray = GetMAPrice(BTCUSDPriceDataArray, 5);
            m_ZoomChart.AddPriceDatas(BTCUSD5MAPriceDataArray, System.Windows.Media.Colors.Blue);
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

        PriceData[] GetMAPrice(PriceData[] PriceDataArray, int nAveCount)
        {
            // Get current price array
            decimal[] PriceArray = new decimal[PriceDataArray.Length];
            for (int i = 0; i < PriceArray.Length; i++) {
                PriceArray[i] = PriceDataArray[i].Price;
            }

            // Calculate MA price array
            decimal[] MAPriceArray = Utility.Instance.GetMovingAverage(PriceArray, nAveCount);

            // Construct PriceData array
            PriceData[] MAPriceDataArray = new PriceData[MAPriceArray.Length];
            for (int i = 0; i < MAPriceDataArray.Length; i++) {
               MAPriceDataArray[i] = new PriceData(PriceDataArray[i].Timestamp, MAPriceArray[i]);
            }

            return MAPriceDataArray;
        }
    }
}
