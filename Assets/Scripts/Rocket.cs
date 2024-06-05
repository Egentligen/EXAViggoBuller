using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionForce = 700f;
    [SerializeField] int damage = 50;
    [SerializeField] GameObject explosionObj;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        //Show explosion effect
        GameObject impactObj = Instantiate(explosionObj, transform.position, Quaternion.identity);
        impactObj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        Destroy(impactObj, 2f);

        //Find all nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            //Apply damage to enemies
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
