using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBG : MonoBehaviour
{
    private Image image;
    private Color color;

    private float fadeSpeed = 0;

    private void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
    }

    private void Update()
    {
        fadeSpeed += 0.001f;
        color.a = Mathf.Lerp(color.a, 0, fadeSpeed * Time.deltaTime);
        image.color = color;

        if (color.a < 0.01)
        {
            Destroy(gameObject);
        }
    }
}
