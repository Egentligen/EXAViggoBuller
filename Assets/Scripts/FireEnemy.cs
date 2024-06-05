using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] GameObject projectilePrefab;
    [Header("Movement")]
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float changeDirectionTime = 2.0f;
    [SerializeField] float movementRadius = 5.0f;
    [Header("Attack")]
    [SerializeField] float viewRange = 25f;
    [SerializeField] float attackTime = 0.5f;
    [SerializeField] float attackRange = 10f;
    [SerializeField] float projectileSpeed = 15f;

    Vector3 movementDirection;
    float timeSinceLastChange;
    float timeSinceAttack;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ChangeDirection();
    }

    private void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange > changeDirectionTime)
        {
            ChangeDirection();
            timeSinceLastChange = 0;
        }
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < viewRange)
        {
            Folow();

            if (distanceToPlayer < attackRange)
            {
                ShootProjectile();
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            MoveEnemy();
        }
    }

    private void ChangeDirection()
    {
        //Choose a random direction within the specified radius
        Vector3 randomDirection = Random.insideUnitSphere * movementRadius;
        randomDirection.y = 0; //Keep the movement on the horizontal plane
        movementDirection = randomDirection.normalized;
    }

    private void Folow() 
    {
        //Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        //Face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void MoveEnemy()
    {
        rb.MovePosition(transform.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void ShootProjectile()
    {
        timeSinceAttack += Time.deltaTime;

        if (timeSinceAttack > attackTime)
        {
            // Instantiate and shoot the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.velocity = (player.position - transform.position).normalized * projectileSpeed;

            timeSinceAttack = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, movementRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
