using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : Transistor
{
    [SerializeField] private int powerDelay;

    private List<Transistor> poweredNeighbors = new List<Transistor>();
    private Transistor transiPowerOn = null;
    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetIsOn())
        {
            transiPowerOn = FindFirstSource(this);
            if (transiPowerOn != null)
            {
                StartCoroutine(DelayTimeOn(powerDelay));
            }
        }
        else
        {
            if (!transiPowerOn.GetIsOn())
            {
                StartCoroutine(DelayTimeOff(powerDelay));
            }
        }

        
        /*List<int> sources = FindSourceOn();
        sources.Remove(GetId());

        if(sources.Count > 0)
        {
            StartCoroutine(DelayTimeOn(powerDelay));
        }
        else
        {
            if(GetIsOn())
            {
                StartCoroutine(DelayTimeOff(powerDelay));
            }
        }*/
    }
    private IEnumerator DelayTimeOn(int time)
    {
        yield return new WaitForSeconds(time); // pdt time, le programme peut continuer d'update autre part
        PowerOn();
    }
    private IEnumerator DelayTimeOff(int time)
    {
        yield return new WaitForSeconds(time); // pdt time, le programme peut continuer d'update autre part
        PowerOff();
        SetNeighborOnId(-1);
    }
    public void SetPowerDelay(int time)
    {
        powerDelay = time;
    }
    public int GetPowerDelay()
    {
        return powerDelay;
    }
    public List<Transistor> GetPoweredNeighbors()
    {
        return poweredNeighbors;
    }

    public Transistor GetTransiPowerOn()
    {
        return transiPowerOn;
    }
}
