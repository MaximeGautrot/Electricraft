using UnityEngine;

public class GetSize : MonoBehaviour
{
    void Start()
    {
        Debug.Log("ObjectSize script started");

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        Renderer renderer = GetComponent<Renderer>();

        if (meshRenderer != null)
        {
            Debug.Log("MeshRenderer found");

            Bounds bounds = meshRenderer.bounds;
            Debug.Log("Bounds: " + bounds.min + " to " + bounds.max);

            Vector3 size = bounds.size;
            Debug.Log("Object size: " + size.x + " x " + size.y + " x " + size.z);
        }
        else if (skinnedMeshRenderer != null)
        {
            Debug.Log("SkinnedMeshRenderer found");

            Bounds bounds = skinnedMeshRenderer.bounds;
            Debug.Log("Bounds: " + bounds.min + " to " + bounds.max);

            Vector3 size = bounds.size;
            Debug.Log("Object size: " + size.x + " x " + size.y + " x " + size.z);
        }
        else if (renderer != null)
        {
            Debug.Log("Renderer found");

            Bounds bounds = renderer.bounds;
            Debug.Log("Bounds: " + bounds.min + " to " + bounds.max);

            Vector3 size = bounds.size;
            Debug.Log("Object size: " + size.x + " x " + size.y + " x " + size.z);
        }
        else
        {
            Debug.LogWarning("MeshRenderer, SkinnedMeshRenderer, and Renderer not found");
        }
    }

}

