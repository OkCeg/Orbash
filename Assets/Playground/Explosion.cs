using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : ColorRandomizer
{
    // Set in inspector
    [SerializeField] private GameObject moveBullet;
    [SerializeField] private float scale = 0.3f; // size scale of bullet
    [SerializeField] private float delay = 0.5f; // delay until explosion
    [SerializeField] private float speedDelay = 0.2f; // delay until speed change

    private Color randomColor; //randomized at start
    public float initialSpeed = 8f; //initial speed of bullet
    public float afterSpeed = 4f; //speed of bullet after initial speed
    public int bulletNum = 32; //number of bullets in explosion
    public float explosionAngle = 0f; // shift explosion

    private void Awake()
    {
        randomColor = colors[Random.Range(0, colors.Length)];
    }

    // Explode into pieces
    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(delay);

        GameObject[] bulletList = new GameObject[bulletNum];
        for (int i = 0; i < bulletNum; i++)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.forward * (i * 360f / bulletNum + 90 + explosionAngle));
            GameObject mb = ObjectPool.SharedInstance.CreateMoveBullet(transform.position, rotation, initialSpeed, randomColor, scale);
            if (mb != null)
            {
                bulletList[i] = mb;
            }
            // bullet.GetComponentInChildren<Light>().range *= scale;
        }

        GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(speedDelay);

        for (int i = 0; i < bulletNum; i++)
        {
            bulletList[i].GetComponent<Rigidbody2D>().velocity = bulletList[i].transform.right * afterSpeed;
        }

        gameObject.SetActive(false);
    }
}
