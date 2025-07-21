using System;

namespace Codebase.Utils
{
    [Serializable]
    public class IntRangeValues
    {
        public int Min;
        public int Max;

        public IntRangeValues(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public void ClampValues()
        {
            if (Min > Max)
            {
                Min = Max;
            }
            else if (Max < Min)
            {
                Max = Min;
            }
        }
    }
    
    
}