using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// Randomly select the bullet particle color
public class RandomColorSprite : MonoBehaviour
{
    public static RandomColorSprite SharedInstance;
    public Sprite[] sprites;

    private void Awake()
    {
        SharedInstance = this;
    }

    public Sprite GetRandomSprite()
    {
        return sprites[Random.Range(0, sprites.Length)];
    }
}
