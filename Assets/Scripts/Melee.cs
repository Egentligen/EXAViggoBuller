using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Collider swordCollider;
    [Header("Effects")]
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] ParticleSystem hitEffect;
    [Header("Combo")]
    [SerializeField] Vector3[] swingAngles;
    [SerializeField] int[] swingDamage = { 10, 20, 30 };
    [SerializeField] AudioClip[] swingSounds;
    [SerializeField] float comboTime = 1.0f; //Time window to perform the next combo
    [Header("Swing")]
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float swingDuration = 0.1f;
    [Header("Hit")]
    [SerializeField] Vector3 hitboxSize = new Vector3(1, 1, 1);

    int currentCombo = 0;
    float lastSwingTime = 0;
    bool isSwinging = false;
    bool showHitbox = false;

    Sounds sounds;

    private void Awake()
    {
        sounds = FindObjectOfType<Sounds>();
    }

    private void Update()
    {
        PerformCombo();

        if (Input.GetKeyDown(KeyCode.H))
        {
            showHitbox = !showHitbox;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSwinging && other.CompareTag("Enemy"))
        {
            hitEffect.Play();

            //Apply damage to the enemy
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(swingDamage[currentCombo]);
            }
        }
    }

    private void PerformCombo()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            if (Time.time - lastSwingTime > comboTime)
            {
                currentCombo = 0; //Reset combo if time between swings is too long
            }

            SwingSword(swingAngles[currentCombo], swingDamage[currentCombo]);
            lastSwingTime = Time.time;

            sounds.PlaySound(swingSounds[currentCombo], transform.position);

            currentCombo = (currentCombo + 1) % swingAngles.Length; //Execute the current combo swing
        }
    }

    private void SwingSword(Vector3 angle, int damage)
    {
        Quaternion initialRotation = playerTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(playerTransform.eulerAngles + angle);

        StartCoroutine(SwingAnimation(initialRotation, targetRotation, damage));
    }

    private IEnumerator SwingAnimation(Quaternion initialRotation, Quaternion targetRotation, int damage)
    {
        float elapsedTime = 0;

        trailRenderer.emitting = true;
        isSwinging = true;

        swordCollider.enabled = true;

        while (elapsedTime < swingDuration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / swingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        //Return the sword to its initial position
        elapsedTime = 0;
        while (elapsedTime < swingDuration)
        {
            transform.rotation = Quaternion.Slerp(targetRotation, initialRotation, elapsedTime / swingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = initialRotation;

        swordCollider.enabled = false;

        trailRenderer.emitting = false;
        isSwinging = false;

        yield return new WaitForSeconds(attackCooldown);
    }

    public bool IsSwinging()
    {
        return isSwinging;
    }

    private void OnDrawGizmos()
    {
        if (showHitbox)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(swordCollider.bounds.center, hitboxSize);
        }
    }
}
