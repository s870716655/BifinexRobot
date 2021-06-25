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
            m_ZoomChart.AddPriceDatas(BTCUSD20MAPriceDataArray, System.Windows.Media.Colors.Blue);

            // Get bollinger bands of BTCUSD price in 20MA
            BBandsData BBandsData_20MA = GetBBandsData(BTCUSDPriceDataArray, 20, 2);
            m_ZoomChart.AddPriceDatas(BBandsData_20MA.UpperPriceData, System.Windows.Media.Colors.Green);
            m_ZoomChart.AddPriceDatas(BBandsData_20MA.LowerPriceData, System.Windows.Media.Colors.Red);
        }

        // Private members
        bool m_isLoginSuccess = false;
        System.Windows.Forms.Timer m_UIUpdateTimer;

        struct BBandsData
        {
            PriceData[] m_UpperPriceData;
            PriceData[] m_LowerPriceData;

            internal BBandsData(PriceData[] UpperPriceData, PriceData[] LowerPriceData)
            {
                m_UpperPriceData = UpperPriceData;
                m_LowerPriceData = LowerPriceData;
            }

            internal PriceData[] UpperPriceData
            {
                get
                {
                    return m_UpperPriceData;
                }
            }

            internal PriceData[] LowerPriceData
            {
                get
                {
                    return m_LowerPriceData;
                }
            }
        }

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

        BBandsData GetBBandsData(PriceData[] PriceDataArray, int nAveCount, int nStdDevNum)
        {
            // Get MA PriceData array
            PriceData[] MAPriceDataArray = GetMAPrice(PriceDataArray, nAveCount);

            // Get standard deviation price array
            decimal[] BufferArray = new decimal[nAveCount];
            decimal[] StdDevPriceArray = new decimal[PriceDataArray.Length];
            
            int nCurrentIndex = 0;
            for (int i = 0; i < StdDevPriceArray.Length; i++) {
                BufferArray[nCurrentIndex] = PriceDataArray[i].Price;
                StdDevPriceArray[i] = Utility.Instance.GetStandardDeviation(BufferArray);
                nCurrentIndex = (nCurrentIndex + 1) % nAveCount;
            }

            // Calculate bollinger bands
            PriceData[] UpperPriceDataArray = new PriceData[StdDevPriceArray.Length];
            PriceData[] LowerPriceDataArray = new PriceData[StdDevPriceArray.Length];
            for (int i = 0; i < UpperPriceDataArray.Length; i++) {
                UpperPriceDataArray[i] = new PriceData(PriceDataArray[i].Timestamp, MAPriceDataArray[i].Price + StdDevPriceArray[i] * nStdDevNum);
                LowerPriceDataArray[i] = new PriceData(PriceDataArray[i].Timestamp, MAPriceDataArray[i].Price - StdDevPriceArray[i] * nStdDevNum);
            }

            return new BBandsData(UpperPriceDataArray, LowerPriceDataArray);
        }
    }
}
