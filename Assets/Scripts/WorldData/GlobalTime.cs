using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    public uint duration = 0;
    public uint minute = 0;
    public uint hour = 0;
    public uint day = 1;
    public uint month = 1;
    public uint year = 1848;


    public int DelayAmount = 1;
    protected float Timer;

    // Update is called once per frame
    void Update()
    {
        Timer += (Time.deltaTime);

        if (Timer >= DelayAmount)
        {
            Timer = 0f;

            duration++;
            minute++;
            if (minute >= 60)
            {
                hour++;
                minute = 0;
            }
            if (hour > 23)
            {
                hour = 0;
                duration = 0;
                day++;
            }

            switch (day)
            {
                case 29:
                    if (month == 2 && !((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                    {
                        day = 1;
                        month++;
                    }
                    break;
                case 30:
                    if (month == 2 && ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                    {
                        day = 1;
                        month++;
                    }
                    break;
                case 31: 
                    if (month == 4 || month == 6 || month == 9 || month == 11)
                    {
                        day = 1;
                        month++;
                    }
                    break;
                case 32:
                    if(!(month == 2 || month == 4 || month == 6 || month == 9 || month == 11))
                    {
                        day = 1;
                        month++;
                    }

                    if(month > 12)
                    {
                        year++;
                        month = 1;
                    }
                    break;
            }
        }
    }
}
