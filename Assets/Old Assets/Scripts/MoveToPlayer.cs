using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public GameObject player;

    public float speed;

    private void Start()
    {
        if (Health.Alive)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            GetComponent<Rigidbody2D>().velocity = (Vector2)(player.transform.position - transform.position).normalized * speed;
        }
    }

}
