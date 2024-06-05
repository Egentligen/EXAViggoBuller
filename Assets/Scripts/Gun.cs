using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.5f;
    public float range = 100f;
    public int damage = 10;
    public LayerMask shootableLayer;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioClip fireSound;

    protected float nextTimeToFire = 0f;
    protected Camera mainCam;

    Sounds sounds;

    protected virtual void Awake()
    {
        sounds = FindObjectOfType<Sounds>();

        mainCam = Camera.main;
    }

    protected void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();

            sounds.PlaySound(fireSound, transform.position);
        }
    }

    protected abstract void Fire();
}
