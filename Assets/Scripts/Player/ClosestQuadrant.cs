using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Which corner the player is closest to
// Used to determine how and where the boss attacks
public class ClosestQuadrant : MonoBehaviour
{
    public static ClosestQuadrant Instance { get; private set; }

    // Set in inspector
    public Vector2 corner1;
    public Vector2 corner2;
    public Vector2 corner3;
    public Vector2 corner4;

    public Vector2[] corners;

    private GameObject player;
    private float[] sqrDistanceToCorners;

    private void Awake()
    {
        Instance = this;
        corners = new Vector2[] { corner1, corner2, corner3, corner4 };
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sqrDistanceToCorners = new float[4];
    }

    public int CalculateClosestQuadrant()
    {
        // In case the calculation is called in another script before Start()
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            sqrDistanceToCorners = new float[4];
        }

        sqrDistanceToCorners[0] = Vector2.SqrMagnitude((Vector2) player.transform.position - corner1);
        sqrDistanceToCorners[1] = Vector2.SqrMagnitude((Vector2) player.transform.position - corner2);
        sqrDistanceToCorners[2] = Vector2.SqrMagnitude((Vector2) player.transform.position - corner3);
        sqrDistanceToCorners[3] = Vector2.SqrMagnitude((Vector2) player.transform.position - corner4);

        float closestSqrDistance = Mathf.Min(sqrDistanceToCorners);
        int closestQuadrant = Array.IndexOf(sqrDistanceToCorners, closestSqrDistance);

        return closestQuadrant+1;
    }
}
