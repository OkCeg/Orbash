using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Circler : ColorRandomizer
{
    //set in inspector
    [SerializeField] private GameObject moveBullet;
    [SerializeField] private int angleInterval = 27;
    [SerializeField] private float timeInterval = 0.1f;
    [SerializeField] private int additional = 0;
    [SerializeField] private float bulletSpeed = 8f;

    private int angle = 0;
    private int count = 0;
    private WaitForSeconds yieldInterval;

    private void Start()
    {
        StartCoroutine(CircleWait());
        yieldInterval = new WaitForSeconds(timeInterval);
    }

    private IEnumerator CircleWait()
    {
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j <= additional; j++)
            {
                Quaternion rotation = Quaternion.Euler(Vector3.forward * (angle + j * 360 / (additional + 1)));
                ObjectPool.SharedInstance.CreateMoveBullet(transform.position, rotation, bulletSpeed, colors[count], 1);
            }

            angle += angleInterval;
            count++;

            if (count >= colors.Length)
            {
                count = 0;
            }

            yield return yieldInterval;
        }
    }
}
