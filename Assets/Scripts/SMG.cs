using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Gun
{
    protected override void Fire()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range, shootableLayer))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            impactObj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            Destroy(impactObj, 2f);
        }
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
}
