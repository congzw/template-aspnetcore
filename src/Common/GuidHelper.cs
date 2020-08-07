using System;
using System.Collections.Generic;

namespace Common
{
    public class GuidHelper
    {
        public static Guid GenerateComb()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }
        
        /// <summary>
        /// �����Զ���ֵ��GUID������������ʹ��
        /// </summary>
        /// <param name="seedCount"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static Queue<Guid> CreateMockGuidQueue(int seedCount, string prefix = "")
        {
            if (prefix == null)
            {
                throw new ArgumentNullException("prefix");
            }

            if (seedCount <= 0 || seedCount > 99999)
            {
                throw new ArgumentException("seedCount�������1��99999֮��");
            }

            int totalCount = seedCount.ToString().Length + prefix.Length;
            if (totalCount > 6)
            {
                throw new ArgumentException("seedCount��prefix�������Guid�ĺ���λ");
            }

            string zeroFormat = "";
            for (int i = 0; i < 6 - prefix.Length; i++)
            {
                zeroFormat = zeroFormat + "0";
            }

            var guids = new Queue<Guid>();
            for (int i = 1; i <= seedCount; i++)
            {
                guids.Enqueue(new Guid(string.Format("00000000-0000-0000-0000-000000{0}{1}", prefix, i.ToString(zeroFormat))));
            }

            return guids;
        }
    }
}