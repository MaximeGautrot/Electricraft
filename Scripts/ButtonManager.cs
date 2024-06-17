using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScrollView : MonoBehaviour
{
    public GameObject scrollView; // Assign this in the Inspector
    private bool isVisible = false;

    void Start()
    {
        scrollView.SetActive(isVisible);
    }
    public void ToggleVisibility()
    {
        isVisible = !isVisible;
        scrollView.SetActive(isVisible);
    }
}