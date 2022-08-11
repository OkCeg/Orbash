using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circler : MonoBehaviour
{
    //set in inspector
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject movingBullet;
    [SerializeField] private int angleInterval = 27;
    [SerializeField] private float timeInterval = 0.1f;
    [SerializeField] private int additional = 0;

    private int angle = 0;
    private int count = 0;

    private void Start()
    {
        StartCoroutine(CircleWait());
    }

    private IEnumerator CircleWait()
    {
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j <= additional; j++)
            {
                GameObject bullet = Instantiate(movingBullet, transform.position, Quaternion.Euler(Vector3.forward * (angle + j * 360 / (additional + 1))));
                bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[count];
            }

            angle += angleInterval;
            count++;

            if (count >= colors.Length)
            {
                count = 0;
            }

            yield return new WaitForSeconds(timeInterval);
        }
    }
}
