using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecManager : MonoBehaviour
{
    public int n1 = 5;
    public int n2 = 5;
    public int n3 = 3;
    public int delay = 1;

    private int currentId = 0;

    public Transistor[,,] matrixTransistors = null;
    public GameObject[,,] matrixElements = null;
    public Vector3[,,] matrixVect;

    public ObjectSelected objectSelected;
    public CubeManager cubeManager;
    public List<GameObject> wireManager;
    public GameObject delaySlider;
    private Torch connectTorch = null;
    private Vector3Int torchPosition = new Vector3Int(-10, -10, -10);
    private Vector3Int boxPosition = new Vector3Int(-10, -10, -10);

    // Start is called before the first frame update
    void Start()
    {
        matrixTransistors = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
        matrixElements = GenerateGameObjectMatrix();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        VerifierVoisins();
        UpdateVisu();
    }

    void UpdateVisu()
    {
        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    if (matrixTransistors[i, j, k] != null)
                    {
                        string element;
                        if (matrixTransistors[i, j, k].GetType().Name == "Box")
                        {
                            element = "CubeT";
                        }
                        else
                        {
                            element = matrixTransistors[i, j, k].GetType().Name;
                        }
                        if (element == "Lamp")
                        {
                            if (matrixElements[i, j, k].tag == "On" && !matrixTransistors[i, j, k].GetLightIsOn())
                            {
                                element = element+"Off";
                                Destroy(matrixElements[i, j, k]);
                                GameObject elt = Instantiate(objectSelected.GetElement(element), matrixTransistors[i, j, k].GetCenter(), Quaternion.identity);
                                elt.transform.SetParent(transform);
                                matrixElements[i, j, k] = elt;
                            }
                            else if (matrixElements[i, j, k].tag == "Off" && matrixTransistors[i, j, k].GetLightIsOn())
                            {
                                element = element+"On";
                                Destroy(matrixElements[i, j, k]);
                                GameObject elt = Instantiate(objectSelected.GetElement(element), matrixTransistors[i, j, k].GetCenter(), Quaternion.identity);
                                elt.transform.SetParent(transform);
                                matrixElements[i, j, k] = elt;
                            } 
                        }
                        else
                        {
                            if (matrixTransistors[i, j, k] is Torch torch)
                            {
                                if(torch.GetConnectedBox() != null)
                                {
                                    Quaternion rota = GetRotationAngle(GetDirectionBoxTorch(torch));
                                    if (torch.GetIsOn())
                                    {
                                        element = element+"On";
                                        Destroy(matrixElements[i, j, k]);
                                        GameObject elt = Instantiate(objectSelected.GetElement(element), torch.GetCenter(), rota);
                                        elt.transform.SetParent(transform);
                                        matrixElements[i, j, k] = elt;
                                    }
                                    else
                                    {
                                        element = element+"Off";
                                        Destroy(matrixElements[i, j, k]);
                                        GameObject elt = Instantiate(objectSelected.GetElement(element), torch.GetCenter(), rota);
                                        elt.transform.SetParent(transform);
                                        matrixElements[i, j, k] = elt;
                                    }   
                                }
                                else
                                {
                                    Destroy(matrixElements[i, j, k]);
                                    GameObject elt = Instantiate(objectSelected.GetElement(element), torch.GetCenter(), Quaternion.identity);
                                    elt.transform.SetParent(transform);
                                    matrixElements[i, j, k] = elt;
                                }
                            }
                            else if (matrixElements[i, j, k].tag == "On" && !matrixTransistors[i, j, k].GetIsOn())
                            {
                                element = element+"Off";
                                Destroy(matrixElements[i, j, k]);
                                GameObject elt = Instantiate(objectSelected.GetElement(element), matrixTransistors[i, j, k].GetCenter(), Quaternion.identity);
                                elt.transform.SetParent(transform);
                                matrixElements[i, j, k] = elt;
                            }
                            else if (matrixElements[i, j, k].tag == "Off" && matrixTransistors[i, j, k].GetIsOn())
                            {
                                element = element+"On";
                                Destroy(matrixElements[i, j, k]);
                                GameObject elt = Instantiate(objectSelected.GetElement(element), matrixTransistors[i, j, k].GetCenter(), Quaternion.identity);
                                elt.transform.SetParent(transform);
                                matrixElements[i, j, k] = elt;
                            }
                            else if (matrixElements[i, j, k].tag == "Center")
                            {
                                
                            }
                        }
                    }
                }
            }
        }
        DeleteGameObjectListeWire();
        PlaceWire();
    }

    void PlaceWire()
    {
        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    if (matrixTransistors[i, j, k] != null)
                    {
                        List<Vector3> listeVect = GetDirectionVoisins(i, j, k);
                        Quaternion rota;
                        foreach(Vector3 vect in listeVect)
                        {
                            if (vect.x != 0)
                            {
                                rota = Quaternion.Euler(90, 0, 0);
                            }
                            else if (vect.y != 0)
                            {
                                rota = Quaternion.Euler(0, 0, 90);
                            }
                            else
                            {
                                rota = Quaternion.Euler(0, 90, 0);
                            }
                            if(matrixTransistors[i, j, k].GetIsOn() || matrixTransistors[i, j, k].GetLightIsOn())
                            {
                                GameObject elt = Instantiate(objectSelected.GetElement("WireOn"), matrixTransistors[i, j, k].GetCenter() + vect*5f, rota);
                                elt.transform.SetParent(transform);
                                wireManager.Add(elt);
                            }
                            else
                            {
                                GameObject elt = Instantiate(objectSelected.GetElement("WireOff"), matrixTransistors[i, j, k].GetCenter() + vect*5f, rota);
                                elt.transform.SetParent(transform);
                                wireManager.Add(elt);
                            }
                        }
                    }
                }
            }
        }
    }

    void DeleteGameObjectListeWire()
    {
        for (int i = wireManager.Count - 1; i >= 0; i--)
        {
            GameObject gameObject = wireManager[i];
            wireManager.RemoveAt(i);
            Destroy(gameObject);
        }
    }

    void HandleInput()
    {
        Vector3Int position = cubeManager.GetVector3IntBall();
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            string element = objectSelected.GetCurrentElement();
            if (element != null)
            {
                AddElement(position.x, position.z, position.y, element);
                
            }
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (matrixElements[position.x, position.z, position.y] != null)
            {
                DeleteTransistor(position.x, position.z, position.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ActiverElement(position.x, position.z, position.y);
        }

        if (Input.GetKeyDown(KeyCode.R) && (connectTorch != null))
        {
            connectTorchFunc(position.x, position.z, position.y);
        }
    }

    void connectTorchFunc(int i, int j, int k)
    {
        if (matrixTransistors[i, j, k] is Torch torch)
        {
            connectTorch = torch;
            torchPosition = new Vector3Int(i, j, k);
        }
        else
        {
            boxPosition = new Vector3Int(i, j, k);
            if(IsAdjacent(torchPosition, boxPosition))
            {
                Transistor t = matrixTransistors[i, j, k];
                if(t is Box box)
                {
                    if(IsAdjacent(new Vector3Int(i, j, k), torchPosition))
                    {
                        if (connectTorch.GetConnectedBox() != null)
                        {
                            connectTorch.GetConnectedBox().DelTorch(connectTorch);
                        }
                        connectTorch.SetBox(box);
                        box.AddTorch(connectTorch);
                        boxPosition = new Vector3Int(-10, -10, -10);
                        torchPosition = new Vector3Int(-10, -10, -10);
                    }
                }
            }
        }
    }
    
    bool IsAdjacent(Vector3Int currentPosition, Vector3Int targetPosition)
    {
        Vector3Int[] directions = {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, 0, -1),
            new Vector3Int(0, 0, 1)
        };

        foreach (Vector3Int direction in directions)
        {
            if (targetPosition == currentPosition + direction)
            {
                return true;
            }
        }

        return false;
    }

    void ActiverElement(int i, int j, int k)
    {
        Transistor t = matrixTransistors[i, j, k];
        if (t != null)
        {
            string nom = t.GetType().Name;

            if(nom == "Lever")
            {
                t.Turn();
            }
            else if (nom == "Button")
            {
                t.Push();
            }
        }
    }

    private void AddElement(int i, int j, int k, string element)
    {
        if (matrixElements[i, j, k] == null)
        {
            Vector3 position = matrixVect[i, j, k];
            GameObject elt = Instantiate(objectSelected.GetElement(element), position, Quaternion.identity);
            elt.transform.SetParent(transform);
            matrixElements[i, j, k] = elt;
            addTransistor(i, j, k, element);

            DeleteGameObjectListeWire();
            PlaceWire();
        }
    }

    private void VerifierVoisins()
    {
        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    if (matrixElements[i, j, k] != null)
                    {
                        matrixTransistors[i, j, k].AddNeighborListe(VisiterVoisins(i, j, k));
                    }
                }
            }
        }
    }

    public GameObject[,,] GenerateGameObjectMatrix()
    {
        GameObject[,,] matrix = new GameObject[n1, n2, n3];

        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    matrix[i, j, k] = null;
                }
            }
        }

        return matrix;
    }

    public void DestroyGameObjectMatrix()
    {
        for (int i = 0; i < matrixElements.GetLength(0); i++)
        {
            for (int j = 0; j < matrixElements.GetLength(1); j++)
            {
                for (int k = 0; k < matrixElements.GetLength(2); k++)
                {
                    Destroy(matrixElements[i, j, k]);
                }   
            }
        }

    }

    public Transistor[,,] GenerateTransistorMatrix()
    {
        Transistor[,,] matrix = new Transistor[n1, n2, n3];

        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    matrix[i, j, k] = null;
                }
            }
        }

        return matrix;
    }

    public void DestroyTransistorMatrix()
    {
        for (int i = 0; i < matrixTransistors.GetLength(0); i++)
        {
            for (int j = 0; j < matrixTransistors.GetLength(1); j++)
            {
                for (int k = 0; k < matrixTransistors.GetLength(2); k++)
                {
                    Destroy(matrixTransistors[i, j, k]);
                }   
            }
        }
    }

    public Vector3[,,] GenerateVectorMatrix()
    {
        // The type of matrixVect should be Vector3[,] instead of Vector3
        Vector3[,,] matrixVect = new Vector3[n1, n2, n3];

        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    matrixVect[i, j, k] =  new Vector3(i * 20f, k * 20f, j * 20f);
                }
            }
        }

        return matrixVect;
    }

    public int GetN1()
    {
        return n1;
    }
    public int GetN2()
    {
        return n2;
    }
    public int GetN3()
    {
        return n3;
    }
    public int GetDelay()
    {
        return delay;
    }
    public void SetMyValue1(float value)
    {
        DestroyTransistorMatrix();
        DestroyGameObjectMatrix();
        DeleteGameObjectListeWire();
        n1 = Mathf.RoundToInt(value);
        matrixTransistors = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
        matrixElements = GenerateGameObjectMatrix();
    }
    public void SetMyValue2(float value)
    {
        DestroyTransistorMatrix();
        DestroyGameObjectMatrix();
        DeleteGameObjectListeWire();
        n2 = Mathf.RoundToInt(value);;
        matrixTransistors = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
        matrixElements = GenerateGameObjectMatrix();
    }
    public void SetMyValue3(float value)
    {
        DestroyTransistorMatrix();
        DestroyGameObjectMatrix();
        DeleteGameObjectListeWire();
        n3 = Mathf.RoundToInt(value);;
        matrixTransistors = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
        matrixElements = GenerateGameObjectMatrix();
    }
    public void SetMyDelay(float value)
    {
        delay = Mathf.RoundToInt(value);
    }

    public void addTransistor(int i, int j, int k, string element)
    {
        Destroy(matrixTransistors[i, j, k]);
        Vector3 center = new Vector3(i * 20f, k * 20f, j * 20f);

        if (element == "Torch")
        {
            Torch torch = new GameObject("Torch").AddComponent<Torch>();
            torch.SetCenter(center);
            torch.SetId(currentId);
            currentId++;
            matrixTransistors[i, j, k] = torch;
            torchPosition = new Vector3Int(i, j, k);
            connectTorch = torch;
        }
        if (element == "Relay")
        {
            Relay relay = new GameObject("Relay").AddComponent<Relay>();
            relay.SetCenter(center);
            relay.SetId(currentId);
            currentId++;
            relay.SetPowerDelay(delay);
            matrixTransistors[i, j, k] = relay;
        }
        else if (element == "Wire")
        {
            Wire wire = new GameObject("Wire").AddComponent<Wire>();
            wire.SetCenter(center);
            wire.SetId(currentId);
            currentId++;
            matrixTransistors[i, j, k] = wire;
        }
        else if (element == "Button")
        {
            Button button = new GameObject("Button").AddComponent<Button>();
            button.SetCenter(center);
            button.SetId(currentId);
            currentId++;
            button.SetPowerDuration(delay);
            matrixTransistors[i, j, k] = button;
        }
        else if (element == "Lever")
        {
            Lever lever = new GameObject("Lever").AddComponent<Lever>();
            lever.SetCenter(center);
            lever.SetId(currentId);
            currentId++;
            matrixTransistors[i, j, k] = lever;
        }
        else if (element == "Lamp")
        {
            Lamp lamp = new GameObject("Lamp").AddComponent<Lamp>();
            lamp.SetCenter(center);
            lamp.SetId(currentId);
            currentId++;
            matrixTransistors[i, j, k] = lamp;
        }
        else if (element == "CubeT")
        {
            if(boxPosition != new Vector3Int(-10, -10, -10))
            {
                if (boxPosition.x == i && boxPosition.z == j && boxPosition.y == k)
                {
                    Box cubeT = new GameObject("Box").AddComponent<Box>();
                    cubeT.SetCenter(center);
                    cubeT.SetId(currentId);
                    currentId++;
                    matrixTransistors[i, j, k] = cubeT;
                    connectTorch.SetBox(cubeT);
                    boxPosition = new Vector3Int(-10, -10, -10);
                    connectTorch = null;
                }
            }
            else
            {
                Box cubeT = new GameObject("Box").AddComponent<Box>();
                cubeT.SetCenter(center);
                cubeT.SetId(currentId);
                currentId++;
                matrixTransistors[i, j, k] = cubeT;
            }
            
        }
    }

    public void DeleteTransistor(int i, int j, int k)
    {
        if (matrixTransistors[i, j, k] == connectTorch)
        {
            connectTorch = null;
            boxPosition = new Vector3Int(-10, -10, -10);
        }
        else if (matrixTransistors[i, j, k] is Box box)
        {
            foreach(Torch t in box.GetTorchConnected())
            {
                t.DeleteConnectedBox();
            }
        }

        matrixTransistors[i, j, k].PowerOff();
        Destroy(matrixTransistors[i, j, k]);
        Destroy(matrixElements[i, j, k]);
        matrixTransistors[i, j, k] = null;
        matrixElements[i, j, k] = null;

        DeleteGameObjectListeWire();
        PlaceWire();
    }

    public List<Transistor> VisiterVoisins(int i, int j, int k)
    {
        List<Transistor> voisins = new List<Transistor>();
        // Définissez les déplacements possibles pour les voisins
        int[,] directions = { { -1, 0, 0 }, { 1, 0, 0 }, { 0, -1, 0 }, { 0, 1, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };

        // Parcourez tous les déplacements possibles
        for (int d = 0; d < directions.GetLength(0); d++)
        {
            // Calculez les coordonnées du voisin
            int x = i + directions[d, 0];
            int y = j + directions[d, 1];
            int z = k + directions[d, 2];

            // Vérifiez que les coordonnées du voisin sont dans les limites de la matrice
            if (x >= 0 && x < matrixTransistors.GetLength(0) && y >= 0 && y < matrixTransistors.GetLength(1) && z >= 0 && z < matrixTransistors.GetLength(2))
            {
                if (matrixTransistors[x, y, z] != null)
                {
                    voisins.Add(matrixTransistors[x, y, z]);
                }
            }
        }

        return voisins;
    }

    public List<Vector3> GetDirectionVoisins(int i, int j, int k)
    {
        List<Vector3> output = new List<Vector3>();
        int[,] directions = { { -1, 0, 0 }, { 1, 0, 0 }, { 0, -1, 0 }, { 0, 1, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };

        // Parcourez tous les déplacements possibles
        for (int d = 0; d < directions.GetLength(0); d++)
        {
            // Calculez les coordonnées du voisin
            int x = i + directions[d, 0];
            int y = j + directions[d, 1];
            int z = k + directions[d, 2];

            // Vérifiez que les coordonnées du voisin sont dans les limites de la matrice
            if (x >= 0 && x < matrixTransistors.GetLength(0) && y >= 0 && y < matrixTransistors.GetLength(1) && z >= 0 && z < matrixTransistors.GetLength(2))
            {
                if (matrixTransistors[x, y, z] != null)
                {
                    output.Add(new Vector3(directions[d, 0], directions[d, 2], directions[d, 1]));
                }
            }
        }

        return output;
    }

    public Vector3 GetDirectionBoxTorch(Torch t)
    {
        List<Vector3> output = new List<Vector3>();
        int[,] directions = { { -1, 0, 0 }, { 1, 0, 0 }, { 0, -1, 0 }, { 0, 1, 0 }, { 0, 0, -1 }, { 0, 0, 1 } };

        // Parcourez tous les déplacements possibles
        for (int d = 0; d < directions.GetLength(0); d++)
        {
            // Vérifiez que les coordonnées du voisin sont dans les limites de la matrice
            Vector3 vectOut = new Vector3(directions[d, 0], directions[d, 2], directions[d, 1]);
            Vector3 vect = vectOut * 20f;
            if (Mathf.Approximately((vect + t.GetCenter()).x, t.GetConnectedBox().GetCenter().x) &&
                Mathf.Approximately((vect + t.GetCenter()).y, t.GetConnectedBox().GetCenter().y) &&
                Mathf.Approximately((vect + t.GetCenter()).z, t.GetConnectedBox().GetCenter().z))
            {
                return vectOut;
            }
        }
    return new Vector3(0, 0, 0);
}
    private Quaternion GetRotationAngle(Vector3 direction)
    {
        if (direction == Vector3.up)
        {
            return Quaternion.Euler(0, 0, -90f);
        }
        else if (direction == Vector3.down)
        {
            return Quaternion.Euler(0, 0, 90f);
        }
        else if (direction == Vector3.left)
        {
            return Quaternion.Euler(0, 0, 0);
        }
        else if (direction == Vector3.right)
        {
            return Quaternion.Euler(0, 180f, 0);
        }
        else if (direction == Vector3.forward)
        {
            return Quaternion.Euler(0, 90f, 0);
        }
        else if (direction == Vector3.back)
        {
            return Quaternion.Euler(0, -90f, 0);
        }
        else
        {
            Debug.LogError("Invalid direction");
            return Quaternion.Euler(0, 0, 0);
        }
    }
}
