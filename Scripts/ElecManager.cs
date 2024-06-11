using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecManager : MonoBehaviour
{
    public int n1 = 5;
    public int n2 = 5;
    public int n3 = 3;

    public Transistor[,,] matrix;

    public Vector3[,,] matrixVect;
    // Start is called before the first frame update
    void Start()
    {
        matrix = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    Vector3 center = new Vector3(i * 20f, k * 20f, j * 20f);
                    Transistor transistor = new GameObject("Transistor").AddComponent<Transistor>();
                    transistor.SetCenter(center);
                    matrix[i, j, k] = transistor;
                    Debug.Log(matrix[i, j, k]);
                }
            }
        }

        return matrix;
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
        n1 = Mathf.RoundToInt(value);
        matrix = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
    }
    public void SetMyValue2(float value)
    {
        n2 = Mathf.RoundToInt(value);;
        matrix = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
    }
    public void SetMyValue3(float value)
    {
        n3 = Mathf.RoundToInt(value);;
        matrix = GenerateTransistorMatrix();
        matrixVect = GenerateVectorMatrix();
    }
}
