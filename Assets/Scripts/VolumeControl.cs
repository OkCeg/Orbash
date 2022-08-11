using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    //to stop music when paused
    public static AudioSource Source;

    public AudioMixer mixer;
    public Slider slider;

    //ending early for smooth loop
    private float time;

    /*
     * Hazard Control
     * measures = 43
     * repeatMeasures = 17
     * totalMeasures = 60
     * bpm = 180
     */

    //subtracted one beat
    private float length = 80f - 0.33f;

    private void Start()
    {
        Source = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();

        //defaults to the set ratio volume, but otherwise returns saved volume
        slider.value = PlayerPrefs.GetFloat("MusicVol", 0.5f);

        SetVolume(slider.value);

        StartCoroutine(Timer());
    }

    public void SetVolume(float sliderValue)
    {
        //from -80 dB to 0 dB (slider set from 0.0001 to 1)
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);

        //saved to PlayerPrefs
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }

    public IEnumerator Timer()
    {
        Source.Play();
        yield return new WaitForSeconds(length);
        StartCoroutine(Timer());
    }
}
