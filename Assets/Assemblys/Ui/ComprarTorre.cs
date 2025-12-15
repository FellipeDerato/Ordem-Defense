using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class ComprarTorre : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject Final;
    public GameObject ArthurP = null;
    public GameObject DanteP = null;
    public GameObject RubensP = null;
    public GameObject BaluP = null;
    public GameObject SamuelP = null;
    public GameObject CarinaP = null;

    [Header("Sons")]
    public AudioSource somComprar;
    public AudioSource somVender;

    [Header("Sistema")]
    public GameMaster gm;
    public GameObject Alvo;
    public bool AreaPossivelAtiva;
    public int intAreaPossivel = 0;
    public GameObject[] AgentesArray;
    public bool ClicadoPersonagem = false;
    public Camera cam = null;
    public bool clicouTorre = false;
    GizmoMouse gizmo;
    public GameObject prefabSelecionado = null;
    int preçoTower;

    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0);



    void Start()
    {
        cam = Camera.main;
        gizmo = GetComponent<GizmoMouse>();
    }


    private void Awake()
    {

    }

    private void OnMouseEnter()
    {
        intAreaPossivel++;
    }

    private void OnMouseExit()
    {
        intAreaPossivel--;
    }

    private void Update()
    {

        if (intAreaPossivel >= 1)
        {
            AreaPossivelAtiva = true;
        }
        else
        {
            AreaPossivelAtiva = false;
        }


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (clicouTorre)
            {                
                if (AreaPossivelAtiva)
                {
                    gizmo.ClicouCerto();
                    clicouTorre = false;
                    gm.mm.ValorDinheiro -= preçoTower;
                    gm.mm.AtualizarUI();
                }
                else
                {
                    gizmo.ClicouErrado();
                    clicouTorre = false;
                }
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (clicouTorre)
            {
                gizmo.ClicouErrado();
                clicouTorre = false;
            }            
        }



        



        //PARA TESTES - REMOVER DEPOIS
        if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
        {
            gm.mm.ValorDinheiro += 50;
            gm.mm.AtualizarUI();

        }
        if (Keyboard.current.numpadMinusKey.wasPressedThisFrame)
        {
            gm.mm.ValorDinheiro -= 50;
            gm.mm.AtualizarUI();
        }
    }


    public void TorreAtiva()
    {
        clicouTorre = true;
        StartCoroutine(gizmo.ClicouBotao());
    }

    public void TorreDesativa()
    {
        clicouTorre = false;
    }

    public void EscolherAgente(int preçoAgente, GameObject cardOrigem, GameObject PrefabAgente)
    {
        if (clicouTorre)
        {            
            preçoTower = preçoAgente;
            gizmo.AgentePrefab = PrefabAgente;
            gizmo.cardOrigem = cardOrigem;            
            prefabSelecionado = null;
        }
    
    }
}