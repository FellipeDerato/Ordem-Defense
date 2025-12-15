using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanternaScript : MonoBehaviour
{
    public GameObject Luz;
    public GameObject Vidro;
    public Color CorLigado;
    public Color CorDesligado;
    public float Intensidade = 3f;
    public bool MouseEmCima = false;

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (Luz.GetComponent<Light>().enabled)
            {
                Desligou();
            }
            else
            {
                Ligou();
            }
        }
        if (MouseEmCima)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (Luz.GetComponent<Light>().enabled)
                {
                    Desligou();
                }
                else
                {
                    Ligou();
                }
            }
        }
        
    }
    
    public void OnMouseEnter()
    {
        MouseEmCima = true;
    }
    public void OnMouseExit()
    {
        MouseEmCima = false;
    }

    public void Ligou()
    {
        Material mat = Vidro.GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", (CorLigado) * Intensidade);
        Luz.GetComponent<Light>().enabled = true;
        AudioSource fx = GetComponent<AudioSource>(); fx.Play();
    }

    public void Desligou()
    {
        Material mat = Vidro.GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", (CorDesligado) * 0);
        Luz.GetComponent<Light>().enabled = false;
        AudioSource fx = GetComponent<AudioSource>(); fx.Play(); // fx.PlayOneShot(fx.clip);
    }


}
