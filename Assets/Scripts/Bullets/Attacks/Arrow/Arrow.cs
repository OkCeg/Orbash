using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The boss's arrow attack
public class Arrow : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float trailDelay;
    [SerializeField] private float trailFireRate;
    [SerializeField] private float timeUntilDestroy;
    public float timeUntilFire; // Set in ObjectPool

    private Rigidbody2D rb;
    private GameObject player;
    private float timeLeftUntilFire;
    private float timeLeftUntilDestroy;
    private bool facingFinished;
    private Vector3 face; // The vector from the game object to the player

    // The particle system for the arrow trails
    private ArrowParticles particle1; // Right side
    private ArrowParticles particle2; // Left side

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Initialize the trail particles
        particle1 = transform.GetChild(0).GetComponent<ArrowParticles>();
        particle1.fireRate = trailFireRate;
        particle2 = transform.GetChild(1).GetComponent<ArrowParticles>();
        particle2.fireRate = trailFireRate;
    }

    private void FixedUpdate()
    {
        if (!facingFinished)
        {
            if (timeLeftUntilFire >= 0)
            {
                face = player.transform.position - transform.position;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(face.y, face.x) * Mathf.Rad2Deg);
                timeLeftUntilFire -= Time.deltaTime;
            }
            else
            {
                rb.velocity = face.normalized * arrowSpeed;
                facingFinished = true;
            }
        }

        timeLeftUntilDestroy -= Time.deltaTime;
        if (timeLeftUntilDestroy <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Called in ObjectPool
    public void Reset()
    {
        timeLeftUntilFire = timeUntilFire;
        timeLeftUntilDestroy = timeUntilDestroy + timeUntilFire;
        facingFinished = false;

        // Set here for when timeUntilFire changes
        particle1.timeUntilStart = timeUntilFire + trailDelay;
        particle2.timeUntilStart = timeUntilFire + trailDelay;

        // Start the particle trails
        particle1.StartCoroutine("FireParticlesRight");
        particle2.StartCoroutine("FireParticlesLeft");
    }
}
