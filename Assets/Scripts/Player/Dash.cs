using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player dash
public class Dash : MonoBehaviour
{
    private GameObject player;
    private Transform pos;
    private Rigidbody2D rb;
    private Transform ps; // partilcle system

    // checks the time to stop the dash
    public float startDashTime;

    // checks the timer for startDashTime
    private float dashTimeLeft;

    // the speed at which the player dashes
    public float dashSpeed;

    public int direction;

    // for one time invincibility disable (necessary so that damage invincibility works)
    private bool turnedOff = true;

    private void Start()
    {
        player = gameObject;
        pos = player.transform;
        rb = GetComponent<Rigidbody2D>();
        ps = transform.GetChild(0);
    }

    private void Update()
    {
        DashDirection();

        if (Input.GetKeyDown(KeyCode.X))
        {
            // for starting the timer on the dash time
            dashTimeLeft = startDashTime;

            // i-frames for dashing
            Health.Invincible = true;
            turnedOff = false;
        }

        if (dashTimeLeft <= 0)
        {
            if (!turnedOff)
            {
                // don't turn off if invincible due to taking damage
                if (!Health.InvincibleByDamage)
                {
                    Health.Invincible = false;
                }
                turnedOff = true;
            }
            rb.velocity = Vector2.zero;
        }
        else
        {
            dashTimeLeft -= Time.deltaTime;

            rb.velocity = Quaternion.Euler(0, 0, 45 * direction) * Vector2.right * dashSpeed;

            // Old method of calculating velocity
            /*
            float diaDash = dashSpeed* Mathf.Sqrt(2) / 2;
            switch (direction)
            {
                case 0:
                    rb.velocity = Vector2.right * dashSpeed;
                    break;
                case 1:
                    rb.velocity = new Vector2(diaDash, diaDash);
                    break;
                case 2:
                    rb.velocity = Vector2.up * dashSpeed;
                    break;
                case 3:
                    rb.velocity = new Vector2(-diaDash, diaDash);
                    break;
                case 4:
                    rb.velocity = Vector2.left * dashSpeed;
                    break;
                case 5:
                    rb.velocity = new Vector2(-diaDash, -diaDash);
                    break;
                case 6:
                    rb.velocity = Vector2.down * dashSpeed;
                    break;
                case 7:
                    rb.velocity = new Vector2(diaDash, -diaDash);
                    break;
                default:
                    Debug.Log("how did we get here?");
                    break;
            }
            */
        }
    }

    // Might be able to save code length and performance speed by sharing with Movement.cs
    private void DashDirection()
    {
        // direction 0 starts from right and goes counterclockwise
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            direction = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            direction = 1;
        }
        else if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            direction = 2;
        }
        else if (!Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            direction = 3;
        }
        else if (!Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            direction = 4;
        }
        else if (!Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            direction = 5;
        }
        else if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            direction = 6;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            direction = 7;
        }

        //for rotating the particle system (opposite of movement)
        ps.rotation = Quaternion.Euler(0, 0, 45 * direction - 90);
    }
}
