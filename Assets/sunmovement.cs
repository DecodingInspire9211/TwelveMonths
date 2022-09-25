using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunmovement : MonoBehaviour
{

    public GlobalTime gt;

    public int DelayAmount = 1;
    protected float Timer;

    int maxduration = 1440;
    private float _rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        _rotationSpeed = (Time.deltaTime * 3600) / maxduration;
        transform.Rotate (0, _rotationSpeed, 0);
    }
}
