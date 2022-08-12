using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChasers : MonoBehaviour
{
    [SerializeField] GameObject chaser;
    [SerializeField] float timeInterval;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < 100; i++)
        {
            float randomX = Random.Range(-3.75f, 3.75f);
            float randomY = Random.Range(-5f, 5f);
            Instantiate(chaser, new Vector3(randomX, randomY, 0), Quaternion.identity);

            yield return new WaitForSeconds(timeInterval);
        }
    }
}
