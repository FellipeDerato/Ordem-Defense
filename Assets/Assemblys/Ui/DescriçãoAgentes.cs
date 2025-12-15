using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriçãoAgentes : MonoBehaviour
{
    public GameObject ExplicaçãoPagina;
    public GameObject BotãoDescrição;
    public GameObject SeleçãoDesc;

    [Header("Descrições")]
    public GameObject DescArthur;
    public GameObject DescSamuel;
    public GameObject DescRubens;
    public GameObject DescDante;
    public GameObject DescCarina;
    public GameObject DescBalu;
    public GameObject[] ArrayDesc;


    private void Start()
    {
        DescArthur.SetActive(false);
        DescSamuel.SetActive(false);
        DescRubens.SetActive(false);
        DescDante.SetActive(false);
        DescCarina.SetActive(false);
        DescBalu.SetActive(false);
        ExplicaçãoPagina.SetActive(false);
    }
    private void Update()
    {
        ArrayDesc = GameObject.FindGameObjectsWithTag("Descrição");
    }

    public void AtivaDescArthur()
    {
        SeleçãoDesc = DescArthur;
        DescArthur.SetActive(true);
    }

    public void AtivaDescSamuel()
    {
        SeleçãoDesc = DescSamuel;
        DescSamuel.SetActive(true);
    }

    public void AtivaDescRubens()
    {
        SeleçãoDesc = DescRubens;
        DescRubens.SetActive(true);
    }

    public void AtivaDescDante()
    {
        SeleçãoDesc = DescDante;
        DescDante.SetActive(true);
    }

    public void AtivaDescCarina()
    {
        SeleçãoDesc = DescCarina;
        DescCarina.SetActive(true);
    }

    public void AtivaDescBalu()
    {
        SeleçãoDesc = DescBalu;
        DescBalu.SetActive(true);
    }

    public void ExplicaçãoAtiva()
    {
        ExplicaçãoPagina.SetActive(true);
        ExplicaçãoPagina.GetComponent<Animator>().Play("Scale 0 1");
    }

    public void ATIVAMASTER()
    {
        foreach (GameObject Desc in ArrayDesc)
        {
            if (SeleçãoDesc != Desc)
            {
                if (Desc.activeSelf == true)
                {
                    Desc.GetComponent<Animator>().Play("Scale 1 0");
                }
            }
        }
    }







    public void ExpliçãoDesativa()
    {
        ExplicaçãoPagina.GetComponent<Animator>().Play("Scale 1 0");
        DescArthur.SetActive(false);
        DescSamuel.SetActive(false);
        DescRubens.SetActive(false);
        DescDante.SetActive(false);
        DescCarina.SetActive(false);
        DescBalu.SetActive(false);
    }






}
