using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColorRandomizer : MonoBehaviour
{
    // Singleton for other classes to access the colors
    public static ParticleColorRandomizer Instance { get; private set; }

    // Set in inspector
    public Material redMaterial;
    public Material orangeMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material purpleMaterial;
    public Material pinkMaterial;

    // Set in inspector
    public Material defaultSprite;
    public Sprite redSprite;
    public Sprite orangeSprite;
    public Sprite yellowSprite;
    public Sprite greenSprite;
    public Sprite blueSprite;
    public Sprite purpleSprite;
    public Sprite pinkSprite;

    public Material[] colorMaterials;
    public Sprite[] colorSprites;

    public void Awake()
    {
        Instance = this;
        colorMaterials = new Material[] { redMaterial, orangeMaterial, yellowMaterial, greenMaterial, blueMaterial, purpleMaterial, pinkMaterial };
        colorSprites = new Sprite[] { redSprite, orangeSprite, yellowSprite, greenSprite, blueSprite, purpleSprite, pinkSprite };
    }
}
