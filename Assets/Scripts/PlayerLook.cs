using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float minViewDistance = 90f;
    
    float mouseSensitivity = 100;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mouseSensitivity = SettingsManager.mouseSense;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -minViewDistance, minViewDistance);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }

    public void EnableMouse(bool doEnable) 
    {
        if (doEnable) 
        { 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
