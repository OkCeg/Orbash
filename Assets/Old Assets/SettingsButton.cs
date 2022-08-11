using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject panel;

    public static bool PanelOpened = false;

    private void Start()
    {
        PanelOpened = false;
        panel.SetActive(false);
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        PanelOpened = true;

        Time.timeScale = 0;
        VolumeControl.Source.Pause();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        PanelOpened = false;

        Time.timeScale = 1;
        VolumeControl.Source.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (PanelOpened)
            {
                ClosePanel();
            }
            else
            {
                OpenPanel();
            }
        }
    }
}
