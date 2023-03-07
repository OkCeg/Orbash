using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSlash"))
        {
            health -= 10f;
        }
        else if (collision.CompareTag("PlayerProjectile"))
        {
            health -= 5f;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
