using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Pistol : Gun
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
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
}
