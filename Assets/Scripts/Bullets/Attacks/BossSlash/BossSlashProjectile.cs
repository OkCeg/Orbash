using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The projectile of the boss's slash attack
// The projectile is slow at the beginning and increases in speed/size gradually, and drastically increases its speed/size after midpoint
public class BossSlashProjectile : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private Vector2 initialSize;
    [SerializeField] private float midSize; // Size to aim for before speeding up more
    [SerializeField] private float maxSize; // Max size to aim for after reaching midSize
    [SerializeField] private float midGrowthTime;
    [SerializeField] private float maxGrowthTime;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float midSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float timeUntilMid;
    [SerializeField] private float speedGrowthRateToMid;
    [SerializeField] private float speedGrowthRateToMax;

    [SerializeField] private float lifeSpan;

    [SerializeField]private Rigidbody2D rb;
    [SerializeField] private Vector2 midNewSize;
    [SerializeField] private Vector2 maxNewSize;
    [SerializeField] private Vector2 smoothSizeVelocity = Vector2.zero; // For smooth damping
    [SerializeField] private bool toMax = false; // Is the projectile going to maxSpeed?

    [SerializeField] private float currentLifeSpan;
    [SerializeField] private float currentMidTimeLeft;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        transform.localScale = initialSize;
        midNewSize = new Vector2(2.5f * midSize, midSize);
        maxNewSize = new Vector2(2.5f * maxSize, maxSize);
    }

    private void FixedUpdate()
    {
        if (toMax)
        {
            // Increase size to max
            transform.localScale = Vector2.SmoothDamp(transform.localScale, maxNewSize, ref smoothSizeVelocity, maxGrowthTime);

            // Increase velocity to max
            // sqrt is expensive, so do squared comparison
            if (rb.velocity.sqrMagnitude < maxSpeed * maxSpeed)
            {
                rb.velocity *= speedGrowthRateToMax;
            }
        }
        else
        {
            currentMidTimeLeft -= Time.deltaTime;

            // Increase size to mid
            transform.localScale = Vector2.SmoothDamp(transform.localScale, midNewSize, ref smoothSizeVelocity, midGrowthTime);

            // Increase velocity to mid
            if (rb.velocity.sqrMagnitude < midSpeed * midSpeed)
            {
                rb.velocity *= speedGrowthRateToMid;
            }

            // Set velocity to mid and enable toMax
            if (currentMidTimeLeft <= 0)
            {
                rb.velocity = rb.velocity.normalized * midSpeed;
                toMax = true;
            }
        }

        currentLifeSpan -= Time.deltaTime;
        if (currentLifeSpan <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Called in ObjectPool
    public void Reset()
    {
        smoothSizeVelocity = Vector2.zero;
        rb.velocity = rb.velocity.normalized * initialSpeed;
        transform.localScale = initialSize;

        toMax = false;
        currentLifeSpan = lifeSpan;
        currentMidTimeLeft = timeUntilMid;
    }
}
