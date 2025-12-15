using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class SeleçãoAgentes : MonoBehaviour
{
    [Header("Tela de Seleção")]
    public GameObject SeleçãoPersonagens;
    public GameObject BotãoComeçar;

    public GameObject GrupoSeleção;


    [Header(" ")]
    public GameObject barraPersonagens;
    public GameObject BotãoPrincipal;

    [Header("Sistema")]
    public GameMaster gm;
    bool anim1 = false;
    bool anim2 = false;
    public Mapa_UI mu;
    public GameObject cardPrefab;
    public int nSlots = 7;
    public bool seleçãoAtiva;
    Vector2 naTela = new Vector2(0, 0);
    Vector2 foraTela = new Vector3(-800, 0);
    public float Tempo;
    public GameObject BotãoOptions;
    public GameObject FecharOptions1;
    public GameObject FecharOptions2;
    public GameObject FecharOptions3;

    [Header("Sons")]
    public AudioSource HoverSom;
    public AudioSource ClickSom;


    private void Start()
    {
        gm = mu.gm;
        BotãoPrincipal = mu.Botão_Principal;

        BotãoPrincipal.GetComponent<Button>().enabled = false;
        BotãoPrincipal.GetComponentInChildren<Text>().text = "";
        StartCoroutine(Start2());

    }

    IEnumerator Start2()
    {
        yield return new WaitForSeconds(.5f);
        EscolhaAtiva();
    }

    public void CardsJogo(bool Estado)
    {
        foreach (CardAgente card in barraPersonagens.GetComponentsInChildren<CardAgente>())
        {
            if (Estado)
            {
                card.inGame = false;
            }
            else
            {
                card.inGame = true;
            }
        }
    }

    public void EscolhaAtiva()
    {
        GrupoSeleção.SetActive(true);
        StartCoroutine(gm.mu.TelaPretaFundo(true));
        StartCoroutine(AnimaçãoSeleção(true));

        BotãoPrincipal.GetComponentInChildren<Text>().text = "";
        BotãoPrincipal.GetComponent<Button>().enabled = false;
        if (gm.mm.VeloDobrada) { gm.mm.VeloDobrada = false; }
        Time.timeScale = 1;
    }

    public void EscolhaDesativa()
    {
        CardsJogo(false);
        StartCoroutine(gm.mu.TelaPretaFundo(false));
        StartCoroutine(AnimaçãoSeleção(false));

    }

    public void BotãoAutoStart()
    {
        if (gm.mm.AutoStart)
        {
            gm.mm.AutoStart = true;
        }
        else
        {
            gm.mm.AutoStart = false;
        }
    }

    public void BotãoPrincipal_Sistema()
    {
        gm.mm.BotãoPrincipal();
    }


    public IEnumerator AnimaçãoSeleção(bool Ligada)
    {
        if (anim1)
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (!anim1) { break; }
            }
        }
        if (anim2)
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (!anim2) { break; }
            }
        }

        if (Ligada)
        {
            RectTransform rt = GrupoSeleção.GetComponent<RectTransform>();

            while (true)
            {
                if (Vector2.Distance(rt.localPosition, naTela) >= 5f)
                {
                    anim1 = true;
                    rt.localPosition = Vector2.Lerp(rt.localPosition, naTela, Tempo * 1.5f);
                    yield return new WaitForEndOfFrame();
                }
                else { break; }
            }
            anim1 = false;
            rt.localPosition = naTela;
            seleçãoAtiva = true;
            CardsJogo(true);
        }
        else if (!Ligada)
        {
            RectTransform rt = GrupoSeleção.GetComponent<RectTransform>();

            while (true)
            {
                if (Vector2.Distance(rt.localPosition, foraTela) >= 20f)
                {
                    anim2 = true;
                    rt.localPosition = Vector2.Lerp(rt.localPosition, foraTela, Tempo);
                    yield return new WaitForEndOfFrame();
                }
                else { break; }
            }
            anim2 = false;
            seleçãoAtiva = false;
            BotãoPrincipal.GetComponentInChildren<Text>().text = "Iniciar";
            BotãoPrincipal.GetComponent<Button>().enabled = true;
            CardsJogo(false);
            GrupoSeleção.SetActive(false);
        }
               
    }

    

}
