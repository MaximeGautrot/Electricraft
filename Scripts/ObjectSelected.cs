using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelected : MonoBehaviour
{
    
    public GameObject TorchOff;
    public GameObject TorchOn;
    public GameObject TorchCenter;
    public GameObject WireOn;
    public GameObject WireOff;
    public GameObject WireCenter;
    public GameObject ButtonOn;
    public GameObject ButtonOff;
    public GameObject LeverOn;
    public GameObject LeverOff;
    public GameObject LampOn;
    public GameObject LampOff;
    public GameObject CubeTOn;
    public GameObject CubeTOff;
    public GameObject redBall;

    private GameObject printElement;

    public CameraController cameraController;

    public string currentElement;

    public GameObject delaySlider;

    void Start()
    {
        printElement  = Instantiate(redBall, cameraController.GetPositionRedBall(), Quaternion.identity);;
        printElement.transform.SetParent(transform);
        currentElement = null;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        printElement.transform.position = cameraController.GetPositionRedBall();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateElement("");
        }
    }

    public void UpdateElement(string element)
    {
        //leaveButton.SetActive(false);
        Destroy(printElement);

        if (element == "Torch")
        {
            printElement = Instantiate(TorchCenter, cameraController.GetPositionRedBall(), Quaternion.identity);
            currentElement = "Torch";
        }
        else if (element == "Wire")
        {
            printElement = Instantiate(WireCenter, cameraController.GetPositionRedBall(), Quaternion.identity);
            currentElement = "Wire";
        }
        else if (element == "Button")
        {
            printElement = Instantiate(ButtonOff, cameraController.GetPositionRedBall(), Quaternion.identity);
            currentElement = "Button";
        }
        else if (element == "Lever")
        {
            printElement = Instantiate(LeverOff, cameraController.GetPositionRedBall(), Quaternion.identity);
            currentElement = "Lever";
        }
        else if (element == "Lamp")
        {
            printElement = Instantiate(LampOff, cameraController.GetPositionRedBall(), Quaternion.identity);
            currentElement = "Lamp";
        }
        else if (element == "CubeT")
        {
            printElement = Instantiate(CubeTOff, cameraController.GetPositionRedBall(), Quaternion.identity);
            currentElement = "CubeT";
        }
        else
        {
            printElement = Instantiate(redBall, cameraController.GetPositionRedBall(), Quaternion.identity);
            currentElement = null;
        }
        if (element == "Button" || element == "Relay")
        {
            delaySlider.gameObject.SetActive(true);
        }
        else
        {
            delaySlider.gameObject.SetActive(false);
        }

        printElement.transform.SetParent(transform);
    }

    public GameObject GetElement(string element)
    {
        if (element == "TorchOn")
        {
            return TorchOn;
        }
        if (element == "TorchOff")
        {
            return TorchOff;
        }
        if (element == "TorchCenter")
        {
            return TorchCenter;
        }
        if (element == "Torch")
        {
            return TorchCenter;
        }
        if (element == "WireOn")
        {
            return WireOn;
        }
        if (element == "WireOff")
        {
            return WireOff;
        }
        if (element == "WireCenter")
        {
            return WireCenter;
        }
        if (element == "Wire")
        {
            return WireCenter;
        }
        if (element == "ButtonOn")
        {
            return ButtonOn;
        }
        if (element == "ButtonOff")
        {
            return ButtonOff;
        }
        if (element == "Button")
        {
            return ButtonOff;
        }
        if (element == "LeverOn")
        {
            return LeverOn;
        }
        if (element == "LeverOff")
        {
            return LeverOff;
        }
        if (element == "Lever")
        {
            return LeverOff;
        }
        if (element == "LeverOn")
        {
            return LeverOn;
        }
        if (element == "LampOff")
        {
            return LampOff;
        }
        if (element == "Lamp")
        {
            return LampOff;
        }
        if (element == "LampOn")
        {
            return LampOn;
        }
        if (element == "CubeTOn")
        {
            return CubeTOn;
        }
        if (element == "CubeTOff")
        {
            return CubeTOff;
        }
        if (element == "CubeT")
        {
            return CubeTOff;
        }
        return null;
    }

    public string GetCurrentElement()
    {
        return currentElement;
    }
}
