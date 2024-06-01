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
                if(t is not Box)
                {
                    if(t is Torch torch)
                    {
                        if(torch.ConnectedBox != this)
                        {
                            numberNeighborsOn += 1;
                            SetNeighborOnId(t.GetId()); //On sauvegarde l'id du voisin qui allume ce fil
                        }
                    }
                    else
                    {
                        if(t.GetNeighborOnId()!=id) //Si le voisin n'est pas lui meme allumé grace a ce fil
                        {
                            numberNeighborsOn += 1;
                            SetNeighborOnId(t.GetId()); //On sauvegarde l'id du voisin qui allume ce fil
                        }
                    }
                }
              
            }
              
        }
        if(numberNeighborsOn > 0)
        {
            PowerOn();
        }else{
            PowerOff();
            SetNeighborOnId(-1);
        }
    }
}
