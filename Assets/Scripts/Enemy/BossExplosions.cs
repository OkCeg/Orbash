using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet explosions in top center of screen
public class BossExplosions : MonoBehaviour
{
    // Set in inspector
    [SerializeField] GameObject explosion;
    [SerializeField] private Vector2 startingCoords;

    // Boss firing bullets
    [SerializeField] private int explosionCount = 25; // how many explosions will occur at boss
    [SerializeField] private float bossTimeInterval = 0.6f; // how often the boss will fire explosion
    [SerializeField] private float initialSpeed = 12f;
    [SerializeField] private float afterSpeed = 6f;
    [SerializeField] private int bulletNum = 32;
    [SerializeField] private float explosionAngle = 25f; // angle change between explosions at boss
    [SerializeField] private float timeReductionScale = 0.95f; // decrease time interval

    private Vector3 velocity = Vector3.zero;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, startingCoords, ref velocity, 0.2f);

        if (((Vector2) transform.position - startingCoords).sqrMagnitude < 0.1)
        {
            StartCoroutine(SpawnAtBoss());
            enabled = false; // disable script
        }
    }

    private IEnumerator SpawnAtBoss()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            GameObject explode = ObjectPool.SharedInstance.CreateExplosion(transform.position, initialSpeed, afterSpeed, bulletNum, explosionAngle * i);

            yield return new WaitForSecondsRealtime(bossTimeInterval); // fixed interval even when there is some lag

            bossTimeInterval *= timeReductionScale;
        }
    }
}
