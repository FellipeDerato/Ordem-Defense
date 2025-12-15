using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente_Arma_Melee : MonoBehaviour
{
    Enemy nme;
    public GameObject Cerne;
    GameMaster gm;
    Agente_Especifico agtE;
    Agente_Base agtB;
    Agente_UI agtU;
    public bool ZonaDeDano;
    public Transform transformAcerto;
    public GameObject impactEffect;
    public List<GameObject> Inimigos;
    public AudioSource BatidaSom;
    public int DebuffDano = 0;

    private void Start()
    {
        agtE = Cerne.GetComponent<Agente_Especifico>();
        agtB = Cerne.GetComponent<Agente_Base>();
        gm = agtB.gm;
    }

    void OnTriggerEnter(Collider col)
    {
        if (agtE.atirando)
        {
            if (ZonaDeDano)
            {
                if (col.gameObject.CompareTag("Enemy"))
                {
                    //col.gameObject.GetComponent<Enemy>().HpMonstro -= Cerne.GetComponent<Torre1>().DanoInfligido;
                    Inimigos.Add(col.gameObject);  //adicionar na lista
                    transformAcerto = col.gameObject.GetComponent<Transform>();
                    HitTarget();
                }
            }
        }
    }

    public void SwitchZonaDeDano()
    {
        if (!ZonaDeDano)
        {
            ZonaDeDano = true;
            return;
        }
        else
        {
            ZonaDeDano = false;
        }
    }


    void HitTarget()
    {
        if (Inimigos != null)
        {
            BatidaSom.Play();
            foreach (GameObject inimigo in Inimigos)
            {
                nme = inimigo.GetComponent<Enemy>();

                if (agtE.Elemento == Agente_Especifico.agente_elemento.Sangue)
                {
                    if (nme.ConhecimentoE)
                    {
                        DebuffDano += 1;
                    }
                    if (nme.MorteE)
                    {
                        DebuffDano -= 1;
                    }
                }

                if (agtE.Elemento == Agente_Especifico.agente_elemento.Conhecimento)
                {
                    if (nme.EnergiaE)
                    {
                        DebuffDano += 1;
                    }
                    if (nme.SangueE)
                    {
                        DebuffDano -= 1;
                    }
                }

                if (agtE.Elemento == Agente_Especifico.agente_elemento.Energia)
                {
                    if (nme.MorteE)
                    {
                        DebuffDano += 1;
                    }
                    if (nme.ConhecimentoE)
                    {
                        DebuffDano -= 1;
                    }
                }

                if (agtE.Elemento == Agente_Especifico.agente_elemento.Morte)
                {
                    if (nme.SangueE)
                    {
                        DebuffDano += 1;
                    }
                    if (nme.EnergiaE)
                    {
                        DebuffDano -= 1;
                    }
                }


                //Dano igual o DanoAplicado() do cerne
                if (nme.HpMonstro < agtE.DanoFinal + DebuffDano)
                {
                    agtE.DanoAplicado = (int)nme.HpMonstro;
                }
                else
                {
                    agtE.DanoAplicado = agtE.DanoFinal + DebuffDano;
                }
                nme.HpMonstro -= agtE.DanoFinal + DebuffDano;
                gm.mm.ValorDinheiro += (int)Mathf.Ceil(agtE.DanoAplicado);
                agtE.DanoAplicadoTotal += agtE.DanoAplicado;

                GameObject effectIns = (GameObject)Instantiate(impactEffect, inimigo.transform.position, inimigo.transform.rotation);
                Destroy(effectIns, 1f);
            }
            Inimigos.Clear();
        }
    }
}
