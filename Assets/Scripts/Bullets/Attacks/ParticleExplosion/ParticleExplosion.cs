using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The boss's particle explosion attack projectiles
public class ParticleExplosion : MonoBehaviour
{
    // Set in inspector (for default) and ObjectPool
    public int bulletNum;
    public float initialAngle;

    private ParticleSystem ps; // Particle system component
    private ParticleSystem.EmitParams emitParams; // Responsible for creating new bullets
    private Vector3 rotation; // Rotation per bullet

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        emitParams = new ParticleSystem.EmitParams();
    }

    private void RandomizeColor()
    {
        ParticleSystem.TextureSheetAnimationModule texture = ps.textureSheetAnimation;
        texture.AddSprite(ParticleColorRandomizer.Instance.colorSprites[Random.Range(0, 7)]);
    }

    private void Explode()
    {
        for (int i = 0; i < bulletNum; i++)
        {
            transform.Rotate(rotation);
            ps.Emit(emitParams, 1);
        }

        ps.Play();
    }

    public void Reset()
    {
        transform.rotation = Quaternion.Euler(initialAngle, 90, 0);
        rotation = new Vector3(360f / bulletNum, 0, 0);

        RandomizeColor();
        Explode();
    }
}
