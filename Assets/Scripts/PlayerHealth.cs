using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject loseCanvas;
    [SerializeField] float health = 100f;
    [SerializeField] Slider healthBar;

    float maxHealth;

    PlayerLook look;

    private void Awake()
    {
        look = FindObjectOfType<PlayerLook>();
    }

    private void Start()
    {
        maxHealth = health;

        loseCanvas.SetActive(false);
    }

    private void Update()
    {
        healthBar.value = Mathf.Lerp(healthBar.value, health / maxHealth, Time.deltaTime * 5f);

        if (transform.position.y < -5f)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        loseCanvas.SetActive(true);
        look.EnableMouse(true);
        Time.timeScale = 0f;
    }
}
