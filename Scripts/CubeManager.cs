using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject blenderCubePrefab;
    public GameObject blenderCubePrefabOn;
    public ElecManager elecManager;
    public Vector3[,,] positionMatrix;
    private GameObject[,,] cubeMatrix;
    public GameObject redBall;
    private Vector3Int previousBallPosition;

    void Start()
    {
        GetPositionMatrix();

        if (blenderCubePrefab == null || positionMatrix == null)
        {
            Debug.LogError("Blender Cube Prefab or Position Matrix is not set.");
            return;
        }

        InitializeCubeMatrix();
        PlaceCubes();

        previousBallPosition = GetVector3IntBall();
    }

    void Update()
    {
        if (elecManager.GetN1() != positionMatrix.GetLength(0) || elecManager.GetN2() != positionMatrix.GetLength(1) || elecManager.GetN3() != positionMatrix.GetLength(2))
        {
            RemoveAllCubes();
            GetPositionMatrix();
            InitializeCubeMatrix();
            PlaceCubes();
        }
        UpdateCubeState();
        previousBallPosition = GetVector3IntBall();
    }

    private Vector3Int GetVector3IntBall()
    {
        Vector3 ballPosition = redBall.transform.position;

        int x = Mathf.Abs(Mathf.FloorToInt((ballPosition.x + 10) / 20));
        int y = Mathf.Abs(Mathf.FloorToInt((ballPosition.y + 10) / 20));
        int z = Mathf.Abs(Mathf.FloorToInt((ballPosition.z + 10) / 20));

        return new Vector3Int(x, y, z);
    }
    void PlaceCubes()
    {
        int n1 = positionMatrix.GetLength(0);
        int n2 = positionMatrix.GetLength(1);
        int n3 = positionMatrix.GetLength(2);


        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    Vector3 position = positionMatrix[i, j, k];
                    GameObject cubeInstance = Instantiate(blenderCubePrefab, position, Quaternion.identity);
                    cubeInstance.transform.SetParent(transform);
                    cubeMatrix[i, j, k] = cubeInstance;
                }
            }
        }
    }

    public void RemoveAllCubes()
    {
        int n1 = positionMatrix.GetLength(0);
        int n2 = positionMatrix.GetLength(1);
        int n3 = positionMatrix.GetLength(2);

        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    GameObject cube = cubeMatrix[i, j, k];
                    if (cube != null)
                    {
                        Destroy(cube);
                        cubeMatrix[i, j, k] = null;
                    }
                }
            }
        }
    }

    void UpdateCubeState()
    {
        // Récupérer la position de la boule rouge
        Vector3 ballPosition = redBall.transform.position;

        Vector3Int ballPositionInt = GetVector3IntBall();

        GameObject cube = cubeMatrix[ballPositionInt.x, ballPositionInt.z, ballPositionInt.y];
        GameObject lastCube = cubeMatrix[previousBallPosition.x, previousBallPosition.z, previousBallPosition.y];
        if (cube != null)
        {
            // Vérifier si la boule rouge est dans le cube actuel
            if (cube.tag == "BasicCube")
            {
                // Remplacer le cube actuel par un nouveau cube
                changeCube(ballPositionInt.x, ballPositionInt.z, ballPositionInt.y);
                Debug.Log("Oui");

                // Arrêter la boucle, car la boule rouge est dans un cube
                return;
            }
        }

        if (lastCube != null)
        {
            if (ballPositionInt != previousBallPosition)
            {
                changeCubeToBasic(previousBallPosition.x, previousBallPosition.z, previousBallPosition.y);
            }
        }
    }

    void changeCubeToBasic(int x, int y, int z)
    {
        Debug.Log("ChangeCubeAppeler");
        if (x >= 0 && x < cubeMatrix.GetLength(0) &&
            y >= 0 && y < cubeMatrix.GetLength(1) &&
            z >= 0 && z < cubeMatrix.GetLength(2))
        {
            GameObject oldCube = cubeMatrix[x, y, z];
            if (oldCube != null)
            {
                // Enregistrer la position et la rotation du cube actuel
                Vector3 position = oldCube.transform.position;
                Quaternion rotation = oldCube.transform.rotation;

                // Détruire le cube actuel
                Destroy(oldCube);

                // Instancier le cube basique à la même position et rotation
                GameObject newCube = Instantiate(blenderCubePrefab, position, rotation);

                // Mettre à jour la référence dans le tableau cubeMatrix
                cubeMatrix[x, y, z] = newCube;
            }
        }
        else
        {
            Debug.LogError("Invalid cube coordinates.");
        }
    }

    void changeCube(int x, int y, int z)
    {
        Debug.Log("ChangeCubeAppeler");
        if (x >= 0 && x < cubeMatrix.GetLength(0) &&
            y >= 0 && y < cubeMatrix.GetLength(1) &&
            z >= 0 && z < cubeMatrix.GetLength(2))
        {
            GameObject oldCube = cubeMatrix[x, y, z];
            if (oldCube != null)
            {
                // Enregistrer la position et la rotation du cube actuel
                Vector3 position = oldCube.transform.position;
                Quaternion rotation = oldCube.transform.rotation;

                // Détruire le cube actuel
                Destroy(oldCube);

                // Instancier le nouveau cube à la même position et rotation
                GameObject newCube = Instantiate(blenderCubePrefabOn, position, rotation);

                // Mettre à jour la référence dans le tableau cubeMatrix
                cubeMatrix[x, y, z] = newCube;
            }
        }
        else
        {
            Debug.LogError("Invalid cube coordinates.");
        }
    }

     void InitializeCubeMatrix()
    {
        int n1 = positionMatrix.GetLength(0);
        int n2 = positionMatrix.GetLength(1);
        int n3 = positionMatrix.GetLength(2);
        cubeMatrix = new GameObject[n1, n2, n3];

        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                for (int k = 0; k < n3; k++)
                {
                    cubeMatrix[i, j, k] = null;
                }
            }
        }
    }

    void GetPositionMatrix()
    {
        positionMatrix = elecManager.GenerateVectorMatrix();
        Debug.Log(elecManager.GenerateVectorMatrix().GetLength(0));
    }
}
