using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitVelocity : MonoBehaviour
{
    private Rigidbody2D rb;
    public float lowerLimit;
    public float upperLimit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < lowerLimit)
        {
            rb.velocity = new Vector2(rb.velocity.x, lowerLimit);
        }
        else if (rb.velocity.y > upperLimit)
        {
            rb.velocity = new Vector2(rb.velocity.x, upperLimit);
        }
    }
}
