using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MminoWindows.Module
{
    internal static class ConvertUnits
    {
        public static class FileSize
        {
            public enum SizeUnit
            {
                bytes = 0,
                kilobytes = 1,
                megabytes = 2,
                gigabytes = 3,
                terabytes = 4
            }

            public static double ConvertFromBytes(SizeUnit unit, double value)
            {
                for (int i = 0; i < (int)unit; i++)
                {
                    value /= 1024f;
                }
                return value;
            }
        }
    }
}
