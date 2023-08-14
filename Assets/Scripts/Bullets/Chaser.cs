using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : ColorRandomizer
{
    // Set in inspector
    [SerializeField] private GameObject moveBullet;
    [SerializeField] private float speed = 48f; // speed of bullets
    [SerializeField] private float timeInterval = 0.07f; // time between bullet shots
    [SerializeField] private int fireCount = 3; // how many times bullet is fired
    [SerializeField] private float spread = 20f; // spread angle when bulletCount is at least 2
    [SerializeField] private int bulletCount = 1; // should be odd to ensure symmetry

    private float rotation;
    private Transform target;
    private WaitForSeconds yieldInterval;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        yieldInterval = new WaitForSeconds(timeInterval);
    }

    // Shoot towards player
    public IEnumerator Shoot()
    {
        for (int i = 0; i < fireCount; i++)
        {
            Vector3 face = target.position - transform.position;
            for (int j = 0; j < bulletCount; j++)
            {
                rotation = Mathf.Atan2(face.y, face.x) * Mathf.Rad2Deg + spread * (j - bulletCount / 2);
                int randomColorIndex = Random.Range(0, colors.Length);
                ObjectPool.SharedInstance.CreateMoveBullet(transform.position, Quaternion.Euler(0, 0, rotation), speed, colors[randomColorIndex], 1);
            }

            yield return yieldInterval;
        }

        gameObject.SetActive(false);
    }
}
