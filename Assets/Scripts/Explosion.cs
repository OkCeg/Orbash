using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : ColorRandomizer
{
    // Set in inspector
    [SerializeField] private GameObject moveBullet;
    [SerializeField] private float initialSpeed; //initial speed of bullet; default 2
    [SerializeField] private float afterSpeed; //speed of bullet after initial speed; default 1
    [SerializeField] private float scale; //size scale of bullet; default 0.4
    [SerializeField] private float delay; //delay until explosion; default 0.5
    [SerializeField] private float speedDelay; //delay until speed change; default 0.2
    [SerializeField] private int bulletNum; //number of bullets in explosion; default 32

    private Color randomColor; //randomized at start

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
            bullet.GetComponentInChildren<Light>().range *= scale;
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
