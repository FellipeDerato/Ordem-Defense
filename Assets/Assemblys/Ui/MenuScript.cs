using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    [Header ("Textos")]

    public GameObject Txt_1;
    public GameObject Txt_2;
    public GameObject Txt_3;

    [Header("Sistema")]
    public bool transicionando;
    public GameObject Visão;
    public Transform Posição1;
    public Transform Posição2;
    public Transform Posição3;
    public float Tempo = .2f;
    public float TempoAnim;
    public int SetaValor = 1;
    public Color CorOriginal = new Color(112, 0, 255, 1f);
    public Color CorFraca = new Color(40, 0, 40, 0f);
    bool modo_1 = true;
    bool modo_2 = false;
    bool modo_3 = false;

    public GameObject objSetaDireita;
    public GameObject objSetaEsquerda;
    Vector3 Original;
    Vector3 Movimento1;
    bool Anim;
    
    public void Twitter()
    {
        Application.OpenURL("https://twitter.com/feripebeatbox");
        Application.OpenURL("https://twitter.com/cellbit");
    }

    public void SetaDireita()
    {
        if (SetaValor < 3)
        {
            SetaValor++;
        }
        AlterouCoisa();
    }

    public void SetaEsquerda()
    {
        if (SetaValor > 1)
        {
            SetaValor--;
        }
        AlterouCoisa();
    }

    void AlterouCoisa()
    {

        //Modos
        if (SetaValor == 1)
        {
            modo_1 = true; modo_2 = false; modo_3 = false;
            Txt_1.SetActive(true); Txt_2.SetActive(false); Txt_3.SetActive(false);
        }
        else if (SetaValor == 2)
        {
            modo_1 = false; modo_2 = true; modo_3 = false;
            Txt_2.SetActive(true); Txt_1.SetActive(false); Txt_3.SetActive(false);
        }
        else if(SetaValor == 3)
        {
            modo_1 = false; modo_2 = false; modo_3 = true;
            Txt_3.SetActive(true); Txt_1.SetActive(false); Txt_2.SetActive(false);
        }


        //Setas Ativa/Desativa
        if (SetaValor >= 3)
        {
            objSetaDireita.GetComponent<Image>().color = CorFraca;
            objSetaDireita.GetComponent<Animator>().Play("ScaleDisableAnimator");
            objSetaDireita.GetComponent<EventTrigger>().enabled = false;
            objSetaDireita.GetComponent<Button>().enabled = false;
        }
        else
        {
            objSetaDireita.GetComponent<Image>().color = CorOriginal;
            objSetaDireita.GetComponent<Animator>().enabled = true;
            objSetaDireita.GetComponent<EventTrigger>().enabled = true;
            objSetaDireita.GetComponent<Button>().enabled = true;
        }

        if (SetaValor <= 1)
        {
            objSetaEsquerda.GetComponent<Image>().color = CorFraca;
            objSetaEsquerda.GetComponent<Animator>().Play("ScaleDisableAnimator");
            objSetaEsquerda.GetComponent<EventTrigger>().enabled = false;
            objSetaEsquerda.GetComponent<Button>().enabled = false;
        }
        else
        {
            objSetaEsquerda.GetComponent<Image>().color = CorOriginal;
            objSetaEsquerda.GetComponent<Animator>().enabled = true;
            objSetaEsquerda.GetComponent<EventTrigger>().enabled = true;
            objSetaEsquerda.GetComponent<Button>().enabled = true;
        }
    }

    private void Update()
    {
        //Movimento Camera
        if (modo_1)
        {
            if (Visão.transform.position != Posição1.position)
            {
                transicionando = true;
                Visão.transform.position = Vector3.Lerp(Visão.transform.position, Posição1.position, Tempo * Time.deltaTime);
                Visão.transform.rotation = Quaternion.Lerp(Visão.transform.rotation, Posição1.rotation, Tempo * Time.deltaTime);
            }
            else 
            { 
                Original = Posição1.position;
                Movimento1 = new Vector3(Posição1.position.x, Posição1.position.y + .1f, Posição1.position.z);
                transicionando = false;
                modo_1 = false;
            }
        }
        else if (modo_2)
        {
            if (Visão.transform.position != Posição2.position)
            {
                transicionando = true;
                Visão.transform.position = Vector3.Lerp(Visão.transform.position, Posição2.position, Tempo * Time.deltaTime);
                Visão.transform.rotation = Quaternion.Lerp(Visão.transform.rotation, Posição2.rotation, Tempo * Time.deltaTime);
            }
            else
            {
                Original = Visão.transform.position;
                Movimento1 = new Vector3(Visão.transform.position.x, Visão.transform.position.y + .05f, Visão.transform.position.z);
                transicionando = false;
                modo_2 = false;
            }
        }
        else if (modo_3)
        {
            if (Visão.transform.position != Posição3.position)
            {
                transicionando = true;
                Visão.transform.position = Vector3.Lerp(Visão.transform.position, Posição3.position, Tempo * Time.deltaTime);
                Visão.transform.rotation = Quaternion.Lerp(Visão.transform.rotation, Posição3.rotation, Tempo * Time.deltaTime);
            }
            else
            {
                Original = Visão.transform.position;
                Movimento1 = new Vector3(Visão.transform.position.x, Visão.transform.position.y + .05f, Visão.transform.position.z);
                transicionando = false;
                modo_3 = false;
            }
        }

        if (!transicionando)
        {
            if (Anim)
            {
                if (Visão.transform.position != Original)
                {
                    Visão.transform.position = Vector3.Slerp(Visão.transform.position, Original, TempoAnim * Time.deltaTime);
                    return;
                }
                Anim = false;
            }
            else if (!Anim)
            {                
                if (Visão.transform.position != Movimento1)
                {
                    Visão.transform.position = Vector3.Slerp(Visão.transform.position, Movimento1, TempoAnim * Time.deltaTime);
                    return;
                }
                Anim = true;
            }
        }
    }
}
