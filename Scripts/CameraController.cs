using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float zoomSpeed = 5.0f;
    public float minZoom = 1.0f;
    public float maxZoom = 50.0f;
    public float rotateSpeed = 50.0f;

    public float maxRotationX = 45f;
    public float minRotationX = -45f;

    private float currentRotationX;
    private float currentRotationY;

    private float currentZoom;
    private Vector3 mouseStartPosition;

    public float distance = 10f;
    public GameObject redBall;

    void Start()
    {
        currentZoom = transform.position.y;

    }

    void LateUpdate()
    {
        HandleInput();

        // Mettre à jour la position de la boule rouge
        if (redBall != null)
        {
            redBall.transform.position = transform.position + transform.forward * distance;
        }
    }

    public void ResetCameraRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void HandleInput()
    {
        // Movement (ZQSD instead of WASD)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * moveSpeed * Time.deltaTime;
        transform.position += transform.TransformDirection(movement);

        // Zoom in/out
        if (Input.GetKey(KeyCode.Space)) // Space bar
        {
            currentZoom += zoomSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) // Shift key
        {
            currentZoom -= zoomSpeed * Time.deltaTime;
        }
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        transform.position = new Vector3(transform.position.x, currentZoom, transform.position.z);

        // Look up, down, left, and right with the right mouse button
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            mouseStartPosition = Input.mousePosition;
            currentRotationX = transform.eulerAngles.x;
            currentRotationY = transform.eulerAngles.y;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseDelta = Input.mousePosition - mouseStartPosition;
            mouseStartPosition = Input.mousePosition;

            float rotateX = -mouseDelta.y * rotateSpeed * Time.deltaTime;
            float rotateY = mouseDelta.x * rotateSpeed * Time.deltaTime;

            currentRotationX += rotateX;
            currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);

            currentRotationY += rotateY;

            transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ResetCameraRotation();
        }
    }
}
