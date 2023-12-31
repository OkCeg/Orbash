using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Circler : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private int bulletSpreadCount;
    [SerializeField] private float timeUntilStart;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSize;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float rotationSpeed;

    private Material defaultSpriteMaterial;
    private ParticleSystem[] childSystems;
    private int startingColorIndex;

    void Start()
    {
        defaultSpriteMaterial = (Material) Resources.Load("Resources-SpriteDefault");
        childSystems = new ParticleSystem[bulletSpreadCount];
        startingColorIndex = Random.Range(0, 7);

        float initialRotation = Random.Range(0, 360f);
        for (int i = 0; i < bulletSpreadCount; i++)
        {
            GameObject go = new GameObject("Particle System");
            go.transform.Rotate(initialRotation + i * 360f / bulletSpreadCount, 90, 0);
            go.transform.parent = gameObject.transform;
            go.transform.position = gameObject.transform.position;

            ParticleSystem ps = go.AddComponent<ParticleSystem>();
            go.AddComponent<ParticleDamage>();
            childSystems[i] = ps;

            ParticleSystem.MainModule module = ps.main;
            module.startSize = bulletSize;
            module.startSpeed = bulletSpeed;
            module.startLifetime = 20f;
            module.maxParticles = 10000;
            module.simulationSpace = ParticleSystemSimulationSpace.World;

            ParticleSystemRenderer renderer = go.GetComponent<ParticleSystemRenderer>();
            // renderer.material = ParticleColorRandomizer.Instance.colorMaterials[(colorIndex + i) % 7];
            renderer.material = defaultSpriteMaterial;
            renderer.sortingOrder = -1;

            // Disable automatic emission
            ParticleSystem.EmissionModule emission = ps.emission;
            emission.enabled = false;

            // Sprite shape makes the particles go in a line
            ParticleSystem.ShapeModule shape = ps.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Sprite;

            // Change the color
            ParticleSystem.TextureSheetAnimationModule texture = ps.textureSheetAnimation;
            texture.enabled = true;
            texture.mode = ParticleSystemAnimationMode.Sprites;
            texture.AddSprite(ParticleColorRandomizer.Instance.colorSprites[(startingColorIndex + i) % 7]);
            texture.cycleCount = 0;
            texture.startFrame = 0;

            // For collision detection
            ParticleSystem.TriggerModule trigger = ps.trigger;
            trigger.enabled = true;
            trigger.enter = ParticleSystemOverlapAction.Callback;
            trigger.inside = ParticleSystemOverlapAction.Callback;

            // For particle destruction
            ParticleSystem.CollisionModule collision = ps.collision;
            collision.enabled = true;
            collision.type = ParticleSystemCollisionType.World;
            collision.mode = ParticleSystemCollisionMode.Collision2D;
            collision.lifetimeLoss = 1; // Remove 100% of particle lifetime upon wall collision
            collision.collidesWith = 128; // Layers are binary, so 128 represents layer 7 (the walls)
        }

        StartCoroutine("Attack");
    }

    private IEnumerator Attack()
    {
        // Initial wait
        yield return new WaitForSeconds(timeUntilStart);

        // Cache the fire rate
        WaitForSeconds timeUntilNextFire = new WaitForSeconds(fireRate);

        // Cache emitParams
        // Particles declared with EmitParams will override the properties set in the particle system
        // But otherwise uses the original properties
        var emitParams = new ParticleSystem.EmitParams();

        for (int i = 0; i < 100; i++)
        {
            // Spin the gameObject to simulate spinning motion of particles
            // Note that this is technically the same as "transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed)"
            transform.Rotate(new Vector3(0, 0, rotationSpeed));

            for (int j = 0; j < transform.childCount; j++)
            {
                ParticleSystem system = childSystems[j];
                system.Emit(emitParams, 1);
                system.Play();
            }

            yield return timeUntilNextFire;
        }
    }
}
