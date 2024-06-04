using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [Header("JetPack")]
    [SerializeField] KeyCode jetPackKey = KeyCode.E;
    [SerializeField] float jetpackForce = 10f;
    [SerializeField] float maxFuel = 100f;
    [SerializeField] float fuelConsumptionRate = 10f;
    [SerializeField] float fuelRegainRate = 5f;
    [SerializeField] Slider fuelBar;
    [SerializeField] TextMeshProUGUI instruction;
    [Header("GroundCheck")]
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] float groundCheckRadius = 0.4f;
    [SerializeField] LayerMask groundLayerMask;

    float currentFuel;
    Vector3 spawnPos;

    Rigidbody rb;
    Transform camTransform;
    Melee melee;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        melee = FindObjectOfType<Melee>();
    }

    private void Start()
    {
        camTransform = Camera.main.transform;
        currentFuel = maxFuel;
        spawnPos = transform.position;
        instruction.text = "Press " + jetPackKey + " to use jetpack";
    }

    private void Update()
    {
        if (transform.position.y < -5f) 
        {
            transform.position = spawnPos;
        }
    }

    private void FixedUpdate() 
    {
        Move();
        Jump();
        Fly();
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
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

        if (melee.IsSwinging()) 
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
    }

    private void Jump() 
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundLayerMask);
    }


    private void Fly()
    {
        fuelBar.value = Mathf.Lerp(fuelBar.value, currentFuel / maxFuel, Time.deltaTime * 5f);

        if (Input.GetKey(jetPackKey) && currentFuel > 0f)
        {
            rb.AddForce(Vector3.up * jetpackForce, ForceMode.Force);
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
        }
        else if (currentFuel < maxFuel)
        {
            currentFuel += fuelRegainRate * Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }
}
