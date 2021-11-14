using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Graphics : MonoBehaviour
{
    private void Start()
    {
        //set vsync to 0
        QualitySettings.vSyncCount = 0;
        //set max framerate to 60
        Application.targetFrameRate = 60;
        //set screen resolution to 1080p and fullscreen
        Screen.SetResolution(1920, 1080, true);
    }
}