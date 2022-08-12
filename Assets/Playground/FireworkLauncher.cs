using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkLauncher : ColorRandomizer
{
    // set in inspector
    [SerializeField] private GameObject firework;
    [SerializeField] private float timeInterval; //default 0.35
    [SerializeField] private int additional; //default 0

    // initial bullet launch speed
    private float angle;
    // initial bullet launch speed
    private float speed;

    private void Start()
    {
        StartCoroutine(Launch());
    }

    // ToDo:
    // make angles uniquely separated if more than one at a time if additional
    // make negative scale also work (on right side)
    private IEnumerator Launch()
    {
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j <= additional; j++)
            {
                angle = Random.Range(-35f, -5f);
                speed = Random.Range(5.0f, 9.0f);

                GameObject fw = Instantiate(firework, transform.position, Quaternion.identity);
                Firework fwGet = fw.GetComponent<Firework>();
                fwGet.speed = speed;
                fwGet.angle = angle;

                int randomColorIndex = Random.Range(0, colors.Length);
                fw.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[randomColorIndex];
            }

            yield return new WaitForSeconds(timeInterval);
        }
    }
}
