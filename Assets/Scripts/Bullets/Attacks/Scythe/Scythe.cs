using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The boss's projectile for the scythe attack
public class Scythe : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = rotationSpeed;
    }
}
