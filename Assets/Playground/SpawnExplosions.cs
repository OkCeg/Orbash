using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosions : MonoBehaviour
{
    // Set in inspector
    [SerializeField] GameObject explosion;
    [SerializeField] float timeInterval; //default 0.6
    [SerializeField] float spawnCount; //default 12

    private GameObject player;
    private Vector2 previouslyFired;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        previouslyFired = new Vector2(100, 100);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 coords = AwayFromPlayer();
            Instantiate(explosion, coords, Quaternion.identity);

            yield return new WaitForSeconds(timeInterval);
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
        return new Vector2(0, 0);
    }
}