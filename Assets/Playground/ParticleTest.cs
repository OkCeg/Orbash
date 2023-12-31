using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// With help from Maieron's videos:
// https://www.youtube.com/watch?v=ri3D6BmGSaM
// https://www.youtube.com/watch?v=46TqkhJu7uA&t=0s
// And Unity's particle documentation:
// https://docs.unity3d.com/ScriptReference/ParticleSystem.Emit.html
// And Luigi's Reddit post:
// https://www.reddit.com/r/Unity2D/comments/uy7m0h/picked_up_unity_about_a_month_ago_to_make_a/
// Testing how to emit particles
public class ParticleTest : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private Material particleMaterial;

    private ParticleSystem ps;

    // Use multiple ParticleSystems to create a circling effect (not shown here, see in Circler.cs)
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        GetComponent<ParticleSystemRenderer>().material = particleMaterial;

        // Start emitting after 1 seconds then emit every 2 seconds
        InvokeRepeating("EmitParticle", 1.0f, 2f);
    }

    private void EmitParticle()
    {
        var emitParams = new ParticleSystem.EmitParams();
        ps.Emit(emitParams, 1);
        ps.Play(); // Continue normal emissions
    }
}
