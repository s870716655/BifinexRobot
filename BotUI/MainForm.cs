using System;
using System.Windows.Forms;

namespace BotUI
{
    // Public members
    internal partial class MainForm : Form
    {
        internal MainForm()
        {
            InitializeComponent();

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
        }

        // Private members
        bool m_isLoginSuccess = false;
        System.Windows.Forms.Timer m_UIUpdateTimer;

        void InitWatchedCoin()
        {
            CoinTrader.Instance.AddWatchedCoin("tBTCUSD");
            CoinTrader.Instance.AddWatchedCoin("tETHUSD");
        }

        void UIUpdateTimer_Tick(object sender, EventArgs e)
        {
            CoinInfo[] CoinInfos = CoinTrader.Instance.CoinInfos;
            if (CoinInfos.Length == 0) {
                return;
            }

            // Update price
            Lab_PriceBTCUSD.Text = CoinInfos[0].LastPrice.ToString("0");
        }
    }
}
