using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    Vector3 cameraDirection;

    private void Update()
    {
        cameraDirection = Camera.main.transform.forward;
        cameraDirection.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }
}
