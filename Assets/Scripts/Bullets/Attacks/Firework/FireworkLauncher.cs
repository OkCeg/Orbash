using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkLauncher : MonoBehaviour
{
    // set in inspector
    [SerializeField] private GameObject firework;
    [SerializeField] private float timeInterval; //default 0.4 if single, 0.8 if double
    [SerializeField] private int additional; //default 0
    [SerializeField] private float delay; //delay before fire; default 0
    [SerializeField] private bool toRight; //launch fireworks reversed if false

    // initial bullet launch speed
    private float angle;
    // initial bullet launch speed
    private float speed;

    // cache WaitForSeconds
    private WaitForSeconds yieldDelay;
    private WaitForSeconds yieldInterval;

    private void Start()
    {
        yieldDelay = new WaitForSeconds(delay);
        yieldInterval = new WaitForSeconds(timeInterval);

        if (toRight)
        {
            StartCoroutine(Launch());
        }
        else
        {
            StartCoroutine(ReverseLaunch());
        }
    }

    // Fireworks!
    private IEnumerator Launch()
    {
        yield return yieldDelay;

        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j <= additional; j++)
            {
                angle = Random.Range(-35f, -5f);
                speed = Random.Range(15.0f, 20.0f);

                GameObject fw = Instantiate(firework, transform.position, Quaternion.identity);
                Firework fwGet = fw.GetComponent<Firework>();
                fwGet.speed = speed;
                fwGet.angle = angle;

                int randomColorIndex = Random.Range(0, ColorRandomizer.Instance.colors.Length);
                fw.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorRandomizer.Instance.colors[randomColorIndex];
            }

            yield return yieldInterval;
        }
    }

    // Fireworks (but flipped)!
    private IEnumerator ReverseLaunch()
    {
        yield return yieldDelay;

        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j <= additional; j++)
            {
                angle = Random.Range(5f, 35f);
                speed = Random.Range(5.0f, 9.0f);

                GameObject fw = Instantiate(firework, transform.position, Quaternion.identity);
                Firework fwGet = fw.GetComponent<Firework>();
                fwGet.speed = speed;
                fwGet.angle = angle;

                int randomColorIndex = Random.Range(0, ColorRandomizer.Instance.colors.Length);
                fw.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorRandomizer.Instance.colors[randomColorIndex];
            }

            yield return yieldInterval;
        }
    }
}
