using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Gun
{
    [SerializeField] GameObject rocket;
    [SerializeField] float speed;

    protected override void Fire()
    {
        muzzleFlash.Play();

        GameObject projectile = Instantiate(rocket, transform.position + transform.forward, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = mainCam.transform.forward * speed;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
}
