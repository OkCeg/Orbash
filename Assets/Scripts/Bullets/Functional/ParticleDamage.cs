using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    private ParticleSystem ps;
    private List<ParticleSystem.Particle> insideParticles;
    private List<ParticleSystem.Particle> enteredParticles;

    private static GameObject player;
    private static Collider2D playerCollider;
    private static Health playerHealth;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        insideParticles = new List<ParticleSystem.Particle>();
        enteredParticles = new List<ParticleSystem.Particle>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerCollider = player.GetComponent<Collider2D>();
            playerHealth = player.GetComponent<Health>();
        }
        ps.trigger.SetCollider(0, playerCollider);
    }

    private void OnParticleTrigger()
    {
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, insideParticles);
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enteredParticles);

        if (numInside > 0 || numEnter > 0)
        {
            playerHealth.LoseHealth();
        }
    }
}
