using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Increase damage the further the projectile travels
public class ProjectileDamage : MonoBehaviour
{
    // Set in inspector
    [SerializeField] private float damageScale; // how much to increase damage per frame
    public float damage;

    private void FixedUpdate()
    {
        damage += damageScale;
    }
}
