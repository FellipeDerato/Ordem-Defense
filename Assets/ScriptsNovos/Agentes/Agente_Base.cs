using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Agente_Base : MonoBehaviour
{

    [Header("UI Coisas")]

    public GameObject IconesGrupo;
    public GameObject VenderEfeito;

    [Header("Sons")]
    public AudioClip VenderSomCell;
    public AudioClip CliqueSomCell;
    public AudioClip FecharSomCell;


    [Header("Sistema")]
    public bool mouseDentroDoObjeto = false;
    public bool Menu_Agente_bool = false;
    public bool AgenteClicado;

    [Header("Não Mexer")]
    public Animator animatorMenu;
    public Animator animatorAgente;
    public GameObject bl;

    ComprarTorre cpt;
    Agente_Especifico agtE;
    public GameMaster gm;
    public Agente_UI cell;
    public CardAgente cardOrigem;

    [Header("Som")]

    public AudioSource audiosource1;
    public AudioSource audiosource2;    

    void Start()
    {
        animatorMenu = cell.animator;
        animatorAgente = GetComponent<Animator>();
        agtE = GetComponent<Agente_Especifico>();
        cpt = gm.cpt;

        

        audiosource1 = gameObject.AddComponent<AudioSource>();
        audiosource1.name = "audiosource1";
        audiosource2 = gameObject.AddComponent<AudioSource>();
        audiosource2.name = "audiosource2";

        audiosource1.pitch = 1;
        audiosource2.pitch = 1;


        mouseDentroDoObjeto = false;

        gm.mm.L_Agentes.Add(gameObject.GetComponent<Agente_Base>());
    }
        
    public void ChangeTarget() 
    {
        ///muda o valor e o texto e o tipo de target no Agente_Especifico
        agtE.EscolhaDeTarget++;
        if (agtE.EscolhaDeTarget == 2)
        {
            cell.AlvoTexto.text = "Alvo:\nO Último.";
        }
        if (agtE.EscolhaDeTarget == 3)
        {
            cell.AlvoTexto.text = "Alvo:\nO Mais Forte.";
        }
        if (agtE.EscolhaDeTarget == 4)
        {
            cell.AlvoTexto.text = "Alvo:\nO Mais Perto.";
        }
        if (agtE.EscolhaDeTarget == 5)
        {
            cell.AlvoTexto.text = "Alvo:\nO Mais Longe.";
        }
        if (agtE.EscolhaDeTarget > 5)
        {
            agtE.EscolhaDeTarget = 1;
            cell.AlvoTexto.text = "Alvo:\nO Primeiro.";
        }

    }   
    
    public void VenderAgente()
    {
        Menu_Agente(false);
        //gm.mm.ValorDinheiro += agtE. dinheiro coisa arrumar
        cpt.somVender.PlayOneShot(cpt.somVender.clip);
        animatorAgente.SetBool("Vendeu", true);
        animatorAgente.speed = 1;
        gm.mm.L_Agentes.Remove(gameObject.GetComponent<Agente_Base>());
        Destroy(GetComponentInParent<GameObject>().gameObject, 1f);
        GetComponent<Outline>().enabled = false;
        Vector3 efxVender = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        GameObject effectIns = (GameObject)Instantiate(VenderEfeito, efxVender, transform.rotation);
        Destroy(effectIns, 0.3f);
    }

    public void Menu_Agente(bool Ligada)
    {
        if (Ligada)
        {
            if (!IconesGrupo.activeSelf)
            {
                agtE.InvokeRepeating("Atualizar_Valores", 0, .1f);
            }
        }
        else if (!Ligada)
        {
            if (IconesGrupo.activeSelf)
            {
                agtE.CancelInvoke("Atualizar_Valores");
            }
        }
        Menu_Agente_bool = Ligada;
        cell.gameObject.SetActive(Ligada);
        animatorMenu.SetBool("EsquerdaAtivou", Ligada);
        GetComponent<Outline>().enabled = Ligada;
        IconesGrupo.SetActive(Ligada);
        gm.cpt.ClicadoPersonagem = Ligada;
        audiosource2.PlayOneShot(CliqueSomCell);

    }

    void Update()
    {
        cell.DanoCausado.text = "Dano: " + agtE.DanoAplicadoTotal;


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (cell.mouseDentroDoObjeto || mouseDentroDoObjeto)
            {
                if (!Menu_Agente_bool)
                {
                    Menu_Agente(true);
                }
            }
            else
            {
                if (Menu_Agente_bool)
                {
                    Menu_Agente(false);
                }
            }
        }
    }

}