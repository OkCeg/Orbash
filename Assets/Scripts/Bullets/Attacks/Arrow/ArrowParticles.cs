using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The particles trails generated from the boss's arrow attack
public class ArrowParticles : MonoBehaviour
{
    // Will be set when a DampParticle game object is created
    public float timeUntilStart;
    public float fireRate;

    private Quaternion initialRotation;
    private ParticleSystem ps;

    // Coroutines are called in Arrow.cs
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Create the particles corresponding to the left side of the arrow
    private IEnumerator FireParticlesLeft()
    {
        // Initial wait
        yield return new WaitForSeconds(timeUntilStart);

        initialRotation = transform.rotation;
        // Cache the fire rate
        WaitForSeconds timeUntilNextFire = new WaitForSeconds(fireRate);
        for (int i = 0; i < 100; i++)
        {
            // For some variance in shooting direction (the main game object will rotate based on the arrow object)
            transform.rotation = initialRotation * Quaternion.Euler(Random.Range(-20f, 10f), 0, 0);

            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startLifetime = ps.main.startLifetime.constant + Random.Range(-0.3f, 0.7f);
            ps.Emit(emitParams, 1);
            ps.Play();

            yield return timeUntilNextFire;
        }
    }

    // Create the particles corresponding to the right side of the arrow
    private IEnumerator FireParticlesRight()
    {
        // Initial wait
        yield return new WaitForSeconds(timeUntilStart);

        initialRotation = transform.rotation;
        // Cache the fire rate
        WaitForSeconds timeUntilNextFire = new WaitForSeconds(fireRate);
        for (int i = 0; i < 100; i++)
        {
            // For some variance in shooting direction (the main game object will rotate based on the arrow object)
            transform.rotation = initialRotation * Quaternion.Euler(Random.Range(-10f, 20f), 0, 0);

            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startLifetime = ps.main.startLifetime.constant + Random.Range(-0.3f, 0.7f);
            ps.Emit(emitParams, 1);
            ps.Play();

            yield return timeUntilNextFire;
        }
    }
}
