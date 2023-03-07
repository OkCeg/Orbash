using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : ColorRandomizer
{
    // Set in inspector
    [SerializeField] private GameObject moveBullet;
    [SerializeField] private float scale = 0.3f; //size scale of bullet
    [SerializeField] private float delay = 0.5f; //delay until explosion
    [SerializeField] private float speedDelay = 0.2f; //delay until speed change

    private Color randomColor; //randomized at start
    public float initialSpeed = 8f; //initial speed of bullet
    public float afterSpeed = 4f; //speed of bullet after initial speed
    public int bulletNum = 32; //number of bullets in explosion

    // Start is called before the first frame update
    void Start()
    {
        randomColor = colors[Random.Range(0, colors.Length)];
        StartCoroutine(Explode());
    }

    // Explode into pieces
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(delay);

        GameObject[] bulletList = new GameObject[bulletNum];
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bullet = Instantiate(moveBullet, transform.position, Quaternion.Euler(Vector3.forward * (i * 360 / bulletNum + 90)));
            bulletList[i] = bullet;
            bullet.GetComponent<MoveBullet>().speed = initialSpeed;
            bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = randomColor;
            bullet.transform.localScale *= scale;
            // bullet.GetComponentInChildren<Light>().range *= scale;
        }

        GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(speedDelay);

        for (int i = 0; i < bulletNum; i++)
        {
            bulletList[i].GetComponent<Rigidbody2D>().velocity = bulletList[i].transform.right * afterSpeed;
        }

        Destroy(gameObject);
    }
}
