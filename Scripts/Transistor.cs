using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transistor : MonoBehaviour
{
    protected bool isOn;
    protected List<Transistor> neighbors = new List<Transistor>();
    protected Vector3 center = new Vector3(0f, 0f, 0f);

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
        neighbors.Add(neighbor);
    }
    public void DeleteNeighbor(Transistor neighbor)
    {
        neighbors.Remove(neighbor);
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
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}