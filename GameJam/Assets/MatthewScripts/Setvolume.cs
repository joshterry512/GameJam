﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Setvolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetLevel(float sliderValue){
        mixer.SetFloat("Musicvol", Mathf.Log10 (sliderValue) * 20);
    }
}
