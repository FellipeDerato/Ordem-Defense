using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CelularS : MonoBehaviour
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

    private void Start()
    {
        UpScript = GetComponentInParent<ScriptUpgrades>();
        menuU = GetComponentInParent<MenuUpgd>();
        torre1 = GetComponentInParent<Torre1>();

        UpScript.cell = gameObject.GetComponent<CelularS>();
        UpScript.Celular = gameObject;
        UpScript.UpSom = UpgradeSom;


        Upgrade1Comprar.GetComponent<Image>().sprite = UpScript.sprite1x1;
        Upgrade2Comprar.GetComponent<Image>().sprite = UpScript.sprite2x1;
        Upgrade3Comprar.GetComponent<Image>().sprite = UpScript.sprite3x1;


        //ARRUMAR
        //gameObject.transform.chield.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

        gameObject.GetComponentInChildren<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        gameObject.GetComponentInChildren<Canvas>().worldCamera = menuU.Camera;
        gameObject.GetComponentInChildren<Canvas>().planeDistance = 0.5f;


        menuU.Menue = gameObject;
        menuU.VenderSomCell = VenderSom;
        menuU.CliqueSomCell = CliqueSimplesSom;
        menuU.FecharSomCell = FecharMenuSom;
        menuU.DanoCausado = DanoCausado;
        menuU.ExplicarUp1 = ExplicarUp1;
        menuU.ExplicarUp2 = ExplicarUp2;
        menuU.ExplicarUp3 = ExplicarUp3;
        menuU.animatorMenu = gameObject.GetComponent<Animator>();

        torre1.Celular = gameObject;
        torre1.AlvoTexto = AlvoTexto;
        torre1.H_nao = H_nao;
        torre1.H_sim = H_sim;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        //Quadradinhos do 1
        if (UpScript.Upgrade1 >= 1)
        {
            q11.GetComponent<Image>().color = CorComprou;
        }
        if (UpScript.Upgrade1 >= 2)
        {
            q12.GetComponent<Image>().color = CorComprou;
        }
        if (UpScript.Upgrade1 >= 3)
        {
            q13.GetComponent<Image>().color = CorComprou;
        }

        //Quadradinhos do 2
        if (UpScript.Upgrade2 >= 1)
        {
            q21.GetComponent<Image>().color = CorComprou;
        }
        if (UpScript.Upgrade2 >= 2)
        {
            q22.GetComponent<Image>().color = CorComprou;
        }
        if (UpScript.Upgrade2 >= 3)
        {
            q23.GetComponent<Image>().color = CorComprou;
        }

        //Quadradinhos do 3
        if (UpScript.Upgrade3 >= 1)
        {
            q31.GetComponent<Image>().color = CorComprou;
        }
        if (UpScript.Upgrade3 >= 2)
        {
            q32.GetComponent<Image>().color = CorComprou;
        }
        if (UpScript.Upgrade3 >= 3)
        {
            q33.GetComponent<Image>().color = CorComprou;
        }
    }

    public void BotaoUP(int i)
    {
        if (i == 1)
        {
            UpScript.BotaoUP1();
        }
        else if (i== 2)
        {
            UpScript.BotaoUP2();
        }
        else if (i == 3)
        {
            UpScript.BotaoUP3();
        }
    }


    public void MouseDentro()
    {
        menuU.MouseDentro();
    }
    public void MouseFora()
    {
        menuU.MouseFora();
    }

    public void AbrirMenuUpg()
    {
        //menuU.AbrirMenuUpg();
    }

    public void FecharMenuUpg()
    {
        //menuU.FecharMenuUpg();
    }

    public void VenderAgente()
    {
        //menuU.VenderAgente();
    }

    public void ChangeTarget()
    {
        torre1.ChangeTarget();
    }

    public void _Habilidade1()
    {
        torre1.Habilidade1();
    }

    public void _Habilidade2()
    {
        torre1.Habilidade2();
    }

}
