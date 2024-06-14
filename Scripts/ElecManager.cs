using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecManager : MonoBehaviour
{
    public int n1 = 5;
    public int n2 = 5;
    public int n3 = 3;

    public Transistor[,,] matrixTransistors = null;
    public GameObject[,,] matrixElements = null;
    public Vector3[,,] matrixVect;
    // Start is called before the first frame update
    public ObjectSelected objectSelected;
    public CubeManager cubeManager;

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
                        if (matrixElements[i, j, k].tag == "On" && !matrixTransistors[i, j, k].GetIsOn())
                        {
                            string element = matrixElements[i, j, k].GetType().Name + "Off";
                            Destroy(matrixElements[i, j, k]);
                            matrixElements[i, j, k] = Instantiate(objectSelected.GetElement(element), matrixTransistors[i, j, k].GetCenter(), Quaternion.identity);
                        }
                        else if (matrixElements[i, j, k].tag == "Off" && !matrixTransistors[i, j, k].GetIsOn())
                        {
                            string element = matrixElements[i, j, k].GetType().Name + "On";
                            Destroy(matrixElements[i, j, k]);
                            matrixElements[i, j, k] = Instantiate(objectSelected.GetElement(element), matrixTransistors[i, j, k].GetCenter(), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3Int position = cubeManager.GetVector3IntBall();
            string element = objectSelected.GetCurrentElement();
            if (element != null)
            {
                AddElement(position.x, position.z, position.y, element);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            Vector3Int position = cubeManager.GetVector3IntBall();
            if (matrixElements[position.x, position.z, position.y] != null)
            {
                DeleteTransistor(position.x, position.z, position.y);
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
            VerifierVoisins();
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
    public void SetMyValue1(float value)
    {
        DestroyTransistorMatrix();
        DestroyGameObjectMatrix();
        n1 = Mathf.RoundToInt(value);
        matrixTransistors = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
        matrixElements = GenerateGameObjectMatrix();
    }
    public void SetMyValue2(float value)
    {
        DestroyTransistorMatrix();
        DestroyGameObjectMatrix();
        n2 = Mathf.RoundToInt(value);;
        matrixTransistors = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
        matrixElements = GenerateGameObjectMatrix();
    }
    public void SetMyValue3(float value)
    {
        DestroyTransistorMatrix();
        DestroyGameObjectMatrix();
        n3 = Mathf.RoundToInt(value);;
        matrixTransistors = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
        matrixElements = GenerateGameObjectMatrix();
    }

    public void addTransistor(int i, int j, int k, string element)
    {
        Destroy(matrixTransistors[i, j, k]);
        Vector3 center = new Vector3(i * 20f, k * 20f, j * 20f);

        if (element == "Torch")
        {
            Torch torch = new GameObject("Torch").AddComponent<Torch>();
            torch.SetCenter(center);
            matrixTransistors[i, j, k] = torch;
        }
        else if (element == "Wire")
        {
            Wire wire = new GameObject("Wire").AddComponent<Wire>();
            wire.SetCenter(center);
            matrixTransistors[i, j, k] = wire;
        }
        else if (element == "Button")
        {
            Button button = new GameObject("Button").AddComponent<Button>();
            button.SetCenter(center);
            matrixTransistors[i, j, k] = button;
        }
        else if (element == "Lever")
        {
            Lever lever = new GameObject("Lever").AddComponent<Lever>();
            lever.SetCenter(center);
            matrixTransistors[i, j, k] = lever;
        }
        else if (element == "Lamp")
        {
            Lamp lamp = new GameObject("Lamp").AddComponent<Lamp>();
            lamp.SetCenter(center);
            matrixTransistors[i, j, k] = lamp;
        }
        else if (element == "CubeT")
        {
            Box cubeT = new GameObject("Box").AddComponent<Box>();
            cubeT.SetCenter(center);
            matrixTransistors[i, j, k] = cubeT;
        }
    }

    public void DeleteTransistor(int i, int j, int k)
    {
        Destroy(matrixTransistors[i, j, k]);
        Destroy(matrixElements[i, j, k]);
        Vector3 center = new Vector3(i * 20f, k * 20f, j * 20f);
        Transistor transistor = new GameObject("Transistor").AddComponent<Transistor>();
        transistor.SetCenter(center);
        matrixTransistors[i, j, k] = transistor;
        matrixElements[i, j, k] = null;
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

}
