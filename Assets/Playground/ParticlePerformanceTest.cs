using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test how much particle systems impact performance
public class ParticlePerformanceTest : MonoBehaviour
{
    public GameObject particleSys;
    public bool testParticle;

    private WaitForSeconds yieldInterval = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        if (testParticle)
        {
            StartCoroutine(ParticleTest());
        }
        else
        {
            StartCoroutine(GameObjectTest());
        }
    }

    private IEnumerator ParticleTest()
    {
        for (int i = 0; i < 100; i++)
        {
            Instantiate(particleSys);
            yield return yieldInterval;
        }
    }

    private IEnumerator GameObjectTest()
    {
        // Turned down to prevent lag
        for (int i = 0; i < 10; i++)
        {
            ObjectPool.SharedInstance.CreateRigidBodyExplosion(Vector2.zero, 8, 4, 32, 0);
            yield return yieldInterval;
        }
    }
}
