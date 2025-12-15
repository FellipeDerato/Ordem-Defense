using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GizmoMouse : MonoBehaviour
{
    public GameMaster gm;

    MeshCollider mesh;

    public BoxCollider AreaVisívelCollider;
    public GameObject AreaVisívelGameObject;

    public BoxCollider[] coliders; 

    public GameObject GizmoMouseAtivo;
    public Camera cam;
    public Vector3 worldPosition;
    Ray ray;
    public AudioSource somComprar;
    ComprarTorre comprarTorre;
    public Color CorVerde;
    public Color CorVermelha;
    public GameObject AgentePrefab;
    public GameObject cardOrigem;
    GameObject agenteP = null;
    bool cliqueAtivo = false;
    public float Range;
    CapsuleCollider[] colliders = null;

    void Start()
    {
        mesh = GetComponent<MeshCollider>();
        comprarTorre = GetComponent<ComprarTorre>();
        cam = Camera.main;
        

    }

    public IEnumerator ClicouBotao()
    {
        yield return 2f;
        agenteP = (GameObject)Instantiate(AgentePrefab, new Vector3(worldPosition.x, worldPosition.y, worldPosition.z), AgentePrefab.transform.rotation);
        Range = agenteP.GetComponentInChildren<Agente_Especifico>().RangePadrão * 2;
        GizmoMouseAtivo.transform.localScale = new Vector3(Range, GizmoMouseAtivo.transform.localScale.y, Range);

        colliders = agenteP.GetComponentsInChildren<CapsuleCollider>();
        foreach (CapsuleCollider collider in colliders)
        {
            collider.enabled = false;
        }
        agenteP.GetComponentInChildren<Agente_Base>().cardOrigem = cardOrigem.GetComponent<CardAgente>();
        agenteP.GetComponentInChildren<Agente_Base>().enabled = false;
        agenteP.GetComponentInChildren<Agente_Especifico>().enabled = false;
        agenteP.GetComponentInChildren<Agente_Base>().gm = gm;
        cliqueAtivo = true;
    }

    public void ClicouErrado()
    {
        cliqueAtivo = false;
        GizmoMouseAtivo.SetActive(false);
        AgentePrefab = null;
        colliders = null;
        Destroy(agenteP);
    }

    public void ClicouCerto()
    {
        cliqueAtivo = false;
        GizmoMouseAtivo.SetActive(false);
        AgentePrefab = null;

        colliders = agenteP.GetComponentsInChildren<CapsuleCollider>();
        foreach (CapsuleCollider collider in colliders)
        {
            collider.enabled = true;
        }
        colliders = null;
        agenteP.GetComponentInChildren<Agente_Base>().enabled = true;
        agenteP.GetComponentInChildren<Agente_Especifico>().enabled = true;

        agenteP = null;
        somComprar.GetComponent<AudioSource>().pitch = 1;
        AudioSource fx = somComprar.GetComponent<AudioSource>(); fx.PlayOneShot(fx.clip);
    }

    void Update()
    {
        ray = cameraFix.cameraPrincipal.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (gm.AreaVisivel.GetComponent<TerrainCollider>().Raycast(ray, out hitData, 1000))
        {
            worldPosition = hitData.point;
        }



        if (cliqueAtivo)
        {
            GizmoMouseAtivo.SetActive(true);
            GizmoMouseAtivo.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
            agenteP.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
            



            if (comprarTorre.AreaPossivelAtiva)
            {
                if (GizmoMouseAtivo.GetComponent<Renderer>().material.color != CorVerde)
                {
                    GizmoMouseAtivo.GetComponent<Renderer>().material.color = CorVerde;
                }
                
            }
            else
            {
                if (GizmoMouseAtivo.GetComponent<Renderer>().material.color != CorVermelha)
                {
                    GizmoMouseAtivo.GetComponent<Renderer>().material.color = CorVermelha;
                }
            }

            


        }    
        else
        {
            GizmoMouseAtivo.SetActive(false);
        }


    }
}