using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkLauncher : MonoBehaviour
{
    //set in inspector
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject firework;
    [SerializeField] private float timeInterval = 0.2f;
    [SerializeField] private int additional = 0;

    private float angle;
    private float speed;
    private int randomColor;

    private void Start()
    {
        StartCoroutine(Launch());
    }

    //nts: balance fireworks (too fast)
    //make angles uniquely separated if more than one at a time if additional
    //make negative scale also work (on right side)
    private IEnumerator Launch()
    {
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j <= additional; j++)
            {
                angle = Random.Range(-30f, -10f);
                speed = Random.Range(6.0f, 10.0f);
                randomColor = Random.Range(0, colors.Length);

                GameObject fw = Instantiate(firework, transform.position, Quaternion.identity);
                Firework fwGet = fw.GetComponent<Firework>();
                fwGet.speed = speed;
                fwGet.angle = angle;

                fw.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[randomColor];
            }

            yield return new WaitForSeconds(timeInterval);
        }
    }
}
