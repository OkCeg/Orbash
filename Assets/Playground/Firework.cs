using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    // set in inspector
    [SerializeField] private GameObject gravityBaseBullet;
    [SerializeField] private int cluster; //default 5 or 8
    [SerializeField] private float miniBulletSpeed; //the speed of the clustered bullets; default 12 with gravity 1, default 6 with gravity 0.6

    // randomized in FireworkLauncher
    public float speed;
    public float angle;

    private Rigidbody2D rb;
    private Color thisColor;

    // makes sure the Update() doesn't detect the drop
    // within the small time gap of WaitForFixedUpdate
    private bool ready = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thisColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        StartCoroutine(Init(speed, angle));
    }

    public IEnumerator Init(float spd, float angleFromY)
    {
        rb.rotation = angleFromY;

        //necessary to update and apply physics of rb.rotation
        yield return new WaitForFixedUpdate();

        ready = true;
        rb.velocity = transform.up * spd;
    }

    private void Update()
    {
        if (ready && rb.velocity.y < -2)
        {
            Cluster(cluster, miniBulletSpeed);
            gameObject.SetActive(false);
        }
    }

    private void Cluster(int num, float speed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject bullet = Instantiate(gravityBaseBullet, transform.position, Quaternion.Euler(Vector3.forward * (i * 360 / num + 90)));
            bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = thisColor;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity += (Vector2)bullet.transform.right * speed;
            bulletRB.velocity += rb.velocity / 2;
        }
    }
}
