using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicaScript : MonoBehaviour
{

    public AudioMixer controllerMusica;
    public GameObject VolumeM;
    public Slider sliderMusica;
    void Start()
    {
        sliderMusica = VolumeM.GetComponent<Slider>();
    }

    public void AjustaVolume(float volume)
    {
        controllerMusica.SetFloat("VolumeMusica", volume);
    }
}