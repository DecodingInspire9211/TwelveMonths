using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunmovement : MonoBehaviour
{

    public GlobalTime gt;

    public int DelayAmount = 1;
    protected float Timer;

    float maxduration = 1440;

    float azimuth, step;

    Vector3 elevation, altitude;

    // Update is called once per frame
    void Update()
    {
        Timer += (Time.deltaTime * gt.multiplier);
        if(Timer >= DelayAmount)
        {
            Timer = 0f;
            Vector3 highpoint = new Vector3(19f, 0,0);
            Vector3 lowpoint = new Vector3(-19f, 0,0);

            float daycycle = gt.duration / maxduration;
            Debug.Log(daycycle);

            if((azimuth >= 0) && (azimuth < 180))
            {
                altitude = Vector3.Slerp(lowpoint, highpoint, daycycle*2);
                Debug.Log(altitude);
                
            }

            if((azimuth >= 180) && ((azimuth < 360) || (azimuth > -90)))
            {
                altitude = Vector3.Slerp(lowpoint, highpoint, ((daycycle*2)*-1)+2);
                Debug.Log(altitude);
            }

            step = (((maxduration / 360f) / 4f) / 4f);
            //Debug.Log(step);


            azimuth = gt.duration * step;
            //Debug.Log($"Duration: {gt.duration}, Azimuth: {azimuth}");

            transform.eulerAngles = new Vector3(altitude.x, azimuth, 0);
        }
    }
}
