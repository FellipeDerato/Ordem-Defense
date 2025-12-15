using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao3dHover : MonoBehaviour
{
    [Header("Sons")]
    public AudioSource Som1;
    public AudioSource Som2;

    [Header("Anima��es")]
    public string Animação1;
    public string Animação2;

    [Header("Qual Bot�o �")]
    public bool JOGAR = false;
    public bool FECHAR = false;
    public bool OPÇÕES = false;
    public bool CRÉDITOS = false;

    [Header("A��es")]
    public CenasController Cenas;
    public MenuScript Menu;

    private void OnMouseEnter()
    {
        Som1.PlayOneShot(Som1.clip);
        gameObject.GetComponent<Animator>().Play(Animação1);
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Animator>().Play(Animação2);
    }

    private void OnMouseUpAsButton()
    {
        if (JOGAR) { Cenas.ProximaCena(1); Som2.PlayOneShot(Som2.clip); }
        else if (FECHAR) { Cenas.FecharJOGO(); Som2.PlayOneShot(Som2.clip); }
    }


}
