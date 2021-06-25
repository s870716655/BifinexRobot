using System;
using System.Collections.Generic;


namespace BotUI
{
    // Public members
    internal partial class Utility
    {
        internal static Utility Instance
        {
            get
            {
                if (m_Utility == null) {
                    m_Utility = new Utility();
                }
                return m_Utility;
            }
        }

        internal decimal[] GetMovingAverage(decimal[] DataArray, int nAveCount)
        {
            if (nAveCount <= 0) {
                return new decimal[0];
            }

            return MovingAverage(DataArray, nAveCount);
        }

        internal decimal GetStandardDeviation(decimal[] DataArray)
        {
            if (DataArray.Length == 0) {
                return 0.0M;
            }

            return StandardDeviation(DataArray);
        }
    }

    // Protected members
    internal partial class Utility
    {
    }

    // Private members
    internal partial class Utility
    {
        static Utility m_Utility;
        Utility()
        {
        }

        decimal[] MovingAverage(decimal[] DataArray, int nAveCount)
        {
            decimal[] BufferArray = new decimal[nAveCount];
            decimal[] OutputArray = new decimal[DataArray.Length];

            int nCurrentIndex = 0;
            for (int i = 0; i < OutputArray.Length; i++) {
                BufferArray[nCurrentIndex] = DataArray[i] / nAveCount;
                for (int j = 0; j < nAveCount; j++) {
                    OutputArray[i] += BufferArray[j];
                }
                nCurrentIndex = (nCurrentIndex + 1) % nAveCount;
            }

            return OutputArray;
        }

        decimal GetAverage(decimal[] DataArray)
        {
            decimal Sum = 0.0M;
            for (int i = 0; i < DataArray.Length; i++) {
                Sum += DataArray[i];
            }

            return Sum / DataArray.Length;
        }

        decimal StandardDeviation(decimal[] DataArray)
        {
            decimal Average = GetAverage(DataArray);

            decimal Sum = 0.0M;
            for (int i = 0; i < DataArray.Length; i++) {
                Sum += ((DataArray[i] - Average) * (DataArray[i] - Average));
            }

            return Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(Sum / DataArray.Length)));
        }
    }
}
