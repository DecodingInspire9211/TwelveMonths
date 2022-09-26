using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    public DateTime totalstart = new DateTime(1848, 01, 01, 00, 00, 00);
    public DateTime start = new DateTime(1848, 03, 13, 14, 23, 0);
    public DateTime current = new DateTime();

    public uint duration = 0;
    public long offset = 0, offsetsincenewyear = 0; 
    public int DelayAmount = 1;
    protected float Timer;

    public int minute, hour, day, month, year, weekday;

    [Range(0.5f, 100f)]
    public float multiplier = 1f;
    int _year;
    long timestampstart, timestamptotal;

    void Start()
    {
        minute = start.Minute;
        hour = start.Hour;
        day = start.Day;
        month = start.Month;
        year = start.Year;

        _year = start.Year;

        timestamptotal = ((DateTimeOffset)totalstart).ToUnixTimeSeconds();
        timestampstart = ((DateTimeOffset)start).ToUnixTimeSeconds();

        offsetsincenewyear = timestampstart - timestamptotal;

        weekday = (int)start.DayOfWeek;

        //Debug.Log($"Game starts on {weekday} the {day}.{month}.{year} at {hour}:{minute}");
        //Debug.Log($"First year offset is {offsetsincenewyear}");
    }

    void Update()
    {
        Timer += (Time.deltaTime * multiplier);

        if(Timer >= DelayAmount)
        {
            Timer = 0f;

            offset++;
            offsetsincenewyear += 60;
            //Debug.Log($"Offset since New Year {offsetsincenewyear}");
            duration++;

            current = start.AddMinutes(offset);

            minute = current.Minute;
            hour = current.Hour;
            day = current.Day;
            month = current.Month;
            year = current.Year;

            weekday = (int)current.DayOfWeek;

            if(minute==0 && hour==0)
            {
                duration=0;
            }

            if(offsetsincenewyear == 14774400)
            {
                Debug.Log("SUMMER - SUN AT HIGHEST ALTITUDE AND LARGEST AZIMUTH");
            }
            if(offsetsincenewyear == 30585600)
            {
                Debug.Log("WINTER - SUN AT LOWEST ALTITUDE AND SMALLEST AZIMUTH");
            }

            if(year != _year)
            {
                _year = current.Year;
                offsetsincenewyear = 0;
                Debug.Log($"Reset to {offsetsincenewyear}");
            }
        }
    }

    // Update is called once per frame
    /* void Update()
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
    } */
}
