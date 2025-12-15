using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeVozesScript : MonoBehaviour
{

    public AudioMixer controllerVozes;
    public GameObject VolumeVZ;
    public Slider sliderVozes;
    void Start()
    {
        sliderVozes = VolumeVZ.GetComponent<Slider>();
    }

    public void AjustaVolume(float volume)
    {
        controllerVozes.SetFloat("VolumeVozes", volume);
    }
}