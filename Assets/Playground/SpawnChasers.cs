using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChasers : MonoBehaviour
{
    // Set in inspector
    [SerializeField] GameObject chaser;
    [SerializeField] GameObject temporaryBaseBullet; // signal that a chaser will spawn soon
    [SerializeField] float timeInterval; //default 0.8
    [SerializeField] int spawnCount; //default 10

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

            // reaction time
            GameObject temp = Instantiate(temporaryBaseBullet, coords, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            Destroy(temp);

            // spawn chaser
            Instantiate(chaser, coords, Quaternion.identity);

            yield return new WaitForSeconds(timeInterval);
        }
    }

    // return coords of valid vector 16 units away from the player & 4 unit away from previously fired chaser
    private Vector2 AwayFromPlayer()
    {
        Vector2 coords = new Vector2(Random.Range(-15f, 15f), Random.Range(-20f, 20f));
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        for (int i = 0; i < 1000; i++)
        {
            if ((coords - playerPos).magnitude > 16 && (coords - previouslyFired).magnitude > 4)
            {
                previouslyFired = coords;
                return coords;
            }

            coords = new Vector2(Random.Range(-15f, 15f), Random.Range(-20f, 20f));
        }
        print("Too long! Edit the coordinates.");
        return new Vector2(0, 0);
    }
}
