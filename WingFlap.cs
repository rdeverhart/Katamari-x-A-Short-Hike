using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingFlap : MonoBehaviour
{
    float ElapsedTime;
    public int wingDir = 1; 

    // Update is called once per frame
    void Update()
    {
        ElapsedTime+=  Time.deltaTime;
        if (ElapsedTime> 3f)
        {
            ElapsedTime=0;
        }

        float wingRot = Mathf.Sin(ElapsedTime*10)*15;
        float wingPos = Mathf.Clamp(wingRot, -20, 30);
        transform.localRotation= Quaternion.Euler(wingDir*wingRot, 0, 0);
        
    }
}
