using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletSample : MonoBehaviour
{
    private Transform pos;
    private Rigidbody2D rb;

    public float speed;

    private void Start()
    {
        pos = gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (pos.position.x > 4)
        {
            rb.velocity = Vector2.left * speed;
        }
        else if (pos.position.x < -4)
        {
            rb.velocity = Vector2.right * speed;
        }
    }
}
