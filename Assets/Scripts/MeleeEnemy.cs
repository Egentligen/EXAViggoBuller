using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] GameObject enemyToSummon;
    [Header("Movement")]
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float changeDirectionTime = 2.0f;
    [SerializeField] float movementRadius = 5.0f;
    [Header("Attack")]
    [SerializeField] float viewRange = 25f;
    [SerializeField] float attackTime = 0.5f;
    [SerializeField] float attackRange;
    [SerializeField] int damage = 10;

    Vector3 movementDirection;
    float timeSinceLastChange;
    float timeSinceHit;

    Rigidbody rb;
    PlayerHealth playerHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = FindObjectOfType<PlayerHealth>();
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
                DealDamage();
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
        //Move to player
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        //Face player
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void MoveEnemy()
    {
        rb.MovePosition(transform.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void DealDamage()
    {
        timeSinceHit += Time.deltaTime;

        if (timeSinceHit > attackTime)
        {
            timeSinceHit = 0;
            playerHealth.TakeDamage(damage);
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
