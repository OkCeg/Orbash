using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject followBullet;

    private Transform child;

    public static float FireInterval = 0.73f;

    private float timer;
    private float nextTime;

    private bool waitingFinished;

    private void Start()
    {
        child = transform.GetChild(0);

        //important when resetting the game within the game
        nextTime = Time.time + FireInterval;

        StartCoroutine(Waiter());
    }

    private void Update()
    {
        if (waitingFinished)
        {
            if (Time.time > nextTime)
            {
                Instantiate(followBullet, child.position, Quaternion.identity);
                nextTime = Time.time + FireInterval;
            }
        }
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.8f);

        waitingFinished = true;
    }
}
