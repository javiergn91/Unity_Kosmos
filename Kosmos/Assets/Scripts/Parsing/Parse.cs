using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosmos.Parsing
{
    public class Parse : MonoBehaviour
    {
        public static string SecondsToString(float currentTimeInSeconds)
        {
            int toInt = (int)currentTimeInSeconds;
            int hours = toInt / 3600;
            toInt = toInt % 3600;
            int minutes = toInt / 60;
            toInt = toInt % 60;
            int seconds = toInt;
            int miliseconds = (int)((currentTimeInSeconds - (int)currentTimeInSeconds) * 1000);

            return ToDigits(hours, 2) + ":" + ToDigits(minutes, 2) + ":" + ToDigits(seconds, 2) + "." + ToDigits(miliseconds, 3);
        }

        public static string ToDigits(int time, int n)
        {
            string trailingZeroes = "";

            for(int i = n - 1; i > 0; i--)
            {
                int limit = (int)Mathf.Pow(10, i);

                if(time < limit)
                {
                    trailingZeroes += "0";
                }
            }
            
            return trailingZeroes + time;
        }
    }
}


