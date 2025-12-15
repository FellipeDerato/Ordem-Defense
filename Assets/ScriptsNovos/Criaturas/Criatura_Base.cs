using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Criatura_Base : MonoBehaviour
{

    public GameMaster gm;
    public Mapa_Master mm;

    [Header("Valores")]
    public float Velocidade_Padrão = 2;
    public float VelocidadeModifier_Soma = 0;
    public float VelocidadeModifier_Vezes = 1;
    public float dintanciaPercorrida;

    public enum criatura_elemento
    { 
        Sangue, Conhecimento, Energia, Morte, Medo 
    }
    public criatura_elemento Elemento;

    public enum criatura_tipo
    {
        ExistidoNormal,
        ExistidoAtivo,
        ExistidoMorte,
        ZumbiBestial,
        ZumbiDeSangue,
        Degolificada,
        Carniçal,
        Mumia,
        Anarquico,
        AnarquicoDescontrolado,
        PerturbadoDeEnergia,
        Viajante,
        Vulto
    }
    public criatura_tipo Criatura;

    [Header("Anima��o e Movimento")]

    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public Animator animator;
    public bool andando;
    public string AnimInicial = "Anim1";
    public GameObject EfeitoMorte;


    public float distanciaPercorrida;
    public bool Oculto;
    public float HpMonstro;

    void Start()
    {
        pathCreator = mm.posSpawn.parent.GetComponent<PathCreator>();
        if (GetComponent<Animator>())
        {
        animator = GetComponent<Animator>();
        animator.Play(AnimInicial, -1, Random.Range(0.0f, 1.0f));
        }
        else
        {
            AndarSwitch(2);
        }
        //Anim2;
        //Anim3;
    }



    #region Movimento Criatura
    public void AndarSwitch(int Ligada)
    {
        if (Ligada == 1) { StartCoroutine(Movimento(false)); }
        if (Ligada == 2) { StartCoroutine(Movimento(true)); }
    }

    public IEnumerator Movimento(bool Ligada)
    {
        andando = Ligada;
        while (true)
        {
            if (pathCreator != null)
            {
                if (!andando) 
                {
                    break;
                }

                if (andando)
                {
                    distanciaPercorrida += ((Velocidade_Padrão + VelocidadeModifier_Soma) * VelocidadeModifier_Vezes) * Time.deltaTime;
                    transform.position = pathCreator.path.GetPointAtDistance(distanciaPercorrida, endOfPathInstruction);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanciaPercorrida, endOfPathInstruction);
                    yield return new WaitForEndOfFrame();
                }

            }
        }

    }
    #endregion

    #region Final Criatura
    public void CriaturaMorta()
    {
        GameObject effectIns = (GameObject)Instantiate(mm.Efeito_MorteSangue, transform.position, transform.rotation);
        effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
        Destroy(effectIns, 1f);
        gm.mm.L_Criaturas.Remove(gameObject);
        Destroy(gameObject);
    }

    public void CriaturaPosFinal()
    {
        mm.ValorVida -= (int)Mathf.Ceil(HpMonstro);
        mm.ValorSanidade -= (int)Mathf.Ceil(HpMonstro / 2);
        mm.L_Criaturas.Remove(gameObject);
        mm.AtualizarUI();
        Destroy(gameObject);
    }
    #endregion

}
