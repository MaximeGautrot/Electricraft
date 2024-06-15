using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leaveButton; // Assign this in the Inspector
    private bool isVisible = false;

    void Start()
    {
        leaveButton.SetActive(isVisible);
    }
    public void LeaveButtonVisibility()
    {
        leaveButton.SetActive(false);
    }
}
