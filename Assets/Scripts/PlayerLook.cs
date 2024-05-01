using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float minViewDistance = 90f;
    [SerializeField] float mouseSensitivity = 100f;

    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -minViewDistance, minViewDistance);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}
