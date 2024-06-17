using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transistor : MonoBehaviour
{
    ////
    protected int id;
    protected int neighborOnId = -1;
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
            if (!listeNeighbors.Contains(n))
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

    public virtual bool GetLightIsOn()
    {
        return false;
    }

    public virtual void Turn()
    {

    }

    public virtual void Push()
    {
        
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

    public virtual Box GetConnectedBox()
    {
        return null;
    }

    public virtual List<Torch> GetTorchConnected()
    {
        return  null;
    }

    public List<int> FindSourceOn(HashSet<Transistor> visited = null)
    {
        if (visited == null)
        {
            visited = new HashSet<Transistor>();
        }

        if (visited.Contains(this))
        {
            return new List<int>(); // Boucle détectée, retourne -1
        }

        visited.Add(this);

        if ((this is Wire))
        {
            List<int> output = new List<int>();
            foreach (Transistor neighbor in neighbors)
            {
                output.AddRange(neighbor.FindSourceOn(visited));
            }
            return output;
        }
        else if (this is Box box)
        {
            List<int> output = new List<int>();
            foreach (Transistor neighbor in neighbors)
            {
                output.AddRange(neighbor.FindSourceOn(visited));
            }
            foreach (Transistor t in GetTorchConnected())
            {
                if (output.Contains(t.GetId()))
                {
                    output.Remove(t.GetId());
                }
            }

            return output;
        }
        else if (this is Lamp)
        {
            return new List<int>();
        }
        else if (this is Relay)
        {
            List<int> output = new List<int>();
            foreach (Transistor neighbor in neighbors)
            {
                output.AddRange(neighbor.FindSourceOn(visited));
            }
            if (GetIsOn())
            {
                return new List<int>() {GetId()};
            }
            else
            {
                return new List<int>();
            }
        }
        else
        {
            if (GetIsOn())
            {
                return new List<int>() {GetId()};
            }
            else
            {
                return new List<int>();
            }
        }
    }

    public List<Relay> FindRelay(HashSet<Transistor> visited = null)
    {
        if (visited == null)
        {
            visited = new HashSet<Transistor>();
        }

        if (visited.Contains(this))
        {
            return new List<Relay>(); // Boucle détectée, retourne -1
        }

        visited.Add(this);

        if ((this is Wire))
        {
            List<Relay> output = new List<Relay>();
            foreach (Transistor neighbor in neighbors)
            {
                output.AddRange(neighbor.FindRelay(visited));
            }
            return output;
        }
        else if (this is Box box)
        {
            List<Relay> output = new List<Relay>();
            foreach (Transistor neighbor in neighbors)
            {
                if (neighbor is Torch t)
                {
                    if (!GetTorchConnected().Contains(t))
                    {
                        output.AddRange(neighbor.FindRelay(visited));
                    }
                }
            }
            return output;
        }
        else if (this is Lamp)
        {
            return new List<Relay>();
        }
        else if (this is Relay relay)
        {
            return new List<Relay>() {relay};
        }
        else
        {
            return new List<Relay>();
        }
    }

    public Transistor FindFirstSource(Transistor start)
    {
        HashSet<Transistor> visited = new HashSet<Transistor>();
        Queue<Transistor> queue = new Queue<Transistor>();
        queue.Enqueue(this);
        visited.Add(this);

        while (queue.Count > 0)
        {
            Transistor current = queue.Dequeue();

            if (current is Relay relay)
            {
                if (current != this)
                {
                    if (current.GetIsOn() && relay.GetTransiPowerOn() != start)
                    {
                        return current;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            else if(current is Button || current is Lever || current is Torch)
            {
                if (current.GetIsOn())
                {
                    return current;
                }
            }

            foreach (Transistor neighbor in current.neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }

            if (current is Box box)
            {
                foreach (Transistor t in box.GetTorchConnected())
                {
                    if (!visited.Contains(t))
                    {
                        visited.Add(t);
                        queue.Enqueue(t);
                    }
                }
            }
        }

        return null;
    }

}
