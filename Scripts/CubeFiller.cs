using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFiller : MonoBehaviour
{
    public int gridSize = 5; // Nombre de cubes sur un côté du grand cube
    public float cubeSize = 1f; // Taille d'un cube
    public float containerSize = 10f; // Taille du grand cube qui contient les petits cubes
    public float edgePercentage = 0.02f; // Pourcentage de la taille du cube pour les arêtes
    public Color cubeColor = new Color(0.5f, 0.5f, 1f); // Couleur des cubes

    private void Start()
    {
        GenerateCubes();
    }

    private void GenerateCubes()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    // Calcule la position de chaque cube dans le grand cube
                    float xPos = x * cubeSize - containerSize / 2 + cubeSize / 2;
                    float yPos = y * cubeSize - containerSize / 2 + cubeSize / 2;
                    float zPos = z * cubeSize - containerSize / 2 + cubeSize / 2;

                    // Crée un cube
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(xPos, yPos, zPos);
                    cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

                    // Ajuste la couleur des cubes
                    cube.GetComponent<Renderer>().material.color = cubeColor;

                    // Ajoute le script de détection de collision au cube
                    CubeCollisionDetector collisionDetector = cube.AddComponent<CubeCollisionDetector>();
                    collisionDetector.Initialize(this, cube);
                }
            }
        }
    }
}
