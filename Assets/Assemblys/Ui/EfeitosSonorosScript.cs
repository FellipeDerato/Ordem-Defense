using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EfeitosSonorosScript : MonoBehaviour
{
   
    public AudioMixer controllerEfeitos;
    public GameObject VolumeEf;
    public Slider sliderEfeitos;
    void Start()
    {
        sliderEfeitos = VolumeEf.GetComponent<Slider>();
    }

    public void AjustaVolume(float volume)
    {
        controllerEfeitos.SetFloat("VolumeEfeito", volume);
    }
}