using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : Transistor
{
    //liste des voisins qui sont choisi par l'utilisateur pour etre lié au fil
    protected List<Transistor> connectedNeighbors = new List<Transistor>();
    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
    }

    // Update is called once per frame
    void Update()
    {
        uint numberNeighborsOn = 0;

        foreach (Transistor t in connectedNeighbors)
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
    //quand on pose un fil sur une case, un point apparait au milieu de cette case puis on doit cliquer sur les cases voisines qui seront connectés a ce fil, pour changer on reclique sur le fil, et pour deselctionné on reclique sur les voisins
    public void AddConnectedNeighbor(Transistor neighbor)
    {
        connectedNeighbors.Add(neighbor);
    }
    public void DeleteConnectedNeighbor(Transistor neighbor)
    {
        connectedNeighbors.Remove(neighbor);
    }
    public List<Transistor> GetConnectedNeighbor()
    {
        return connectedNeighbors;
    }
}
