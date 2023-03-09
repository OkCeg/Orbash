using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Increase damage the further the projectile travels
public class ProjectileDamage : MonoBehaviour
{
    [SerializeField] private float damageScale = 0.1f; // how much to increase damage per frame
    public float damage = 0.5f;

    private void FixedUpdate()
    {
        damage += damageScale;
    }
}
