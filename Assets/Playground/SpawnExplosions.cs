using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosions : MonoBehaviour
{
    // Set in inspector
    [SerializeField] GameObject explosion;
    [SerializeField] private float explosionInitialSpeed = 8;
    [SerializeField] private float explosionAfterSpeed = 4;
    [SerializeField] private float timeInterval = 0.6f;
    [SerializeField] private int spawnCount = 12;
    [SerializeField] private int bulletNum = 32;

    private GameObject player;
    private Vector2 previouslyFired;
    private WaitForSeconds yieldInterval;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        previouslyFired = new Vector2(100, 100);
        yieldInterval = new WaitForSeconds(timeInterval);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 coords = AwayFromPlayer();

            GameObject explode = ObjectPool.SharedInstance.CreateExplosion(coords, explosionInitialSpeed, explosionAfterSpeed, bulletNum, 0);

            yield return yieldInterval;
        }
    }

    // return coords of valid vector 3 units away from the player & 1 unit away from previously fired chaser
    private Vector2 AwayFromPlayer()
    {
        Vector2 coords = new Vector2(Random.Range(-3.75f, 3.75f), Random.Range(-5f, 5f));
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        for (int i = 0; i < 50; i++)
        {
            if ((coords - playerPos).magnitude > 3 && (coords - previouslyFired).magnitude > 1)
            {
                previouslyFired = coords;
                return coords;
            }

            coords = new Vector2(Random.Range(-3.75f, 3.75f), Random.Range(-5f, 5f));
        }
        print("Too long! Edit the coordinates.");
        return coords;
    }
}
