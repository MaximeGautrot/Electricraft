using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Transistor
{
    [SerializeField] private int powerDuration;
    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
        SetNeighborOnId(-1);
    }

    public void Push()
    {
        if(!(GetIsOn()))
        {
            PowerOn();
            //permet de lancer une coroutine responsable de l'activation du boutton (fct pouvant etre mise en pause)
            StartCoroutine(PushDuration(powerDuration));
        }
    }
    private IEnumerator PushDuration(int duration)
    {
        yield return new WaitForSeconds(duration); // pdt duration, le programme peut continuer d'update autre part
        PowerOff();
    }
    public void SetPowerDuration(int duration)
    {
        powerDuration = duration;
    }
    public int GetPowerDuration()
    {
        return powerDuration;
    }
}
