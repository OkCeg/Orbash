using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    //distance to bottom < -3.6
    //distance to top > 5
    //distance to right > 8.8
    //distance to left < -8.8

    public GameObject standardBullet;
    public GameObject shooter;
    public GameObject popBullet;

    private float popAnimationTime = 2f;

    private float introWait = 2f;
    private float summonShooter = 12.2f;
    private float poppers = 24.85f;

    private void Start()
    {
        StartCoroutine(Beginning());
        StartCoroutine(Shooter());
        StartCoroutine(Pops());
    }

    //delete orbs when dings ends
    private IEnumerator Beginning()
    {
        yield return new WaitForSeconds(introWait);

        for (int i = 0; i < 240; i++)
        {
            Instantiate(standardBullet, new Vector3(9f, Random.Range(-3.4f, 4.8f), 0), Quaternion.Euler(0, 0, 180));
            Instantiate(standardBullet, new Vector3(-9f, Random.Range(-3.4f, 4.8f), 0), Quaternion.identity);

            /*switch (i % 2)
            {
                case 0:
                    Instantiate(standardBullet, new Vector3(9f, Random.Range(-3.4f, 4.8f), 0), Quaternion.Euler(0, 0, 180));
                    break;
                case 1:
                    Instantiate(standardBullet, new Vector3(-9f, Random.Range(-3.4f, 4.8f), 0), Quaternion.identity);
                    break;
                default:
                    Debug.Log("how did we get here?");
                    break;
            }*/

            yield return new WaitForSeconds(0.19f);
        }
    }

    private IEnumerator Shooter()
    {
        yield return new WaitForSeconds(summonShooter);

        Instantiate(shooter, new Vector3(-6, 4.5f, 0), Quaternion.identity);
        Instantiate(shooter, new Vector3(6, 4.5f, 0), Quaternion.identity);
    }

    private IEnumerator Pops()
    {
        yield return new WaitForSeconds(poppers - popAnimationTime);

        for (int i = 0; i < 15; i++)
        {
            Instantiate(popBullet, new Vector3(Random.Range(-8f, 8f), Random.Range(-9.8f, -6.1f), 0), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }

        StartCoroutine(Pops2());
    }

    private IEnumerator Pops2()
    {
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 50; i++)
        {
            //choose whether left or right is upside down
            if (Random.value < 0.5)
            {
                Instantiate(popBullet, new Vector3(Random.Range(-8f, -1f), Random.Range(-9.8f, -6.1f), 0), Quaternion.identity);
                Instantiate(popBullet, new Vector3(Random.Range(1f, 8f), Random.Range(6.1f, 9.8f), 0), Quaternion.Euler(0, 0, 180));
            }
            else
            {
                Instantiate(popBullet, new Vector3(Random.Range(-8f, -1f), Random.Range(6.1f, 9.8f), 0), Quaternion.Euler(0, 0, 180));
                Instantiate(popBullet, new Vector3(Random.Range(1f, 8f), Random.Range(-9.8f, -6.1f), 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
