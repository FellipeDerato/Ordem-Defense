using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agente_Especifico : MonoBehaviour
{

    public enum agente_tipo
    {
        Arthur,
        Dante,
        Carina,
        Rubens,
        Balu,
        Samuel
    }
    public agente_tipo Agente;

    public enum agente_elemento
    {
        Fisico,
        Morte,
        Sangue,
        Conhecimento,
        Energia,
    }
    public agente_elemento Elemento;

    [Header("Base Agente")]

    public bool GeraDinheiro;
    public bool AtacaInimigos;
    public bool Ataque_Projetio;
    public bool Ataque_Melee;


    [Header("Attributos")]   

    public float RangePadrão = 5f;
    public float RangeModifier_Soma = 0;
    public float RangeModifier_Vezes = 1;
    public float RangeFinal = 0f;

    public float CadênciaPadrão = 1f;
    public float CadênciaModifier_Soma = 0;
    public float CadênciaModifier_Vezes = 1;
    public float CadênciaFinal = 0f;

    public float DanoPadrão = 1;
    public float DanoModifier_Soma = 0;
    public float DanoModifier_Vezes = 1;
    public float DanoFinal = 0;

    public float VelocidadeBala = 10f;
    public float RotaçãoBala = 10f;
    public float PierceBala = 1;
    public float TempoBala = 1.5f;
    public bool balaSegue = false;



    [Header("Dano aplicado (numero para a UI)")]

    public float DanoAplicado = 0;
    public float DanoAplicadoTotal = 0;

    [Header("Importante")]

    public int EscolhaDeTarget = 1;
    public float turnSpeed = 10f;
    public int ocultoAcerta = 0;
    public GameObject TargetAtualT1;

    [Header("Setup Unity")]

    private Animator animator;
    private Transform target;

    public bool atirando;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public GameObject ArmaMele;
    public Transform firePoint;
    public float AnimatorSpeed = 1;
    public GameObject AreaAtiravel;
    public Text AlvoTexto;

    [Header("Habilidades")]
    public float timerHabilidade1 = 0f;
    public float timerHabilidade2 = 0f;
    public bool prontaHabilidade1 = true;
    public bool prontaHabilidade2 = true;
    public float CooldownH_1 = 5f;
    public float CooldownH_2 = 5f;
    public Color H_nao;
    public Color H_sim;

    [Header("Som")]

    public AudioClip somTiro1;
    public AudioClip somTiro2;

    #region Upgrades

    [Header("Upgrades")]
    public int Upgrade1 = 0;
    public int Upgrade2 = 0;
    public int Upgrade3 = 0;
    public int upsComprados = 0;
    public int up1PreçoATUAL;
    public int up2PreçoATUAL;
    public int up3PreçoATUAL;

    [Space]

    public int DinheiroGasto;
    public int DinheiroDividido;
    public float Modifier_ValorVenda = 0.75f;

    [Header("Preços N�vel 1")]

    public int up1Preço1 = 200;
    public int up2Preço1 = 350;
    public int up3Preço1 = 150;

    [Header("Preços N�vel 2")]

    public int up1Preço2 = 100;
    public int up2Preço2 = 100;
    public int up3Preço2 = 100;

    [Header("Preços N�vel 3")]

    public int up1Preço3 = 100;
    public int up2Preço3 = 100;
    public int up3Preço3 = 100;


    [Header("Sprites Imagens")]
    public Sprite sprite1x1;
    public Sprite sprite1x2;
    public Sprite sprite1x3;

    public Sprite sprite2x1;
    public Sprite sprite2x2;
    public Sprite sprite2x3;

    public Sprite sprite3x1;
    public Sprite sprite3x2;
    public Sprite sprite3x3;

    public Sprite CaminhoCompleto;
    public Sprite CaminhoBloqueado;
    public Sprite Habilidade1Sprite;
    public Sprite Habilidade2Sprite;

    public Color VermelhoVar = new Color(215, 24, 22, 1);
    public Color VerdeVar = new Color(71, 227, 63, 1);
    public Color CinzaVar = new Color(119, 111, 130, 1);

    [Header("Explica��es Upgrades")]

    public string string1x1;
    public string string1x2;
    public string string1x3;

    public string string2x1;
    public string string2x2;
    public string string2x3;

    public string string3x1;
    public string string3x2;
    public string string3x3;


    #endregion



    ComprarTorre cpt;
    GameMaster gm;
    Mapa_Master mm;
    Agente_Base agtB;
    Agente_UI cell;
    Criatura_Base criaturaB;
    Agente_Bala agente_Bala;
    Agente_Arma_Melee agente_Melee;

    public MonoBehaviour classe_agente;


    #region Setup

    private void Start()
    {
        agtB = GetComponent<Agente_Base>();
        gm = agtB.gm;
        cpt = gm.cpt;
        mm = gm.mm;
        cell = agtB.cell;
        animator = GetComponent<Animator>();
        StartCoroutine(UpdateTarget());
        SetupAgente();
    }

    void SetupAgente()
    {
        up1PreçoATUAL = up1Preço1;
        up2PreçoATUAL = up2Preço1;
        up3PreçoATUAL = up3Preço1;

        cell.ExplicarUp1.text = string1x1;
        cell.ExplicarUp2.text = string2x1;
        cell.ExplicarUp3.text = string3x1;

        Agente_Escolhido();
    }

    void Agente_Escolhido()
    {
        if (Agente == agente_tipo.Arthur)
        {
            classe_agente = gameObject.AddComponent<Class_Arthur>();
        }
        if (Agente == agente_tipo.Dante)
        {
            classe_agente = gameObject.AddComponent<Class_Dante>();
        }
        if (Agente == agente_tipo.Carina)
        {
            classe_agente = gameObject.AddComponent<Class_Carina>();
        }
        if (Agente == agente_tipo.Rubens)
        {
            classe_agente = gameObject.AddComponent<Class_Rubens>();
        }
        if (Agente == agente_tipo.Balu)
        {
            classe_agente = gameObject.AddComponent<Class_Balu>();
        }
        if (Agente == agente_tipo.Samuel)
        {
            classe_agente = gameObject.AddComponent<Class_Samuel>();
        }
    }

    #endregion

    #region Upgrades

    public void Upgrade_Agente(int Caminho)
    {
        if (Caminho == 1)
        {
            if (cell.Botao1.GetComponent<Button>().enabled)
            {
                Upgrade1++;
                Caminho_1();
            }            
        }
        else if (Caminho == 2)
        {
            if (cell.Botao2.GetComponent<Button>().enabled)
            {
                Upgrade2++;
                Caminho_2();
            }            
        }
        else if (Caminho == 3)
        {
            if (cell.Botao3.GetComponent<Button>().enabled)
            {
                Upgrade3++;
                Caminho_3();
            }            
        }

        #region Fechar Upgrades por Limite de Compra

        //Bot�o 1
        if (upsComprados >= 2 && Upgrade1 == 0) 
        { 
            cell.Upgrade1Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao1.GetComponent<Image>().color = CinzaVar;
            cell.Botao1.GetComponent<Button>().enabled = false;
        }
        else if (Upgrade2 >= 3 && Upgrade1 >= 2) 
        { 
            cell.Upgrade1Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao1.GetComponent<Image>().color = CinzaVar;
            cell.Botao1.GetComponent<Button>().enabled = false;
        }
        else if (Upgrade3 >= 3 && Upgrade1 >= 2) 
        { 
            cell.Upgrade1Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao1.GetComponent<Image>().color = CinzaVar;
            cell.Botao1.GetComponent<Button>().enabled = false;
        }
        else
        {
            cell.Botao1.GetComponent<Button>().enabled = true;
        }

        //Bot�o 2
        if (upsComprados >= 2 && Upgrade2 == 0) 
        { 
            cell.Upgrade2Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao2.GetComponent<Image>().color = CinzaVar;
            cell.Botao2.GetComponent<Button>().enabled = false;
        }
        else if (Upgrade1 >= 3 && Upgrade2 >= 2) 
        { 
            cell.Upgrade2Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao2.GetComponent<Image>().color = CinzaVar;
            cell.Botao2.GetComponent<Button>().enabled = false;
        }
        else if (Upgrade3 >= 3 && Upgrade2 >= 2) 
        { 
            cell.Upgrade2Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao2.GetComponent<Image>().color = CinzaVar;
            cell.Botao2.GetComponent<Button>().enabled = false;
        }
        else
        {
            cell.Botao2.GetComponent<Button>().enabled = true;
        }

        //Bot�o 3
        if (upsComprados >= 2 && Upgrade3 == 0) 
        { 
            cell.Upgrade3Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao3.GetComponent<Image>().color = CinzaVar;
            cell.Botao3.GetComponent<Button>().enabled = false;
        }

        else if (Upgrade1 >= 3 && Upgrade3 >= 2) 
        { 
            cell.Upgrade3Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao3.GetComponent<Image>().color = CinzaVar;
            cell.Botao3.GetComponent<Button>().enabled = false;
        }
        else if (Upgrade2 >= 3 && Upgrade3 >= 2) 
        { 
            cell.Upgrade3Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; 
            cell.Botao3.GetComponent<Image>().color = CinzaVar;
            cell.Botao3.GetComponent<Button>().enabled = false;
        }
        else
        {
            cell.Botao3.GetComponent<Button>().enabled = true;
        }

        #endregion

    }

    void Caminho_1()
    {
        Image comprarS = cell.Upgrade1Comprar.GetComponent<Image>();
        Image atualS = cell.Upgrade1Atual.GetComponent<Image>();
        DinheiroGasto += up1PreçoATUAL;
        DinheiroDividido = Mathf.CeilToInt((float)(DinheiroGasto * Modifier_ValorVenda));
        cell.verderText.text = "Vender por $" + DinheiroDividido;
        gm.mm.ValorDinheiro -= up1PreçoATUAL;

        if (Upgrade1 == 0)
        {
            upsComprados++;
            comprarS.sprite = sprite1x2;
            atualS.sprite = sprite1x1;
            up1PreçoATUAL = up1Preço2;
            cell.ExplicarUp1.text = string1x2;
        }
        else if (Upgrade1 == 1)
        {
            comprarS.sprite = sprite1x3;
            atualS.sprite = sprite1x2;
            up1PreçoATUAL = up1Preço3;
            cell.ExplicarUp1.text = string1x3;
        }
        else if (Upgrade1 == 2)
        {
            comprarS.sprite = CaminhoCompleto;
            atualS.sprite = sprite1x3;
        }

        if (Upgrade1 < 3) { classe_agente.Invoke("Caminho_1", 0.01f); }
        Atualizar_Valores();

    }
    void Caminho_2()
    {
        Image comprarS = cell.Upgrade2Comprar.GetComponent<Image>();
        Image atualS = cell.Upgrade2Atual.GetComponent<Image>();
        DinheiroGasto += up2PreçoATUAL;
        DinheiroDividido = Mathf.CeilToInt((float)(DinheiroGasto * Modifier_ValorVenda));
        cell.verderText.text = "Vender por $" + DinheiroDividido;
        gm.mm.ValorDinheiro -= up2PreçoATUAL;

        if (Upgrade2 == 0)
        {
            upsComprados++;
            comprarS.sprite = sprite2x2;
            atualS.sprite = sprite2x1;
            up2PreçoATUAL = up2Preço2;
            cell.ExplicarUp2.text = string2x2;
        }
        else if (Upgrade2 == 1)
        {
            comprarS.sprite = sprite2x3;
            atualS.sprite = sprite2x2;
            up2PreçoATUAL = up2Preço3;
            cell.ExplicarUp2.text = string2x3;
        }
        else if (Upgrade2 == 2)
        {
            comprarS.sprite = CaminhoCompleto;
            atualS.sprite = sprite2x3;
        }

        if (Upgrade2 < 3) { classe_agente.Invoke("Caminho_2", 0.01f); }
        Atualizar_Valores();

    }
    void Caminho_3()
    {
        Image comprarS = cell.Upgrade3Comprar.GetComponent<Image>();
        Image atualS = cell.Upgrade3Atual.GetComponent<Image>();
        DinheiroGasto += up3PreçoATUAL;
        DinheiroDividido = Mathf.CeilToInt((float)(DinheiroGasto * Modifier_ValorVenda));
        cell.verderText.text = "Vender por $" + DinheiroDividido;
        gm.mm.ValorDinheiro -= up3PreçoATUAL;

        if (Upgrade3 == 0)
        {
            upsComprados++;
            comprarS.sprite = sprite3x2;
            atualS.sprite = sprite3x1;
            up3PreçoATUAL = up3Preço2;
            cell.ExplicarUp3.text = string3x2;
        }
        else if (Upgrade3 == 1)
        {
            comprarS.sprite = sprite3x3;
            atualS.sprite = sprite3x2;
            up3PreçoATUAL = up3Preço3;
            cell.ExplicarUp3.text = string3x3;
        }
        else if (Upgrade3 == 2)
        {
            comprarS.sprite = CaminhoCompleto;
            atualS.sprite = sprite3x3;
        }

        if (Upgrade3 < 3) { classe_agente.Invoke("Caminho_3", 0.01f); }
        Atualizar_Valores();

    }

    public void Atualizar_Valores()
    {
        RangeFinal = (RangePadrão + RangeModifier_Soma) * RangeModifier_Vezes;
        CadênciaFinal = (CadênciaPadrão + CadênciaModifier_Soma) * CadênciaModifier_Vezes;
        DanoFinal = (DanoPadrão + DanoModifier_Soma) * DanoModifier_Vezes;
        AreaAtiravel.transform.localScale = new Vector3(RangeFinal * 2f, AreaAtiravel.transform.localScale.y, RangeFinal * 2f);

        #region Fechar Upgrades pelo Preço e Dinheiro

        if (Upgrade1 < 3)
        {
            if (gm.mm.ValorDinheiro < up1PreçoATUAL)
            {
                cell.Botao1.GetComponent<Image>().color = VermelhoVar;
                cell.Botao1.GetComponent<Button>().enabled = false;
            }
            else
            {
                cell.Botao1.GetComponent<Image>().color = VerdeVar;
                cell.Botao1.GetComponent<Button>().enabled = true;
            }
            cell.up1TextPreço.text = "$" + up1PreçoATUAL.ToString();
        }
        else if (Upgrade1 >= 3)
        {
            cell.Botao1.GetComponent<Image>().color = CinzaVar;
            cell.Botao1.GetComponent<Button>().enabled = false;
            cell.up1TextPreço.text = " ";
        }

        if (Upgrade2 < 3)
        {
            if (gm.mm.ValorDinheiro < up2PreçoATUAL)
            {
                cell.Botao2.GetComponent<Image>().color = VermelhoVar;
                cell.Botao2.GetComponent<Button>().enabled = false;
            }
            else
            {
                cell.Botao2.GetComponent<Image>().color = VerdeVar;
                cell.Botao2.GetComponent<Button>().enabled = true;
            }
            cell.up2TextPreço.text = "$" + up2PreçoATUAL.ToString();
        }
        else if (Upgrade2 >= 3)
        {
            cell.Botao1.GetComponent<Image>().color = CinzaVar;
            cell.Botao1.GetComponent<Button>().enabled = false;
            cell.up2TextPreço.text = " ";
        }

        if (Upgrade3 < 3)
        {
            if (gm.mm.ValorDinheiro < up3PreçoATUAL)
            {
                cell.Botao3.GetComponent<Image>().color = VermelhoVar;
                cell.Botao3.GetComponent<Button>().enabled = false;
            }
            else
            {
                cell.Botao3.GetComponent<Image>().color = VerdeVar;
                cell.Botao3.GetComponent<Button>().enabled = true;
            }
            cell.up3TextPreço.text = "$" + up3PreçoATUAL.ToString();
        }
        else if (Upgrade1 >= 3)
        {
            cell.Botao3.GetComponent<Image>().color = CinzaVar;
            cell.Botao3.GetComponent<Button>().enabled = false;
            cell.up3TextPreço.text = " ";
        }



        #endregion

    }

    #endregion

    #region Ataque

    public void Ataque(int ação)
    {
        if (target != null)

        if (Ataque_Projetio)
        {
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            agente_Bala = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Agente_Bala>();
            agente_Bala.TargetAtual = TargetAtualT1;
            agente_Bala.Cerne = gameObject;
            agente_Bala.pierce = PierceBala;
            agente_Bala.gm = gm;
            agente_Bala.GetComponent<Rigidbody>().linearVelocity = agente_Bala.transform.forward * VelocidadeBala;
            agtB.audiosource1.PlayOneShot(somTiro1);
            if (balaSegue) { agente_Bala.Seek(target); }
        }

        if (Ataque_Melee)
        {
            agente_Melee = ArmaMele.GetComponent<Agente_Arma_Melee>();

            if (ação == 1)
            {
                Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * turnSpeed).eulerAngles;
                partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
            else if (ação == 2)
            {
                agente_Melee.ZonaDeDano = true;
            }
            else if (ação == 3)
            {
                agente_Melee.ZonaDeDano = false;
            }
        }
    }

    public IEnumerator Update_Ataque()
    {
        if (gm.mm.L_Criaturas.Count > 0)
        {
            foreach (GameObject criatura in gm.mm.L_Criaturas)
            {
                if (criatura == null)
                {
                    target = null;
                    animator.SetInteger("Anim", 0);
                    atirando = false;
                    yield return new WaitForEndOfFrame();
                }
                criaturaB = criatura.GetComponent<Criatura_Base>();
                float faerstDistance = 0f;
                float shortestDistance = Mathf.Infinity;
                float maiorVida = 0;
                float disPerPrimeiro = 0f;
                float disPerUltimo = Mathf.Infinity;
                float distanceToCriatura = Vector3.Distance(transform.position, criatura.transform.position);


                //Inimigo Primeiro
                if (EscolhaDeTarget == 1)
                {
                    if (distanceToCriatura <= RangeFinal)
                    {
                        if (criaturaB.distanciaPercorrida > disPerPrimeiro)
                        {
                            if (ocultoAcerta >= 1)
                            {
                                shortestDistance = distanceToCriatura;
                                disPerPrimeiro = criaturaB.distanciaPercorrida;
                                TargetAtualT1 = criatura;
                            }
                            else
                            {
                                if (!criaturaB.Oculto)
                                {
                                    shortestDistance = distanceToCriatura;
                                    disPerPrimeiro = criaturaB.distanciaPercorrida;
                                    TargetAtualT1 = criatura;
                                }
                            }
                        }

                    }


                    if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetInteger("Anim", 1);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetInteger("Anim", 0);
                        atirando = false;
                    }
                }

                //Inimigo Ultimo
                else if (EscolhaDeTarget == 2)
                {
                    if (distanceToCriatura <= RangeFinal)
                    {
                        if (criaturaB.distanciaPercorrida < disPerUltimo)
                        {
                            if (ocultoAcerta >= 1)
                            {
                                shortestDistance = distanceToCriatura;
                                disPerUltimo = criaturaB.distanciaPercorrida;
                                TargetAtualT1 = criatura;
                            }
                            else
                            {
                                if (!criaturaB.Oculto)
                                {
                                    shortestDistance = distanceToCriatura;
                                    disPerUltimo = criaturaB.distanciaPercorrida;
                                    TargetAtualT1 = criatura;
                                }
                            }
                        }

                    }


                    if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetInteger("Anim", 1);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetInteger("Anim", 0);
                        atirando = false;
                    }
                }

                //Inimigo mais forte
                else if (EscolhaDeTarget == 3)
                {

                    if (distanceToCriatura < shortestDistance)
                    {
                        if (distanceToCriatura <= RangeFinal)
                        {
                            if (criaturaB.HpMonstro >= maiorVida)
                            {
                                if (ocultoAcerta >= 1)
                                {
                                    shortestDistance = distanceToCriatura;
                                    maiorVida = criaturaB.HpMonstro;
                                    TargetAtualT1 = criatura;
                                }
                                else
                                {
                                    if (!criaturaB.Oculto)
                                    {
                                        shortestDistance = distanceToCriatura;
                                        maiorVida = criaturaB.HpMonstro;
                                        TargetAtualT1 = criatura;
                                    }
                                }
                            }
                        }
                    }


                    if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetInteger("Anim", 1);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetInteger("Anim", 0);
                        atirando = false;
                    }
                }

                //Inimigo mais perto
                else if (EscolhaDeTarget == 4)
                {
                    if (distanceToCriatura < shortestDistance)
                    {
                        if (ocultoAcerta >= 1)
                        {
                            shortestDistance = distanceToCriatura;
                            TargetAtualT1 = criatura;
                        }
                        else
                        {
                            if (!criaturaB.Oculto)
                            {
                                shortestDistance = distanceToCriatura;
                                TargetAtualT1 = criatura;
                            }
                        }

                    }


                    if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetInteger("Anim", 1);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetInteger("Anim", 0);
                        atirando = false;
                    }
                }

                //Inimigo mais Longe
                else if (EscolhaDeTarget == 5)
                {
                    if (distanceToCriatura > faerstDistance)
                    {
                        if (distanceToCriatura <= RangeFinal)
                        {
                            if (ocultoAcerta >= 1)
                            {
                                faerstDistance = distanceToCriatura;
                                TargetAtualT1 = criatura;
                            }
                            else
                            {
                                if (!criaturaB.Oculto)
                                {
                                    faerstDistance = distanceToCriatura;
                                    TargetAtualT1 = criatura;
                                }
                            }
                        }
                    }


                    if (TargetAtualT1 != null && faerstDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetInteger("Anim", 1);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetInteger("Anim", 0);
                        atirando = false;
                    }
                }

            }
        }
        else
        {
            target = null;
            animator.SetInteger("Anim", 0);
            atirando = false;
        }



        Ataque(1);
        yield return new WaitForSeconds(1f / (CadênciaPadrão + CadênciaModifier_Soma) * CadênciaModifier_Vezes);
    }

    public IEnumerator UpdateTarget()
    {
        if (gm.mm.L_Criaturas.Count > 0)
        {
            foreach (GameObject enemy in gm.mm.L_Criaturas)
            {
                if (enemy == null)
                {
                    target = null;
                    animator.SetBool("AtirandoPistola", false);
                    atirando = false;
                    yield return new WaitForEndOfFrame();
                }
                criaturaB = enemy.GetComponent<Criatura_Base>();
                float faerstDistance = 0f;
                float shortestDistance = Mathf.Infinity;
                float maiorVida = 0;
                float disPerPrimeiro = 0f;
                float disPerUltimo = Mathf.Infinity;
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);


                //Inimigo Primeiro
                if (EscolhaDeTarget == 1)
                {
                    if (distanceToEnemy <= RangeFinal)
                    {
                        if (criaturaB.distanciaPercorrida > disPerPrimeiro)
                        {
                            if (ocultoAcerta >= 1)
                            {
                                shortestDistance = distanceToEnemy;
                                disPerPrimeiro = criaturaB.distanciaPercorrida;
                                TargetAtualT1 = enemy;
                            }
                            else
                            {
                                if (!criaturaB.Oculto)
                                {
                                    shortestDistance = distanceToEnemy;
                                    disPerPrimeiro = criaturaB.distanciaPercorrida;
                                    TargetAtualT1 = enemy;
                                }
                            }
                        }

                    }


                    if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetBool("AtirandoPistola", true);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetBool("AtirandoPistola", false);
                        atirando = false;
                    }
                }

                //Inimigo Ultimo
                else if (EscolhaDeTarget == 2)
                {
                    if (distanceToEnemy <= RangeFinal)
                    {
                        if (criaturaB.distanciaPercorrida < disPerUltimo)
                        {
                            if (ocultoAcerta >= 1)
                            {
                                shortestDistance = distanceToEnemy;
                                disPerUltimo = criaturaB.distanciaPercorrida;
                                TargetAtualT1 = enemy;
                            }
                            else
                            {
                                if (!criaturaB.Oculto)
                                {
                                    shortestDistance = distanceToEnemy;
                                    disPerUltimo = criaturaB.distanciaPercorrida;
                                    TargetAtualT1 = enemy;
                                }
                            }
                        }

                    }


                    if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetBool("AtirandoPistola", true);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetBool("AtirandoPistola", false);
                        atirando = false;
                    }
                }

                //Inimigo mais forte
                else if (EscolhaDeTarget == 3)
                {

                    if (distanceToEnemy < shortestDistance)
                    {
                        if (distanceToEnemy <= RangeFinal)
                        {
                            if (criaturaB.HpMonstro >= maiorVida)
                            {
                                if (ocultoAcerta >= 1)
                                {
                                    shortestDistance = distanceToEnemy;
                                    maiorVida = criaturaB.HpMonstro;
                                    TargetAtualT1 = enemy;
                                }
                                else
                                {
                                    if (!criaturaB.Oculto)
                                    {
                                        shortestDistance = distanceToEnemy;
                                        maiorVida = criaturaB.HpMonstro;
                                        TargetAtualT1 = enemy;
                                    }
                                }
                            }
                        }
                    }


                    if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetBool("AtirandoPistola", true);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetBool("AtirandoPistola", false);
                        atirando = false;
                    }
                }

                //Inimigo mais perto
                else if (EscolhaDeTarget == 4)
                {
                    if (distanceToEnemy < shortestDistance)
                    {
                        if (ocultoAcerta >= 1)
                        {
                            shortestDistance = distanceToEnemy;
                            TargetAtualT1 = enemy;
                        }
                        else
                        {
                            if (!criaturaB.Oculto)
                            {
                                shortestDistance = distanceToEnemy;
                                TargetAtualT1 = enemy;
                            }
                        }

                    }


                    if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetBool("AtirandoPistola", true);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetBool("AtirandoPistola", false);
                        atirando = false;
                    }
                }

                //Inimigo mais Longe
                else if (EscolhaDeTarget == 5)
                {
                    if (distanceToEnemy > faerstDistance)
                    {
                        if (distanceToEnemy <= RangeFinal)
                        {
                            if (ocultoAcerta >= 1)
                            {
                                faerstDistance = distanceToEnemy;
                                TargetAtualT1 = enemy;
                            }
                            else
                            {
                                if (!criaturaB.Oculto)
                                {
                                    faerstDistance = distanceToEnemy;
                                    TargetAtualT1 = enemy;
                                }
                            }
                        }
                    }


                    if (TargetAtualT1 != null && faerstDistance <= RangeFinal)
                    {
                        target = TargetAtualT1.transform;
                        animator.SetBool("AtirandoPistola", true);
                        atirando = true;
                    }
                    else
                    {
                        target = null;
                        animator.SetBool("AtirandoPistola", false);
                        atirando = false;
                    }
                }

            }
        }
        else
        {
            target = null;
            animator.SetBool("AtirandoPistola", false);
            atirando = false;
        }
        yield return new WaitForEndOfFrame();
    }

    #endregion    

}

#region Classes dos Agentes Especificos

public class Class_Arthur : MonoBehaviour
{
    GameMaster gm;
    Agente_Base agtB;
    Agente_Especifico agtE;

    private void Start()
    {
        agtB = GetComponent<Agente_Base>();
        agtE = GetComponent<Agente_Especifico>();
        gm = agtB.gm;
    }

    public void Caminho_1()
    {
        if (agtE.Upgrade1 == 0)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 1)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 2)
        {
            agtE.CadênciaModifier_Soma += 3;
        }
    }

    public void Caminho_2()
    {
        if (agtE.Upgrade2 == 0)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 1)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 2)
        {
            agtE.DanoModifier_Soma += 4;
        }
    }

    public void Caminho_3()
    {
        if (agtE.Upgrade3 == 0)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 1;
        }
        else if (agtE.Upgrade3 == 1)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 2;
            agtE.ocultoAcerta += 1;
        }
        else if (agtE.Upgrade3 == 2)
        {
            agtE.RangeModifier_Soma += 4;
            agtE.PierceBala += 3;
            agtE.VelocidadeBala += 10f;
            agtE.DanoModifier_Soma += 2;
        }
    }
}

public class Class_Dante : MonoBehaviour
{
    GameMaster gm;
    Agente_Base agtB;
    Agente_Especifico agtE;

    private void Start()
    {
        agtB = GetComponent<Agente_Base>();
        agtE = GetComponent<Agente_Especifico>();
        gm = agtB.gm;
    }

    public void Caminho_1()
    {
        if (agtE.Upgrade1 == 0)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 1)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 2)
        {
            agtE.CadênciaModifier_Soma += 3;
        }
    }

    public void Caminho_2()
    {
        if (agtE.Upgrade2 == 0)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 1)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 2)
        {
            agtE.DanoModifier_Soma += 4;
        }
    }

    public void Caminho_3()
    {
        if (agtE.Upgrade3 == 0)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 1;
        }
        else if (agtE.Upgrade3 == 1)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 2;
            agtE.ocultoAcerta += 1;
        }
        else if (agtE.Upgrade3 == 2)
        {
            agtE.RangeModifier_Soma += 4;
            agtE.PierceBala += 3;
            agtE.VelocidadeBala += 10f;
            agtE.DanoModifier_Soma += 2;
        }
    }
}

public class Class_Carina : MonoBehaviour
{
    GameMaster gm;
    Agente_Base agtB;
    Agente_Especifico agtE;

    private void Start()
    {
        agtB = GetComponent<Agente_Base>();
        agtE = GetComponent<Agente_Especifico>();
        gm = agtB.gm;
    }

    public void Caminho_1()
    {
        if (agtE.Upgrade1 == 0)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 1)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 2)
        {
            agtE.CadênciaModifier_Soma += 3;
        }
    }

    public void Caminho_2()
    {
        if (agtE.Upgrade2 == 0)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 1)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 2)
        {
            agtE.DanoModifier_Soma += 4;
        }
    }

    public void Caminho_3()
    {
        if (agtE.Upgrade3 == 0)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 1;
        }
        else if (agtE.Upgrade3 == 1)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 2;
            agtE.ocultoAcerta += 1;
        }
        else if (agtE.Upgrade3 == 2)
        {
            agtE.RangeModifier_Soma += 4;
            agtE.PierceBala += 3;
            agtE.VelocidadeBala += 10f;
            agtE.DanoModifier_Soma += 2;
        }
    }
}

public class Class_Rubens : MonoBehaviour
{
    GameMaster gm;
    Agente_Base agtB;
    Agente_Especifico agtE;

    private void Start()
    {
        agtB = GetComponent<Agente_Base>();
        agtE = GetComponent<Agente_Especifico>();
        gm = agtB.gm;
    }

    public void Caminho_1()
    {
        if (agtE.Upgrade1 == 0)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 1)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 2)
        {
            agtE.CadênciaModifier_Soma += 3;
        }
    }

    public void Caminho_2()
    {
        if (agtE.Upgrade2 == 0)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 1)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 2)
        {
            agtE.DanoModifier_Soma += 4;
        }
    }

    public void Caminho_3()
    {
        if (agtE.Upgrade3 == 0)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 1;
        }
        else if (agtE.Upgrade3 == 1)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 2;
            agtE.ocultoAcerta += 1;
        }
        else if (agtE.Upgrade3 == 2)
        {
            agtE.RangeModifier_Soma += 4;
            agtE.PierceBala += 3;
            agtE.VelocidadeBala += 10f;
            agtE.DanoModifier_Soma += 2;
        }
    }
}

public class Class_Balu : MonoBehaviour
{
    GameMaster gm;
    Agente_Base agtB;
    Agente_Especifico agtE;

    private void Start()
    {
        agtB = GetComponent<Agente_Base>();
        agtE = GetComponent<Agente_Especifico>();
        gm = agtB.gm;
    }

    public void Caminho_1()
    {
        if (agtE.Upgrade1 == 0)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 1)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 2)
        {
            agtE.CadênciaModifier_Soma += 3;
        }
    }

    public void Caminho_2()
    {
        if (agtE.Upgrade2 == 0)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 1)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 2)
        {
            agtE.DanoModifier_Soma += 4;
        }
    }

    public void Caminho_3()
    {
        if (agtE.Upgrade3 == 0)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 1;
        }
        else if (agtE.Upgrade3 == 1)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 2;
            agtE.ocultoAcerta += 1;
        }
        else if (agtE.Upgrade3 == 2)
        {
            agtE.RangeModifier_Soma += 4;
            agtE.PierceBala += 3;
            agtE.VelocidadeBala += 10f;
            agtE.DanoModifier_Soma += 2;
        }
    }
}

public class Class_Samuel : MonoBehaviour
{
    GameMaster gm;
    Agente_Base agtB;
    Agente_Especifico agtE;

    private void Start()
    {
        agtB = GetComponent<Agente_Base>();
        agtE = GetComponent<Agente_Especifico>();
        gm = agtB.gm;
    }

    public void Caminho_1()
    {
        if (agtE.Upgrade1 == 0)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 1)
        {
            agtE.CadênciaModifier_Soma += 1;
        }
        else if (agtE.Upgrade1 == 2)
        {
            agtE.CadênciaModifier_Soma += 3;
        }
    }

    public void Caminho_2()
    {
        if (agtE.Upgrade2 == 0)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 1)
        {
            agtE.DanoModifier_Soma += 2;
        }
        else if (agtE.Upgrade2 == 2)
        {
            agtE.DanoModifier_Soma += 4;
        }
    }

    public void Caminho_3()
    {
        if (agtE.Upgrade3 == 0)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 1;
        }
        else if (agtE.Upgrade3 == 1)
        {
            agtE.RangeModifier_Soma += 2;
            agtE.PierceBala += 2;
            agtE.ocultoAcerta += 1;
        }
        else if (agtE.Upgrade3 == 2)
        {
            agtE.RangeModifier_Soma += 4;
            agtE.PierceBala += 3;
            agtE.VelocidadeBala += 10f;
            agtE.DanoModifier_Soma += 2;
        }
    }
}



#endregion