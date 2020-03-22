using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControll : MonoBehaviour
{
    public Slider slider;

    private AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ControllVolume();
    }
    private void ControllVolume()
    {
        audio.volume = slider.value;
    }
}
