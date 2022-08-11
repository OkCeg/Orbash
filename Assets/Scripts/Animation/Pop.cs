using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Pop : MonoBehaviour
{
    public GameObject smallerBalls;

    public void End(string message)
    {
        if (message.Equals("Animation Ended"))
        {
            Destroy(transform.parent.gameObject);
            TwelveAttack();
        }
    }

    public void TwelveAttack()
    {
        for (int i = 0; i < 12; i++)
        {
            Instantiate(smallerBalls, transform.position, Quaternion.Euler(0, 0, 30 * i));
        }
    }
}
