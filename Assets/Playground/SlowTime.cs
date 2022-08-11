using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    public AudioSource source;

    public void Reset()
    {
        source.pitch = 1;
        Time.timeScale = 1;
    }

    public void Halve()
    {
        source.pitch /= 2;
        Time.timeScale /= 2;
    }

    public void FiftyPercentBoost()
    {
        source.pitch *= 1.5f;
        Time.timeScale *= 1.5f;
    }
}
