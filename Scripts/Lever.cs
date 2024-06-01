using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Transistor
{
    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
        SetNeighborOnId(-1);
    }

    public void Turn()
    {
        if(GetIsOn())
        {
            PowerOff();
        }else{
            PowerOn();
        }
    }
}
