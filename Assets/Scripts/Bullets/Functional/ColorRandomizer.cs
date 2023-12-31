using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains the colors for color randomization
public class ColorRandomizer : MonoBehaviour
{
    // Singleton for other classes to access the colors
    public static ColorRandomizer Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public Color[] colors = {
        new Color(1, 121f/255, 121f/255, 1),
        new Color(1, 193f/255, 85f/255, 1),
        new Color(248f/255, 1, 78f/255, 1),
        new Color(81f/255, 1, 78f/255, 1),
        new Color(109f/255, 211f/255, 1, 1),
        new Color(207f/255, 148f/255, 1, 1),
        new Color(1, 100f/255, 213f/255, 1)};
}
