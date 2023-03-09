using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Player attack with a projectile
public class Slash : MonoBehaviour
{
    private Dash dash; // determine direction of attack
    private int direction = 0; // use Dash direction value for calculations
    private GameObject enemy; // for projectile homing

    [SerializeField] private GameObject slash;
    [SerializeField] private GameObject projectile;

    public float homingAngle = 15f; // half of full homing leniency (left and right homing available)

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        dash = GetComponent<Dash>();
    }

    // ToDo: When animating slash, make sure it can go in both directions. Also make sure to change projectile information when adding slash.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            direction = dash.direction;
            SpawnSlashAndProjectile();
        }
    }

    public void SpawnSlashAndProjectile()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 45 * direction);

        if (enemy != null)
        {
            // homing projectile angle check
            Vector3 angleFromEnemy = enemy.transform.position - transform.position;

            float zRotation = Mathf.Atan2(angleFromEnemy.y, angleFromEnemy.x) * Mathf.Rad2Deg;
            // atan2 range is (-pi, pi) so Quadrants I and II are valid without change
            if (angleFromEnemy.x < 0 && angleFromEnemy.y < 0 || angleFromEnemy.x > 0 && angleFromEnemy.y < 0) // check Quadrants III and IV
            {
                zRotation += 360;
            }

            // home if within homing angle
            if (zRotation < 45 * direction + homingAngle && zRotation > 45 * direction - homingAngle)
            {
                rotation = Quaternion.Euler(0, 0, zRotation);
            }
        }

        Vector3 spawnLoc = transform.position + rotation * new Vector3(1, 0, 0);

        ObjectPool.SharedInstance.CreateSlash(spawnLoc, rotation);
        ObjectPool.SharedInstance.CreateProjectile(spawnLoc, rotation);
    }
}
