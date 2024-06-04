using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject popupText;
    [SerializeField] float health = 100f;
    [SerializeField] bool isDummy;

    public void TakeDamage(float damage)
    {
        DamagePopUp(damage);

        if (!isDummy) 
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void DamagePopUp(float damage) 
    { 
        var popup = Instantiate(popupText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        popup.GetComponent<TextMesh>().text = damage.ToString();
    }
}
