using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuUpgd : MonoBehaviour
{
    [Header("Qual Agente �")]
    public bool agenteArthur = false;
    public bool agenteDante = false;
    public bool agenteRubens = false;
    public bool agenteBalu = false;
    public bool agenteSamuel = false;
    public bool agenteCarina = false;
    public bool agenteErin = false;

    [Header("Elemento Selecionado")]
    public bool Fisico;
    public bool Morte;
    public bool Sangue;
    public bool Conhecimento;
    public bool Energia;

    [Header("Tipo de Dano")]
    public bool DanoMele;

    [Header("System")]
    public bool mouseDentroDoObjeto = false;
    public bool menuAtivo = false;
    public bool AgenteClicado;
    public Camera Camera;
    
    public Animator animatorMenu;
    public Animator animatorAgente;
    public GameObject Menue;
    public GameObject IconesGrupo;


    public GameObject Vendeyeffect;
    public GameObject Parent;
    ComprarTorre conT;
    ScriptUpgrades sUpgrades;
    Torre1 torre1;
    public GameObject[] Aliados;

    [Header("Sons")]
    public AudioSource VenderSomCell;
    public AudioSource CliqueSomCell;
    public AudioSource FecharSomCell;

    [Header("Explica��o Ups")]
    public Text DanoCausado;
    public Text ExplicarUp1;
    public Text ExplicarUp2;
    public Text ExplicarUp3;

    [Header("Icones")]
    public GameObject iconOculto;
    public GameObject iconRaio;

    void Start()
    {
        mouseDentroDoObjeto = false;
        animatorMenu = Menue.GetComponent<Animator>();
        animatorAgente = gameObject.GetComponent<Animator>();

        torre1 = gameObject.GetComponent<Torre1>();

        sUpgrades = gameObject.GetComponent<ScriptUpgrades>();
        conT = GameObject.FindGameObjectWithTag("AreaDeCompra").GetComponent<ComprarTorre>();
    }

    void Update()
    {
        if (torre1.ocultoAcerta || torre1.ocultoAcertaTemporario)
        {
            iconOculto.SetActive(true);
        }
        else
        {
            iconOculto.SetActive(false);
        }



        //Explica��o Upgrades Arthur
        if (agenteArthur)
        {
            //Caminho 1
            if (sUpgrades.Upgrade1 == 0)
            {
                ExplicarUp1.text = "Aumenta Cadencia dos Tiros em 1.";
            }
            else if (sUpgrades.Upgrade1 == 1)
            {
                ExplicarUp1.text = "Aumenta Cadencia dos Tiros em mais 1.";
            }
            else if (sUpgrades.Upgrade1 == 2)
            {
                ExplicarUp1.text = "Aumenta Cadencia dos Tiros em mais 3.";
            }
            else if (sUpgrades.Upgrade1 >= 3)
            {
                ExplicarUp1.text = "Caminho Completo.";
            }

            //Caminho 2
            if (sUpgrades.Upgrade2 == 0)
            {
                ExplicarUp2.text = "Aumenta Dano em 2.";
            }
            else if (sUpgrades.Upgrade2 == 1)
            {
                ExplicarUp2.text = "Aumenta Dano em mais 2.";
            }
            else if (sUpgrades.Upgrade2 == 2)
            {
                ExplicarUp2.text = "Aumenta Dano em mais 3 e muda o dano para o tipo Energia.";
            }
            else if (sUpgrades.Upgrade2 >= 3)
            {
                ExplicarUp2.text = "Caminho Completo.";
            }

            //Caminho 3
            if (sUpgrades.Upgrade3 == 0)
            {
                ExplicarUp3.text = "Aumenta o Range em 50% e aumenta 1 de perfura��o de bala.";
            }
            else if (sUpgrades.Upgrade3 == 1)
            {
                ExplicarUp3.text = "Aumenta o Range em 20% e permite ver ocultos.";
            }
            else if (sUpgrades.Upgrade3 == 2)
            {
                ExplicarUp3.text = "Aumenta o Range em mais 40%, mais 1 de perfura��o e muda o dano pra tipo Sangue.";
            }
            else if (sUpgrades.Upgrade3 >= 3)
            {
                ExplicarUp3.text = "Caminho Completo.";
            }
        }

        //Explica��o Upgrades Dante
        if (agenteDante)
        {
            //Caminho 1
            if (sUpgrades.Upgrade1 == 0)
            {
                ExplicarUp1.text = "Aumenta sua Range em 2.";
            }
            else if (sUpgrades.Upgrade1 == 1)
            {
                ExplicarUp1.text = "Melhora a Range de todos em sua volta.";
            }
            else if (sUpgrades.Upgrade1 == 2)
            {
                ExplicarUp1.text = "Melhora a Cadencia de todos em sua volta.";
            }
            else if (sUpgrades.Upgrade1 >= 3)
            {
                ExplicarUp1.text = "Caminho Completo.";
            }

            //Caminho 2
            if (sUpgrades.Upgrade2 == 0)
            {
                ExplicarUp2.text = "Aumenta o Dano e Perfuca��o do ataque em 1.";
            }
            else if (sUpgrades.Upgrade2 == 1)
            {
                ExplicarUp2.text = "Aumenta o Dano e Perfuca��o do ataque em 2.";
            }
            else if (sUpgrades.Upgrade2 == 2)
            {
                ExplicarUp2.text = "Aumenta o Dano e Perfuca��o em 4 e muda de Morte para Energia.";
            }
            else if (sUpgrades.Upgrade2 >= 3)
            {
                ExplicarUp2.text = "Caminho Completo.";
            }

            //Caminho 3
            if (sUpgrades.Upgrade3 == 0)
            {
                ExplicarUp3.text = "Permite que dante possa ver Monstros Ocultos.";
            }
            else if (sUpgrades.Upgrade3 == 1)
            {
                ExplicarUp3.text = "Permite que outros agentes, no raio de Dante, possam acertar Monstros Ocultos";
            }
            else if (sUpgrades.Upgrade3 == 2)
            {
                ExplicarUp3.text = "Habilidade Paradiso: Ganha 5 de Vida e Sanidade Total quando ativa.";
            }
            else if (sUpgrades.Upgrade3 >= 3)
            {
                ExplicarUp3.text = "Caminho Completo.";
            }
        }

        //Explica��o Upgrades Samuel
        if (agenteSamuel)
        {
            //Caminho 1
            if (sUpgrades.Upgrade1 == 0)
            {
                ExplicarUp1.text = "Aumenta a quantia da produ��o de dinheiro.";
            }
            else if (sUpgrades.Upgrade1 == 1)
            {
                ExplicarUp1.text = "Aumenta mais a quantia da produ��o de dinheiro.";
            }
            else if (sUpgrades.Upgrade1 == 2)
            {
                ExplicarUp1.text = "Aumenta muito mais a quantia da produ��o de dinheiro.";
            }
            else if (sUpgrades.Upgrade1 >= 3)
            {
                ExplicarUp1.text = "Caminho Completo.";
            }

            //Caminho 2
            if (sUpgrades.Upgrade2 == 0)
            {
                ExplicarUp2.text = "Aumenta a produ��o por Round em 1.";
            }
            else if (sUpgrades.Upgrade2 == 1)
            {
                ExplicarUp2.text = "Aumenta a produ��o por Round em 1.";
            }
            else if (sUpgrades.Upgrade2 == 2)
            {
                ExplicarUp2.text = "Aumenta a produ��o por Round em 2.";
            }
            else if (sUpgrades.Upgrade2 >= 3)
            {
                ExplicarUp2.text = "Caminho Completo.";
            }

            //Caminho 3
            if (sUpgrades.Upgrade3 == 0)
            {
                ExplicarUp3.text = "Da desconto em novos Agentes.";
            }
            else if (sUpgrades.Upgrade3 == 1)
            {
                ExplicarUp3.text = "Da desconto em Upgrades de pr�ximos.";
            }
            else if (sUpgrades.Upgrade3 == 2)
            {
                ExplicarUp3.text = "Aumenta a Area em 20%, de novo.";
            }
            else if (sUpgrades.Upgrade3 >= 3)
            {
                ExplicarUp3.text = "Habilidade DaylightDeamon: Melhore um agente escolhido por 10 segundos.";
            }
        }

        //Explica��o Upgrades Balu
        if (agenteBalu)
        {
            //Caminho 1
            if (sUpgrades.Upgrade1 == 0)
            {
                ExplicarUp1.text = "Aumenta Cadencia dos Tiros.";
            }
            else if (sUpgrades.Upgrade1 == 1)
            {
                ExplicarUp1.text = "Aumenta mais Cadencia dos Tiros.";
            }
            else if (sUpgrades.Upgrade1 == 2)
            {
                ExplicarUp1.text = "Aumenta muito mais Cadencia dos Tiros.";
            }
            else if (sUpgrades.Upgrade1 >= 3)
            {
                ExplicarUp1.text = "Caminho Completo.";
            }

            //Caminho 2
            if (sUpgrades.Upgrade2 == 0)
            {
                ExplicarUp2.text = "Aumenta o Dano.";
            }
            else if (sUpgrades.Upgrade2 == 1)
            {
                ExplicarUp2.text = "Aumenta mais o Dano.";
            }
            else if (sUpgrades.Upgrade2 == 2)
            {
                ExplicarUp2.text = "Aumenta mais ainda o Dano.";
            }
            else if (sUpgrades.Upgrade2 >= 3)
            {
                ExplicarUp2.text = "Caminho Completo.";
            }

            //Caminho 3
            if (sUpgrades.Upgrade3 == 0)
            {
                ExplicarUp3.text = "Aumenta a Area em 20%.";
            }
            else if (sUpgrades.Upgrade3 == 1)
            {
                ExplicarUp3.text = "Aumenta a Area em mais 20%.";
            }
            else if (sUpgrades.Upgrade3 == 2)
            {
                ExplicarUp3.text = "Aumenta a Area em 20%, de novo.";
            }
            else if (sUpgrades.Upgrade3 >= 3)
            {
                ExplicarUp3.text = "Caminho Completo.";
            }
        }

        //Explica��o Upgrades Rubens
        if (agenteRubens)
        {
            //Caminho 1
            if (sUpgrades.Upgrade1 == 0)
            {
                ExplicarUp1.text = "Aumenta Vida e Sanidade por round em 1.";
            }
            else if (sUpgrades.Upgrade1 == 1)
            {
                ExplicarUp1.text = "Aumenta Vida e Sanidade por round em 2.";
            }
            else if (sUpgrades.Upgrade1 == 2)
            {
                ExplicarUp1.text = "Aumenta Vida e Sanidade por round em 3.";
            }
            else if (sUpgrades.Upgrade1 >= 3)
            {
                ExplicarUp1.text = "Caminho Completo.";
            }

            //Caminho 2
            if (sUpgrades.Upgrade2 == 0)
            {
                ExplicarUp2.text = "Se tem um Balu pr�ximo, Aumenta a Vida em 3 e Sanidade em 2 por round.";
            }
            else if (sUpgrades.Upgrade2 == 1)
            {
                ExplicarUp2.text = "Aumenta o Dano do Balu mais pr�ximo em 4.";
            }
            else if (sUpgrades.Upgrade2 == 2)
            {
                ExplicarUp2.text = "Habilidade Forma Fantasmag�rica: Permite teletransportar um agente pra outro lugardo mapa.";
            }
            else if (sUpgrades.Upgrade2 >= 3)
            {
                ExplicarUp2.text = "Caminho Completo.";
            }

            //Caminho 3
            if (sUpgrades.Upgrade3 == 0)
            {
                ExplicarUp3.text = "Aumenta o Dano em 1.";
            }
            else if (sUpgrades.Upgrade3 == 1)
            {
                ExplicarUp3.text = "Aumenta o Dano em 2.";
            }
            else if (sUpgrades.Upgrade3 == 2)
            {
                ExplicarUp3.text = "Aumenta o Dano em 2 e faz ganhar mais 2 de Sanidade por Round.";
            }
            else if (sUpgrades.Upgrade3 >= 3)
            {
                ExplicarUp3.text = "Caminho Completo.";
            }
        }

    }

    void OnMouseEnter()
    {
        mouseDentroDoObjeto = true;
    }
    void OnMouseExit()
    {
        mouseDentroDoObjeto = false;
    }

    public void MouseDentro()
    {
        mouseDentroDoObjeto = true;
    }

    public void MouseFora()
    {
        mouseDentroDoObjeto = false;
    }

}
