using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agente_UI : MonoBehaviour
{
    [Header("Botoes Upgrades")]
    public GameObject Botao1;
    public GameObject Botao2;
    public GameObject Botao3;

    [Header("Imagem Comprar")]
    public GameObject Upgrade1Comprar;
    public GameObject Upgrade2Comprar;
    public GameObject Upgrade3Comprar;

    [Header("Imagem Atual")]
    public GameObject Upgrade1Atual;
    public GameObject Upgrade2Atual;
    public GameObject Upgrade3Atual;

    [Header("Textos Preços")]
    public Text up1TextPreço;
    public Text up2TextPreço;
    public Text up3TextPreço;

    [Header("Explica��o Ups")]
    public Text DanoCausado;
    public Text ExplicarUp1;
    public Text ExplicarUp2;
    public Text ExplicarUp3;
    public Text verderText;
    public Text AlvoTexto;

    [Header("Sons")]
    public AudioSource UpgradeSom;
    public AudioSource VenderSom;
    public AudioSource CliqueSimplesSom;
    public AudioSource FecharMenuSom;

    [Header("Quadradinhos")]
    public GameObject q11;
    public GameObject q12;
    public GameObject q13;

    public GameObject q21;
    public GameObject q22;
    public GameObject q23;

    public GameObject q31;
    public GameObject q32;
    public GameObject q33;

    [Header("Habilidades")]
    public GameObject Habilidade1;
    public GameObject Habilidade2;
    public GameObject Habilidade1Sprite;
    public GameObject Habilidade2Sprite;
    public Color H_nao;
    public Color H_sim;
    public GameObject txtH_1;
    public GameObject txtH_2;
    public bool mouseDentroDoObjeto;


    public Color CorNormal = new Color(55, 55, 55);
    public Color CorComprou = new Color(180, 180, 180);



    GameObject Cerne;
    ScriptUpgrades UpScript;
    Torre1 torre1;
    MenuUpgd menuU;
    public Animator animator;

}
