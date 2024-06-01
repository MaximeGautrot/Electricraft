using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : Transistor
{
    [SerializeField] private int powerDelay;
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
                if(t.GetNeighborOnId()!=id) //Si le voisin n'est pas lui meme allumé grace a ce fil
                {
                    numberNeighborsOn += 1;
                    SetNeighborOnId(t.GetId()); //On sauvegarde l'id du voisin qui allume ce fil
                }
            }
        }

        if(numberNeighborsOn > 0)
        {
            StartCoroutine(DelayTimeOn(powerDelay));
        }else{
            if(GetIsOn())
            {
                StartCoroutine(DelayTimeOff(powerDelay));
            }
        }
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
}
