using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotUI
{
    internal struct MASetting
    {
        internal int AveCount
        {
            get;
            set;
        }

        internal MASetting(int nAveCount)
        {
            AveCount = nAveCount;
        }
    }

    internal struct BBandsSetting
    {
        internal int AveCount
        {
            get;
            set;
        }

        internal int StdDevNum
        {
            get;
            set;
        }

        internal BBandsSetting(int nAveCount, int nStdDevNum)
        {
            AveCount = nAveCount;
            StdDevNum = nStdDevNum;
        }
    }

    // Public members
    internal partial class DataAnalyzer
    {
        internal DataAnalyzer()
        {
            m_PriceDatasArray_Raw = new PriceData[0];
            DoAllAnalize();
        }

        internal PriceData[] PriceData
        {
            get
            {
                return m_PriceDatasArray_Raw;
            }

            set
            {
                m_PriceDatasArray_Raw = value;
                DoAllAnalize();
            }
        }

        internal PriceData[] MA
        {
            get
            {
                return m_PriceDatasArray_MA;
            }
        }

        internal PriceData[] BBands_Upper
        {
            get
            {
                return m_UpperPriceDatasArray_BBands;
            }
        }

        internal PriceData[] BBands_Lower
        {
            get
            {
                return m_LowerPriceDatasArray_BBands;
            }
        }

        internal MASetting MASetting
        {
            set
            {
                m_MASetting = value;
                DoAllAnalize();
            }
        }

        internal BBandsSetting BBandsSetting
        {
            set
            {
                m_BBandsSetting = value;
                DoAllAnalize();
            }
        }
    }

    // Protected members
    internal partial class DataAnalyzer
    {
    }

    // Private members
    internal partial class DataAnalyzer
    {
        // Raw datas
        PriceData[] m_PriceDatasArray_Raw;

        // Moving average properties
        PriceData[] m_PriceDatasArray_MA;
        MASetting m_MASetting = new MASetting(20);

        // Bollinger bands properties
        PriceData[] m_UpperPriceDatasArray_BBands;
        PriceData[] m_LowerPriceDatasArray_BBands;
        BBandsSetting m_BBandsSetting = new BBandsSetting(20, 2);

        void DoAllAnalize()
        {
            // Calculate moving average
            m_PriceDatasArray_MA = GetMAPrice(m_PriceDatasArray_Raw, m_MASetting.AveCount);

            // Calculate bollinger bands
            GetBBandsData(m_PriceDatasArray_Raw, m_BBandsSetting.AveCount, m_BBandsSetting.StdDevNum, out m_UpperPriceDatasArray_BBands, out m_LowerPriceDatasArray_BBands);
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

        void GetBBandsData(PriceData[] PriceDataArray, int nAveCount, int nStdDevNum, out PriceData[] UpperPriceDataArray, out PriceData[] LowerPriceDataArray)
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
            UpperPriceDataArray = new PriceData[StdDevPriceArray.Length];
            LowerPriceDataArray = new PriceData[StdDevPriceArray.Length];
            for (int i = 0; i < UpperPriceDataArray.Length; i++) {
                UpperPriceDataArray[i] = new PriceData(PriceDataArray[i].Timestamp, MAPriceDataArray[i].Price + StdDevPriceArray[i] * nStdDevNum);
                LowerPriceDataArray[i] = new PriceData(PriceDataArray[i].Timestamp, MAPriceDataArray[i].Price - StdDevPriceArray[i] * nStdDevNum);
            }
        }
    }
}
