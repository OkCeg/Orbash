using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure the slash sprite moves with the player
public class MoveWithPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 previousPosition;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        previousPosition = playerTransform.position;
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition = playerTransform.position;
        transform.position += currentPosition - previousPosition;
        previousPosition = currentPosition;
    }
}