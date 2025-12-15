using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Mapa_Master : MonoBehaviour
{
    public GameMaster gm;
    Mapa_UI mu;

    public Transform posSpawn;
    public Transform posFinal;

    [Header("Prefabs")]

    public Transform ZumbiDeSangue;
    public Transform Degolificada;
    public Transform Carniçal;
    public Transform ZumbiBestial;
    public Transform Existido;
    public Transform ExistidoMorte;
    public Transform Mumia;
    public Transform Anarquico;
    public Transform AnarquicoDescontrolado;
    public Transform PerturbadosDeEnergia;
    public Transform Viajante;
    public Transform Vulto;

    [Header("Prefabs Efeitos")]

    public GameObject Efeito_SangueImpacto;
    public GameObject Efeito_Incendiado;
    public GameObject Efeito_Maldito;
    public GameObject Efeito_MorteSangue;
    public GameObject Efeito_Oculto;

    [Header("Entidades")]

    public List<Agente_Base> L_Agentes;
    public List<GameObject> L_Criaturas;

    [Header("Round")]

    public bool AutoStart;
    public bool RoundRolando = false;
    bool SpaawnInicial = false;
    bool PassouRound = false;
    public bool VeloDobrada = false;

    [Header("Valores")]

    public int ValorRound;
    public int ValorRound_Mínimo;
    public int ValorDinheiro;
    public int ValorSanidade;
    public int ValorVida;

    public int DinheiroPorRound;
    public int PenalidadeSanidade;
    float countdownSanidade;

    [Header("Sistema")]
    bool Morreu;

    private void Start()
    {
        mu = gm.mu;
    }

    private void Update()
    {

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Spawnar(ZumbiDeSangue);
        }

        #region Se tem Criaturas vivas
        if (L_Criaturas.Count <= 0)
        {
            RoundRolando = false;
        }
        else
        {
            RoundRolando = true;
        }
        #endregion


        // Come�o de Round
        if (!SpaawnInicial && !RoundRolando && PassouRound)
        {
            if (AutoStart)
            {
                ValorDinheiro += DinheiroPorRound;
                ValorRound++;
                StartCoroutine(ComeçarRound());
            }
            else
            {
                if (ValorRound != 1)
                {
                    gm.mu.Botão_Principal.GetComponentInChildren<Text>().text = "Iniciar";
                    ValorDinheiro += DinheiroPorRound;
                    ValorRound++;
                }
            }

            PassouRound = false;

        }
        
        // Se morrer.
        if (!Morreu)
        {
            // Sanidade Chega a 0
            if (ValorSanidade <= 0)
            {
                if (countdownSanidade <= 0)
                {
                    ValorVida -= PenalidadeSanidade;
                    countdownSanidade = 3f;
                }
                countdownSanidade -= Time.deltaTime;
            }


            // Vida chegou a 0 e morreu
            if (ValorVida <= 0)
            {
                foreach (GameObject Criatura in L_Criaturas)
                {
                    Criatura.GetComponent<Criatura_Base>().CriaturaMorta();
                    Morreu = true;
                }
                StopAllCoroutines();
            }
        }        
    }


    public void BotãoPrincipal()
    {
        if (AutoStart)
        {
            if (SpaawnInicial)
            {
                if (RoundRolando)
                {
                    Vocidade();
                    return;
                }
            }
            else
            {
                if (!RoundRolando)
                {
                    StartCoroutine(ComeçarRound());
                }
            }
        }
        if (!SpaawnInicial)
        {
            if (!RoundRolando)
            {
                gm.mu.Botão_Principal.GetComponentInChildren<Text>().text = "X 1";
                StartCoroutine(ComeçarRound());
                return;
            }
        }
        Vocidade();
    }

    public void Vocidade()
    {
        if (VeloDobrada)
        {
            Time.timeScale = 2;
            VeloDobrada = false;
            gm.mu.Botão_Principal.GetComponentInChildren<Text>().text = "X 2";
        }
        else if (!VeloDobrada)
        {
            Time.timeScale = 1;
            VeloDobrada = true;
            gm.mu.Botão_Principal.GetComponentInChildren<Text>().text = "X 1";
        }
    }


    public void AtualizarUI()
    {
        mu.ValorDinheiro.GetComponent<Text>().text = ValorDinheiro.ToString();
        mu.ValorRound.GetComponent<Text>().text = ValorRound.ToString() + "/" + ValorRound_Mínimo.ToString();
        mu.ValorSanidade.GetComponent<Text>().text = ValorDinheiro.ToString();
        mu.ValorVida.GetComponent<Text>().text = ValorVida.ToString();
    }


    public IEnumerator ComeçarRound()
    {
        SpaawnInicial = true;

        //Rounds Normais
        #region

        if (ValorRound == 1)
        {
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
            Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(.1f);
        }

        #endregion



        yield return new WaitForSeconds(1f);
        PassouRound = true;
    }


    public void Spawnar(Transform prefab)
    {
        GameObject Criatura = Instantiate(prefab.gameObject, posSpawn.position, posSpawn.rotation);
        Criatura.GetComponent<Transform>().parent = posSpawn;
        L_Criaturas.Add(Criatura);
        Criatura.GetComponent<Criatura_Base>().mm = GetComponent<Mapa_Master>();
        Criatura.GetComponent<Criatura_Base>().gm = gm;
    }


}
