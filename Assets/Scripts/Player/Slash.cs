using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Player attack
public class Slash : MonoBehaviour
{
    private GameObject player;
    private Transform pos;
    private int direction = 0; // determine direction of attack

    public float projectileSpeed;
    public GameObject slash;
    public GameObject projectile;

    private void Start()
    {
        player = gameObject;
        pos = player.transform;
    }

    // ToDo: When animating slash, make sure it can go in both directions. Also make sure to change projectile information when adding slash.
    private void Update()
    {
        SlashDirection();
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnSlashAndProjectile();
        }
    }

    private void SpawnSlashAndProjectile()
    {
        Vector3 spawnLoc = pos.position + Quaternion.Euler(0, 0, 45 * direction) * new Vector3(1, 0, 0);

        Instantiate(slash, spawnLoc, Quaternion.Euler(0, 0, 45 * direction));

        GameObject projectileObject = Instantiate(projectile, spawnLoc, Quaternion.Euler(0, 0, 45 * direction));
        projectileObject.GetComponent<MoveBullet>().speed = projectileSpeed;
    }

    // Might be able to save code length and performance speed by sharing with Movement.cs
    private void SlashDirection()
    {
        // direction 0 starts from right and goes counterclockwise
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            direction = 0;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            direction = 1;
        }
        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            direction = 2;
        }
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            direction = 3;
        }
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            direction = 4;
        }
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            direction = 5;
        }
        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            direction = 6;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            direction = 7;
        }
    }
}
