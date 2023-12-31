using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // Set in inspector;
    [SerializeField] private float health;
    [SerializeField] private Slider healthBarSlider;

    private float maxHealth;

    private void Start()
    {
        maxHealth = health;
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSlash"))
        {
            health -= 3f;
            UpdateHealth();
        }
        else if (collision.CompareTag("PlayerProjectile"))
        {
            health -= collision.gameObject.GetComponent<ProjectileDamage>().damage;
            UpdateHealth();
            // Debug.Log(health);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealth()
    {
        healthBarSlider.value = health / maxHealth;
    }
}
