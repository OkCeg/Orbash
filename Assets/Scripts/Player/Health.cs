using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public SpriteRenderer sr;

    public static Color Normal;
    public Color invincibleColor;

    public static int HP;
    public static bool Invincible;
    public static bool InvincibleByDamage; //used to toggle invincibility when damaged vs dashing
    public static bool Alive = true;

    //for changing health text color when hit (unused as of post-firework balancing)
    public Color healthFour;
    public Color healthThree;
    public Color healthTwo;
    public Color healthOne;

    // invincibility time
    public float iframes = 1.5f;

    private WaitForSeconds yieldIFrames;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Normal = sr.color;

        //change later
        HP = 100;

        Invincible = false;
        Alive = true;

        yieldIFrames = new WaitForSeconds(iframes);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoseHealth(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        LoseHealth(collision);
    }

    private void Update()
    {
        if (Invincible)
        {
            sr.color = invincibleColor;
        }
        else
        {
            sr.color = Normal;
        }
    }

    private IEnumerator IFrames()
    {
        Invincible = true;
        InvincibleByDamage = true;
        yield return yieldIFrames;
        Invincible = false;
        InvincibleByDamage = false;
    }

    /*private void ChangeHealthText()
    {
        switch (HP)
        {
            case 3:
                HealthText.text.color = healthThree;
                break;
            case 2:
                HealthText.text.color = healthTwo;
                break;
            case 1:
            case 0:
                HealthText.text.color = healthOne;
                break;
            default:
                HealthText.text.color = healthFour;
                break;
        }
    }*/

    private void LoseHealth(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !Invincible)
        {
            HP--;

            if (HP <= 0)
            {
                Destroy(gameObject);
                Alive = false;
            }

            StartCoroutine(IFrames());

            // Debug.Log("ouch");
            // ChangeHealthText();
        }
    }
}
