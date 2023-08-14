using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The object that the camera is parented to
    public GameObject cameraBox;

    private float m_FieldOfView;

    // The different speed multipliers of the camera
    private float m_MovementSpeed;
    private float m_AdditionalSprintSpeed;
    private int m_RotationSpeed;

    // Vector variables to store the change in position and rotation
    private Vector3 m_Movement;
    private Vector2 m_Turn;

    // A variable to store the rotation that the camera starts at in the x axis
    private float defaultCameraRotation;

    // A variable to store the limit of how far the camera can move
    Vector3 m_CameraBoundary;

    // Start is called before the first frame update
    void Start()
    {
        m_FieldOfView = Camera.main.fieldOfView;
        m_MovementSpeed = 10.92f;
        m_AdditionalSprintSpeed = 10.68f;
        m_RotationSpeed = 3;
        defaultCameraRotation = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the WASD input keys
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Increase the FOV by 1 and add additional movement speed when left shift is held down
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            Camera.main.fieldOfView = m_FieldOfView + 1;
            cameraBox.transform.Translate(Vector3.forward * m_AdditionalSprintSpeed * Time.deltaTime);
        }

        // Change the FOV back to normal
        else
        {
            Camera.main.fieldOfView = m_FieldOfView;
        }

        // Move up while Space is held down
        if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl))
        {
            // Set the values for m_Movement to use horizontalInput and verticalInput
            m_Movement.Set(horizontalInput, 1f, verticalInput);

            // Make the movement of the camera consistent
            m_Movement.Normalize();

            // Move forwards, backwards, left and right
            cameraBox.transform.Translate(m_Movement * m_MovementSpeed * Time.deltaTime);
        }

        // Move down while LeftControl is held down
        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.Space))
        {
            m_Movement.Set(horizontalInput, -1f, verticalInput);
            m_Movement.Normalize();
            cameraBox.transform.Translate(m_Movement * m_MovementSpeed * Time.deltaTime);
        }

        else
        {
            m_Movement.Set(horizontalInput, 0f, verticalInput);
            m_Movement.Normalize();
            cameraBox.transform.Translate(m_Movement * m_MovementSpeed * Time.deltaTime);
        }

        // Set boundaries to limit where the camera can move
        m_CameraBoundary = cameraBox.transform.position;
        m_CameraBoundary.x = Mathf.Clamp(m_CameraBoundary.x, -128f, 128f);
        m_CameraBoundary.y = Mathf.Clamp(m_CameraBoundary.y, 22.5f, 75);
        m_CameraBoundary.z = Mathf.Clamp(m_CameraBoundary.z, -128f, 128f);
        cameraBox.transform.position = m_CameraBoundary;

        // Allow the camera to be rotated while the right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            // Make the cursor invisible and lock it to the middle of the screen
            Cursor.lockState = CursorLockMode.Locked;

            // Get the mouse movements
            m_Turn.x += Input.GetAxis("Mouse X") * m_RotationSpeed;
            m_Turn.y += Input.GetAxis("Mouse Y") * m_RotationSpeed;

            // Stop the camera from being able to go upside down
            m_Turn.y = Mathf.Clamp(m_Turn.y, -90 + defaultCameraRotation, 90 + defaultCameraRotation);

            // Make the camera rotate up and down
            transform.localRotation = Quaternion.Euler(-m_Turn.y + defaultCameraRotation, 0, 0);

            // Make the camera rotate left and right
            cameraBox.transform.rotation = Quaternion.Euler(0, m_Turn.x, 0);
        }

        else
        {
            // Make the cursor visible again
            Cursor.lockState = CursorLockMode.None;
        }
    }
}