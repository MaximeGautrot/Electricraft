using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Transistor
{
    public Box ConnectedBox = null;
    // Start is called before the first frame update
    void Start()
    {
        PowerOn();
    }

    // Update is called once per frame
    void Update()
    {
        if(ConnectedBox != null)
        {
            if(ConnectedBox.GetIsOn())
            {
                PowerOff();
            }else{
                PowerOn();
            }
        }else{
            PowerOn();
        }
    }

    public void SetBox(Box b)
    {
        ConnectedBox = b;
    }

    public void DeleteConnectedBox()
    {
        ConnectedBox = null;
    }

    public Box GetConnectedBox()
    {
        return ConnectedBox;
    }
}
