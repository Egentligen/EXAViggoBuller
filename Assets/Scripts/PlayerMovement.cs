using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isJumping = false;

    Rigidbody playerRigidbody;
    Transform camTransform;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Get movement direction relative to camera
        Vector3 cameraForward = camTransform.forward;
        Vector3 cameraRight = camTransform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        //Calculate movement direction based on input and camera orientation
        Vector3 moveDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;

        playerRigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, playerRigidbody.velocity.y, moveDirection.z * moveSpeed);

        //Sprint
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
    }

    private void Jump() 
    {
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            playerRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }
    }
}
