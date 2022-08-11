using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//all commented numbers in parentheses are post-firework balancing
public class Movement : MonoBehaviour
{
    public GameObject hero;
    public Transform pos;
    public ParticleSystem ps;


    //the speed of player movement (0.05)
    public float speed;

    //how much to shrink by (0.8)
    public float scale;

    //time for full shrink to happen (10)
    public float scaleTime;

    private Vector3 normalScale;
    private Vector3 shrinkH;
    private Vector3 shrinkV;
    private bool isMovingH;
    private bool isMovingV;

    private void Start()
    {
        hero = gameObject;
        pos = hero.transform;
        ps = GetComponentInChildren<ParticleSystem>();

        normalScale = new Vector3(pos.localScale.x, pos.localScale.y, pos.localScale.z);
        shrinkH = new Vector3(pos.localScale.x * scale, pos.localScale.y, pos.localScale.z);
        shrinkV = new Vector3(pos.localScale.x, pos.localScale.y * scale, pos.localScale.z);
    }

    private void FixedUpdate()
    {
        Move();
        MovementAnimation();
    }

    private void Move()
    {
        if (Input.GetKey("up"))
        {
            isMovingV = true;
            pos.position += new Vector3(0, speed, 0);
        }
        if (Input.GetKey("down"))
        {
            isMovingV = true;
            pos.position -= new Vector3(0, speed, 0);
        }
        if (Input.GetKey("left"))
        {
            isMovingH = true;
            pos.position -= new Vector3(speed, 0, 0);
        }
        if (Input.GetKey("right"))
        {
            isMovingH = true;
            pos.position += new Vector3(speed, 0, 0);
        }

        //for resetting all traveling booleans
        if (!Input.GetKey("up") && !Input.GetKey("down"))
        {
            isMovingV = false;
        }
        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            isMovingH = false;
        }
    }

    private void MovementAnimation()
    {
        if (isMovingH)
        {
            pos.localScale = Vector3.Lerp(pos.localScale, shrinkV, scaleTime * Time.deltaTime);
        }
        if (isMovingV)
        {
            pos.localScale = Vector3.Lerp(pos.localScale, shrinkH, scaleTime * Time.deltaTime);
        }

        //particle system control
        var emission = ps.emission;
        if (!isMovingH && !isMovingV)
        {
            pos.localScale = normalScale;

            emission.enabled = false;
        }
        else
        {
            emission.enabled = true;
        }
    }
}
