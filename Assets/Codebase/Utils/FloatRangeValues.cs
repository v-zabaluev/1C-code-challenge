using System;

namespace Codebase.Utils
{
    [Serializable]
    public class FloatRangeValues
    {
        public float Min;
        public float Max;

        public FloatRangeValues(float min, float max)
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