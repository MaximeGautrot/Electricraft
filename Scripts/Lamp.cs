using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Transistor
{
    protected bool lightIsOn;
    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
        LightOff();
    }

    // Update is called once per frame
    void Update()
    {
        uint numberNeighborsOn = 0;

        foreach (Transistor t in neighbors)
        {
            if(t.GetIsOn())
            {
                numberNeighborsOn += 1;
            }
        }

        if(numberNeighborsOn > 0)
        {
            LightOn();
        }else{
            LightOff();
        }
    }
    
    public void LightOn()
    {
        lightIsOn = true;
    }
    public void LightOff()
    {
        lightIsOn = false;
    }
    public override bool GetLightIsOn()
    {
        return lightIsOn;
    }
}
