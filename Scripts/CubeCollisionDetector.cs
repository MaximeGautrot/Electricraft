using UnityEngine;

public class CubeCollisionDetector : MonoBehaviour
{
    private CubeFiller cubeFiller;
    private GameObject cubeObject; // Référence au cube
    private Material originalMaterial; // Matériau original du cube

    public void Initialize(CubeFiller filler, GameObject cube)
    {
        cubeFiller = filler;
        cubeObject = cube;

        // Enregistre le matériau original du cube
        originalMaterial = cube.GetComponent<Renderer>().material;
    }

    private void Start()
    {
        CreateEdges();
    }

    private void CreateEdges()
    {
        // Crée quatre arêtes autour du cube
        CreateEdge(Vector3.forward);
        CreateEdge(Vector3.back);
        CreateEdge(Vector3.left);
        CreateEdge(Vector3.right);
    }

    private void CreateEdge(Vector3 direction)
    {
        GameObject edge = new GameObject("Edge");
        edge.transform.parent = transform;
        edge.tag = "Edge";

        edge.transform.localPosition = direction * 0.5f;
        edge.AddComponent<BoxCollider>();
        edge.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            // Si la collision se produit avec une arête, change la couleur des arêtes du cube
            ChangeEdgeColor(true);
        }
        else if (other.CompareTag("PointSphere"))
        {
            // Si la collision se produit avec la sphère du point de la caméra
            // Ajoutez ici le comportement que vous souhaitez, par exemple, changer la couleur du cube
            cubeObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            // Si la caméra quitte le cube, rétablit la couleur des arêtes du cube
            ChangeEdgeColor(false);
        }
        else if (other.CompareTag("PointSphere"))
        {
            // Ajoutez ici le comportement que vous souhaitez lorsque la sphère quitte le cube
            // par exemple, rétablir la couleur originale du cube
            cubeObject.GetComponent<Renderer>().material.color = originalMaterial.color;
        }
    }

    private void ChangeEdgeColor(bool highlight)
    {
        // Récupère tous les enfants avec le tag "Edge"
        Transform[] edges = cubeObject.GetComponentsInChildren<Transform>(true);

        foreach (Transform edge in edges)
        {
            if (edge.CompareTag("Edge"))
            {
                Renderer edgeRenderer = edge.GetComponent<Renderer>();

                // Change la couleur des arêtes en bleu turquoise ou rétablit la couleur originale
                edgeRenderer.material.color = highlight ? new Color(0.25f, 1f, 0.85f) : originalMaterial.color;
            }
        }
    }
}

