using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public GlobalTime gt;

    public int DelayAmount = 1;
    protected float Timer;
    const int summer = 14774400, winter = 30585600;
    const float wintersol = 19f, summersol = 65f;
    const float maxduration = 1440;
    const float yearduration = 31532399;

    float elevation, azimuth, step, yearcycle, daycycle;

    Vector3 altitude, highpoint, lowpoint;


    // Update is called once per frame
    void FixedUpdate()
    {
        Timer += (Time.deltaTime * gt.multiplier);
        if(Timer >= DelayAmount)
        {
            Timer = 0f;

            solstice();
            sunorbit();
        }
    }

    public void solstice()
    {
        yearcycle = (gt.offsetsincenewyear+907200) / yearduration;
        float rise, set;

        rise = (yearcycle * 2);
        set = ((yearcycle*2)*-1)+2;
        //Debug.Log($"Ongoing Year is {yearcycle*100} complete, solstice rise at {rise} and solstice set at {set}");

        //Summer Solstice to Winter Solstice
        if((gt.offsetsincenewyear > summer) && (gt.offsetsincenewyear < winter))
        {
            elevation = Mathf.Lerp(wintersol, summersol, set);
            //Debug.Log($"{elevation}, Summer Solstice to Winter Solstice");
        }

        //Winter Solstice to Summer Solstice
        if((yearcycle < 0.4711725 && yearcycle > 0.0000000) || (yearcycle > 0.9727138 && yearcycle < 0.9999))
        {
            elevation = Mathf.Lerp(wintersol, summersol, rise);
            //Debug.Log($"{elevation}, Winter Solstice to Summer Solstice");
        }
    }

    public void sunorbit()
    {
        highpoint = new Vector3(elevation, 0, 0);
        lowpoint = new Vector3(-elevation, 0, 0);

        //Daycycle
        daycycle = gt.duration / maxduration;
        float rise, set;

        rise = daycycle * 2;
        set = ((daycycle*2)*-1)+2;
        //Debug.Log($"Ongoing Day is {daycycle*100} complete, zenith rise at {rise} and zenith set at {set}");

        //Debug.Log(daycycle);

        if((azimuth >= 0) && (azimuth < 180))
        {
            altitude = Vector3.Slerp(lowpoint, highpoint, rise);
            //Debug.Log(altitude);
            
        }

        if((azimuth >= 180) && ((azimuth < 360) || (azimuth > -90)))
        {
            altitude = Vector3.Slerp(lowpoint, highpoint, set);
            //Debug.Log(altitude);
        }

        step = (((maxduration / 360f) / 4f) / 4f);
        //Debug.Log(step);

        azimuth = gt.duration * step;
        //Debug.Log($"Duration: {gt.duration}, Azimuth: {azimuth}");

        transform.eulerAngles = new Vector3(altitude.x, azimuth, 0);
    }
}
