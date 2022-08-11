using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutIntro : MonoBehaviour
{
    private Text text;
    private Color color;

    private float fadeSpeed = 0;

    private void Start()
    {
        text = GetComponent<Text>();
        color = text.color;
    }

    private void Update()
    {
        fadeSpeed += 0.001f;
        color.a = Mathf.Lerp(color.a, 0, fadeSpeed * Time.deltaTime);
        text.color = color;

        if (color.a < 0.01)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
