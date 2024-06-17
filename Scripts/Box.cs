using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Transistor
{
    public List<Torch> torchConnected = new List<Torch>();
    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
    }

    // Update is called once per frame
    void Update()
    {
        List<int> sources = FindSourceOn();
        if(sources.Count == 0)
        {
            if(GetIsOn())
            {
                PowerOff();
            }
        }
        else
        {
            foreach (Transistor t in GetTorchConnected())
            {
                if (sources.Contains(t.GetId()))
                {
                    sources.Remove(t.GetId());
                }
            }
            if(!GetIsOn())
            {
                if(sources.Count > 0)
                {
                    PowerOn();
                }
            }
        }
    }

    public void AddTorch(Torch t)
    {
        torchConnected.Add(t);
    }

    public void DelTorch(Torch t)
    {
        torchConnected.Remove(t);
    }

    public bool contientTorch(Torch t)
    {
        return torchConnected.Contains(t);
    }

    public override List<Torch> GetTorchConnected()
    {
        return torchConnected;
    }
}
