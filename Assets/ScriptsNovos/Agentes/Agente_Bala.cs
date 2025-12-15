using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente_Bala : MonoBehaviour
{
    public float pierce = 1;
    public float DebuffDano = 1;
    public float multplicadorDbf = 0.15f;
    public float danoTotal;
    Criatura_Base criatura_Base;
    Rigidbody rigido;
    public Mapa_Master mm;
    public GameMaster gm;
    public Agente_Base agtB;
    public Agente_Especifico agtE;

    public List<GameObject> Inimigos;
    public Transform transformAcerto;
    public GameObject impactEffect;
    public GameObject Cerne;
    public List<GameObject> criaturas_1;
    public List<GameObject> criaturas_2;

    [Header("Seek")]
    private Transform target;
    public GameObject TargetAtual;


    private void Start()
    {
        Destroy(gameObject, agtE.TempoBala);
        if (agtE.balaSegue)
        {
            InvokeRepeating("UpdateTarget", 0f, 0.1f);
            foreach (GameObject enemy in gm.mm.L_Criaturas.ToArray())
            {
                criaturas_1.Add(enemy);
                criaturas_2.Add(enemy);
            }
        }

    }
    public void Seek(Transform _target)
    {
        target = _target;
    }




    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("ParedeBala"))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }



        if (col.gameObject.CompareTag("Criatura_Base"))
        {
            Criatura_Base criatura = col.gameObject.GetComponent<Criatura_Base>();

            //Oculto
            if (criatura.Oculto)
            {
                if (agtE.ocultoAcerta >= 1)
                {
                    pierce -= 1;
                    Inimigos.Add(criatura.gameObject);  //adicionar na lista
                    transformAcerto = criatura.gameObject.GetComponent<Transform>();
                    if (pierce <= 0) { Destroy(gameObject); }
                    if (agtE.balaSegue) { criaturas_2.Remove(criatura.gameObject); }
                    HitTarget();
                }
            }
            else
            {
                pierce -= 1;
                Inimigos.Add(criatura.gameObject);  //adicionar na lista
                transformAcerto = criatura.gameObject.GetComponent<Transform>();
                if (pierce <= 0) { Destroy(gameObject); }
                if (agtE.balaSegue) { criaturas_2.Remove(criatura.gameObject); }
                HitTarget();
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void UpdateTarget()
    {
        if (agtE.balaSegue)
        {
            if (target == null)
            {
                if (criaturas_1 != null)
                {
                    float shortestDistance = Mathf.Infinity;
                    foreach (GameObject criatura1 in criaturas_1)
                    {
                        if (criatura1 != null)
                        {
                            if (criatura1.GetComponent<Enemy>().Oculto)
                            {
                                if (agtE.ocultoAcerta >= 1)
                                {
                                    if (criaturas_2.Contains(criatura1))
                                    {
                                        float distanceToEnemy = Vector3.Distance(transform.position, criatura1.transform.position);
                                        if (distanceToEnemy < shortestDistance)
                                        {
                                            shortestDistance = distanceToEnemy;
                                            target = criatura1.transform;
                                        }

                                    }
                                    return;
                                }
                                else
                                {
                                    Destroy(gameObject);
                                    return;
                                }
                            }
                        }

                        if (criaturas_2.Contains(criatura1))
                        {
                            if (criatura1 != null)
                            {
                                float distanceToEnemy = Vector3.Distance(transform.position, criatura1.transform.position);
                                if (distanceToEnemy < shortestDistance)
                                {
                                    shortestDistance = distanceToEnemy;
                                    target = criatura1.transform;
                                }
                            }
                        }
                    }

                }
                else
                {
                    Destroy(gameObject);
                }
                return;
            }
        }
    }

    private void Update()
    {
        //ARRUMAR ISSO!!!!!!!! dante

        if (agtE.balaSegue)
        {
            if (target == null)
            {
                return;
                //transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            }
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = agtE.VelocidadeBala * Time.deltaTime;
            if (dir.magnitude <= distanceThisFrame)
            {
                return;
            }
            ///if (agtE.agenteErin) { dir = new Vector3(dir.x, dir.y, dir.z); } else { dir = new Vector3(dir.x, 0, dir.z); }
            gameObject.GetComponent<Rigidbody>().linearVelocity = transform.forward * agtE.VelocidadeBala;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * agtE.RotaçãoBala);
        }
    }

    void HitTarget()
    {
        if (Inimigos != null)
        {

            foreach (GameObject inimigo in Inimigos)
            {
                criatura_Base = inimigo.GetComponent<Criatura_Base>();


                //Buffs pelo Elemento                
                if (agtE.Elemento == Agente_Especifico.agente_elemento.Sangue)
                {
                    if (TargetAtual)
                    {
                        if (criatura_Base.Elemento == Criatura_Base.criatura_elemento.Conhecimento)
                        {
                            DebuffDano += multplicadorDbf;
                        }
                        if (criatura_Base.Elemento == Criatura_Base.criatura_elemento.Morte)
                        {
                            DebuffDano -= multplicadorDbf;
                        }
                    }
                }

                if (agtE.Elemento == Agente_Especifico.agente_elemento.Conhecimento)
                {
                    if (TargetAtual)
                    {
                        if (criatura_Base.Elemento == Criatura_Base.criatura_elemento.Energia)
                        {
                            DebuffDano += multplicadorDbf;
                        }
                        if (criatura_Base.Elemento == Criatura_Base.criatura_elemento.Sangue)
                        {
                            DebuffDano -= multplicadorDbf;
                        }
                    }
                }

                if (agtE.Elemento == Agente_Especifico.agente_elemento.Energia)
                {
                    if (TargetAtual)
                    {
                        if (criatura_Base.Elemento == Criatura_Base.criatura_elemento.Morte)
                        {
                            DebuffDano += multplicadorDbf;
                        }
                        if (criatura_Base.Elemento == Criatura_Base.criatura_elemento.Conhecimento)
                        {
                            DebuffDano -= multplicadorDbf;
                        }
                    }
                }

                if (agtE.Elemento == Agente_Especifico.agente_elemento.Morte)
                {
                    if (TargetAtual)
                    {
                        if (criatura_Base.Elemento == Criatura_Base.criatura_elemento.Sangue)
                        {
                            DebuffDano += multplicadorDbf;
                        }
                        if (criatura_Base.Elemento == Criatura_Base.criatura_elemento.Energia)
                        {
                            DebuffDano -= multplicadorDbf;
                        }
                    }
                }

                danoTotal = Mathf.Ceil(agtE.DanoFinal * DebuffDano);


                //Dano igual o DanoAplicado() do cerne
                if (criatura_Base.HpMonstro < danoTotal)
                {
                    agtE.DanoAplicado = criatura_Base.HpMonstro;
                }
                else
                {
                    agtE.DanoAplicado = danoTotal;
                }

                if ((agtE.DanoFinal + DebuffDano) > 0)
                {
                    criatura_Base.HpMonstro -= danoTotal;
                    gm.mm.ValorDinheiro += (int)Mathf.Ceil(agtE.DanoAplicado);
                    agtE.DanoAplicadoTotal += agtE.DanoAplicado;

                    GameObject effectIns = (GameObject)Instantiate(mm.Efeito_SangueImpacto, inimigo.transform.position, inimigo.transform.rotation);
                    Destroy(effectIns, 1f);
                }
            }
            Inimigos.Clear();
        }

        if (agtE.balaSegue)
        {
            foreach (GameObject item in gm.mm.L_Criaturas.ToArray())
            {
                criaturas_1.Add(item);
            }
            target = null;
        }
        gm.mm.AtualizarUI();
    }
}