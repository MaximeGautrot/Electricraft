using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transistor : MonoBehaviour
{
    ////
    protected int id;
    protected int neighborOnId;
    ////
    
    protected bool isOn;
    protected List<Transistor> neighbors = new List<Transistor>();
    protected Vector3 center = new Vector3(0f, 0f, 0f);

    ////
    public void SetId(int i)
    {
        id = i;
    }
    public int GetId()
    {
        return id;
    }
    public void SetNeighborOnId(int i)
    {
        neighborOnId = i;
    }
    public int GetNeighborOnId()
    {
        return neighborOnId;
    }

    public List<Transistor> GetNeighbors()
    {
        return neighbors;
    }
    ////

    public void SetCenter(Vector3 vect)
    {
        center = vect;
    }
    public Vector3 GetCenter()
    {
        return center;
    }

    public void AddNeighbor(Transistor neighbor)
    {
        if (!IsNeighborInList(neighbor))
        {
            neighbors.Add(neighbor);
        }
    }

    public void AddNeighborListe(List<Transistor> listeNeighbors)
    {
        foreach (Transistor n in listeNeighbors)
        {
            if (!IsNeighborInList(n))
            {
                neighbors.Add(n);
            }
        }

        foreach (Transistor n  in neighbors)
        {
            if (!neighbors.Contains(n))
            {
                neighbors.Remove(n);
            }
        }
    }

    public void DeleteNeighbor(Transistor neighbor)
    {
        if (IsNeighborInList(neighbor))
        {
            neighbors.Remove(neighbor);
        }
    }

    public List<Transistor> GetNeighbor()
    {
        return neighbors;
    }

    public virtual void PowerOn()
    {
        isOn = true;
    }
    public virtual void PowerOff()
    {
        isOn = false;
    }
    public bool GetIsOn()
    {
        return isOn;
    }
    
    public bool IsNeighborInList(Transistor neighbor)
    {
        return neighbors.Contains(neighbor);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
