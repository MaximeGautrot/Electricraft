using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Transistor
{
    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
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
            PowerOn();
        }else{
            PowerOff();
        }
    }
}
