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
            StartCoroutine(Many());
        }
        else
        {
            StartCoroutine(Many2());
        }
    }

    private IEnumerator Many()
    {
        for (int i = 0; i < 100; i++)
        {
            Instantiate(particleSys);
            yield return yieldInterval;
        }
    }

    private IEnumerator Many2()
    {
        for (int i = 0; i < 100; i++)
        {
            ObjectPool.SharedInstance.CreateExplosion(Vector2.zero, 8, 4, 32, 0);
            yield return yieldInterval;
        }
    }
}
