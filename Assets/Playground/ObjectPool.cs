using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPool : MonoBehaviour
{
    // Access pool using ObjectPool.SharedInstance anywhere
    public static ObjectPool SharedInstance;

    // Player slash
    public int slashNum;
    public GameObject slash;
    public List<GameObject> slashPool;

    // Player slash projectile
    public int projectileNum;
    public GameObject projectile;
    public List<GameObject> projectilePool;

    // Move bullet prefab
    public int moveBulletNum;
    public GameObject moveBullet;
    public List<GameObject> moveBulletPool;
    private Vector3 originalScale;

    // Chaser prefab
    public int chaserNum;
    public GameObject chaser;
    public List<GameObject> chaserPool;

    // Explosion prefab
    public int explosionNum;
    public GameObject explosion;
    public List<GameObject> explosionPool;

    private void Awake()
    {
        // Access pool using ObjectPool.SharedInstance anywhere
        SharedInstance = this;
        GameObject tmp;

        slashPool = new List<GameObject>();
        for (int i = 0; i < slashNum; i++)
        {
            tmp = Instantiate(slash, transform);
            tmp.SetActive(false);
            slashPool.Add(tmp);
        }

        projectilePool = new List<GameObject>();
        for (int i = 0; i < projectileNum; i++)
        {
            tmp = Instantiate(projectile, transform);
            tmp.SetActive(false);
            projectilePool.Add(tmp);
        }

        moveBulletPool = new List<GameObject>();
        for (int i = 0; i < moveBulletNum; i++)
        {
            tmp = Instantiate(moveBullet, transform);
            tmp.SetActive(false);
            moveBulletPool.Add(tmp);
        }
        originalScale = moveBullet.transform.localScale;

        chaserPool = new List<GameObject>();
        for (int i = 0; i < chaserNum; i++)
        {
            tmp = Instantiate(chaser, transform);
            tmp.SetActive(false);
            chaserPool.Add(tmp);
        }

        explosionPool = new List<GameObject>();
        for (int i = 0; i < explosionNum; i++)
        {
            tmp = Instantiate(explosion, transform);
            tmp.SetActive(false);
            explosionPool.Add(tmp);
        }
    }

    // Locate and rotate slash
    public GameObject CreateSlash(Vector2 spawnLoc, Quaternion rotation)
    {
        for (int i = 0; i < slashNum; i++)
        {
            GameObject slashObj = slashPool[i];
            if (!slashObj.activeInHierarchy)
            {
                slashObj.transform.position = spawnLoc;
                slashObj.transform.rotation = rotation;
                StartCoroutine(slashObj.GetComponent<Deactivate>().Deactivation());
                slashObj.SetActive(true);

                return slashObj;
            }
        }
        return null;
    }

    // Locate and rotate slash projectile
    public GameObject CreateProjectile(Vector2 spawnLoc, Quaternion rotation)
    {
        for (int i = 0; i < projectileNum; i++)
        {
            GameObject projectileObj = projectilePool[i];
            if (!projectileObj.activeInHierarchy)
            {
                projectileObj.transform.position = spawnLoc;
                projectileObj.transform.rotation = rotation;
                projectileObj.SetActive(true);

                return projectileObj;
            }
        }
        return null;
    }

    // Locate and rotate the bullet, initialize the move speed, add color, change scale
    public GameObject CreateMoveBullet(Vector2 spawnLoc, Quaternion rotation, float speed, Color color, float scale)
    {
        for (int i = 0; i < moveBulletNum; i++)
        {
            GameObject moveBulletObj = moveBulletPool[i];
            if (!moveBulletObj.activeInHierarchy)
            {
                moveBulletObj.transform.position = spawnLoc;
                moveBulletObj.transform.rotation = rotation;
                moveBulletObj.GetComponent<MoveBullet>().speed = speed;
                moveBulletObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
                moveBulletObj.transform.localScale = originalScale * scale;
                moveBulletObj.SetActive(true);

                return moveBulletObj;
            }
        }
        return null;
    }

    // Find spawn location of chaser and start shooting
    public GameObject CreateChaser(Vector2 spawnLoc)
    {
        for (int i = 0; i < chaserNum; i++)
        {
            GameObject chaserObj = chaserPool[i];
            if (!chaserObj.activeInHierarchy)
            {
                chaserObj.transform.position = spawnLoc;

                chaserObj.SetActive(true);
                StartCoroutine(chaserObj.GetComponent<Chaser>().Shoot());

                return chaserObj;
            }
        }
        return null;
    }

    // Initialize all fields for explosions and start the explosion
    public GameObject CreateExplosion(Vector2 spawnLoc, float initialSpeed, float afterSpeed, int bulletNum, float explosionAngle)
    {
        for (int i = 0; i < explosionNum; i++)
        {
            GameObject explosionObj = explosionPool[i];
            if (!explosionObj.activeInHierarchy)
            {
                explosionObj.transform.position = spawnLoc;

                Explosion explosionComponent = explosionObj.GetComponent<Explosion>();
                explosionComponent.initialSpeed = initialSpeed;
                explosionComponent.afterSpeed = afterSpeed;
                explosionComponent.bulletNum = bulletNum;
                explosionComponent.explosionAngle = explosionAngle;

                explosionObj.SetActive(true);
                StartCoroutine(explosionComponent.Explode());

                return explosionObj;
            }
        }
        return null;
    }
}
