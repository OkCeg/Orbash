using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaser : MonoBehaviour
{
    [SerializeField] private GameObject chaser;
    [SerializeField] private int chaserSpawnCount = 20;
    [SerializeField] private float timeInterval = 0.4f; // time between chaser fires
    [SerializeField] private float smoothTime = 0.3f; // time it takes to move to next firing point
    [SerializeField] private float pauseTime = 0.1f; // wait until fire

    private GameObject player;
    private Vector2 previouslyFired = new Vector2(100, 100);
    private Vector3 velocity = Vector3.zero;
    private Vector2 coords = Vector2.zero; // position to move to
    private bool moveToCoords = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ChaserSpawn());
    }

    private void Update()
    {
        if (moveToCoords)
        {
            // ref is necessary to pass by reference (method updates the value of velocity every time the method is ran)
            transform.position = Vector3.SmoothDamp(transform.position, coords, ref velocity, smoothTime);
        }
    }

    private IEnumerator ChaserSpawn()
    {
        for (int i = 0; i < chaserSpawnCount; i++)
        {
            moveToCoords = true;
            coords = AwayFromPlayer();

            // Wait until enemy moves to coords
            yield return new WaitForSeconds(smoothTime);
            moveToCoords = false;

            // Wait to fire
            yield return new WaitForSeconds(pauseTime);

            // Create chasers
            Instantiate(chaser, transform.position, Quaternion.identity);

            // Time interval until next chaser spawn
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