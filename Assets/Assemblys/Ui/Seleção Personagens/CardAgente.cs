using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAgente : MonoBehaviour
{
    [Header("CoisasJogo")]
    public int preçoCarta;
    public GameObject PrefabAgente;
    public string NomeAgente;

    [Header(" ")]

    public GameObject SeleçãoAgentes;
    public GameObject BarraAgentes;
    public SeleçãoAgentes SLscript;

    public float Tempo = 1f;
    public float Speed = 2f;
    public Color cor1;
    public Color cor2;
    public Color cor3;

    [Header(" ")]
    public Color cor4;

    public GameObject ImagemPersonagem;
    public Text PreçoCard;
    public bool naSeleção = true;
    public bool naBarra = false;
    public Transform cellAnimated;
    public Transform cellNABARRA;
    public bool anim1 = false;
    public bool anim2 = false;
    public bool anim3 = false;
    public bool ligado = true;
    public bool inGame = false;
    public bool travada = false;
    public GameObject origem;
    public GameObject animEmpty;
    RectTransform rt;
    RectTransform rt2;

    public Vector2 pos1;
    public Vector2 pos1Parent;

    [Header("Sistema")]
    public SeleçãoAgentes sA;
    public GameMaster gm;
    ComprarTorre cpt;
    public GameObject Options;

    Vector3 scale_inicial;
    public static GameObject[] botaons;


    private void Start()
    {
        gm = sA.gm;        
        rt = GetComponent<RectTransform>();
        PreçoCard.text = "$" + preçoCarta.ToString();
        scale_inicial = rt.localScale;        

    }

    private void OnMouseEnter()
    {
        if (travada)
        {
            return;
        }

        if (inGame)
            {
                gameObject.GetComponent<Image>().color = cor3;
                SLscript.HoverSom.PlayOneShot(SLscript.HoverSom.clip);
                return;
            }
        if (naSeleção)
        {
            gameObject.GetComponent<Image>().color = cor3;
            SLscript.HoverSom.PlayOneShot(SLscript.HoverSom.clip);
            return;
        }
        if (naBarra)
        {
            gameObject.GetComponent<Image>().color = cor3;
            SLscript.HoverSom.PlayOneShot(SLscript.HoverSom.clip);
        }        
    }

    private void OnMouseExit()
    {
        if (travada)
        {
            return;
        }

        if (inGame)
        {
            gameObject.GetComponent<Image>().color = cor1;
            return;
        }
        if (naSeleção)
        {
            gameObject.GetComponent<Image>().color = cor1;
        }
        if (naBarra)
        {
            gameObject.GetComponent<Image>().color = cor1;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (travada)
        {
            return;
        }
        if (inGame)
        {

            cpt.TorreAtiva();
            cpt.EscolherAgente(preçoCarta, gameObject, PrefabAgente);

            

            SLscript.ClickSom.PlayOneShot(SLscript.ClickSom.clip);
            return;
        }

        if (!ligado)
        {
            return;
        }

        
        if (naSeleção)
        {
            SpawnCard();
        }
        else if (!naSeleção || naBarra)
        {
            SLscript.ClickSom.PlayOneShot(SLscript.ClickSom.clip);
            DespawnCard();
        }
    }

    public void SpawnCard()
    {
        if (naBarra) { DespawnCard(); return; }

        if (BarraAgentes.transform.childCount < SLscript.nSlots)
        {
            SLscript.ClickSom.PlayOneShot(SLscript.ClickSom.clip);
            gameObject.GetComponent<Image>().color = cor1;
            cellNABARRA = GameObject.Instantiate(gameObject.transform, BarraAgentes.transform);
            cellNABARRA.GetComponent<CardAgente>().naSeleção = false;
            cellNABARRA.GetComponent<CardAgente>().naBarra = true;
            cellNABARRA.GetComponent<CardAgente>().origem = gameObject;
            cellNABARRA.GetComponent<CardAgente>().animEmpty = animEmpty;
            cellNABARRA.GetComponent<RectTransform>().localScale = scale_inicial;

            cellNABARRA.GetComponent<Image>().enabled = false;
            foreach (Transform item in cellNABARRA.GetComponentInChildren<Transform>())
            {
                if (item.GetComponent<Text>())
                {
                    item.GetComponent<Text>().enabled = false;
                }
                if (item.GetComponent<Image>())
                {
                    item.GetComponent<Image>().enabled = false;
                }
            }

            cellAnimated = GameObject.Instantiate(gameObject.transform, animEmpty.transform);
            cellAnimated.GetComponent<CardAgente>().ligado = false;
            rt2 = cellAnimated.GetComponent<RectTransform>();
            rt2.position = gameObject.GetComponent<RectTransform>().position;
            rt2.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;

            anim1 = true;
            naSeleção = false;
            gameObject.GetComponent<Image>().color = cor2;
        }
    }

    public void DespawnCard()
    {
        cellAnimated = GameObject.Instantiate(gameObject.transform, animEmpty.transform);
        cellAnimated.GetComponent<CardAgente>().ligado = false;
        cellAnimated.GetComponent<Image>().color = cor1;
        rt2 = cellAnimated.GetComponent<RectTransform>();
        rt2.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;

        if (cellNABARRA != null)
        {
            rt2.position = cellNABARRA.GetComponent<RectTransform>().position;

            anim2 = true;

            Options.GetComponent<BotãoBasico>().Botões.Remove(cellNABARRA.gameObject);
            Destroy(cellNABARRA.gameObject);
        }
        else if (naBarra)
        {
            CardAgente ca = origem.GetComponent<CardAgente>();
            rt2.position = gameObject.GetComponent<RectTransform>().position;

            ca.rt2 = cellAnimated.GetComponent<RectTransform>();
            ca.cellAnimated = cellAnimated;
            ca.anim3 = true;
            Options.GetComponent<BotãoBasico>().Botões.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public IEnumerator AnimaçãoCarta(int mov)
    {
        if (mov == 1)
        {
            ligado = false;
            Vector2 posicaoAbsoluta = cellNABARRA.GetComponent<RectTransform>().localPosition;
            Transform parent = cellNABARRA.parent;
            posicaoAbsoluta.x += parent.GetComponent<RectTransform>().localPosition.x;
            posicaoAbsoluta.y += parent.GetComponent<RectTransform>().localPosition.y;

            Vector2 selfposicaoAbsoluta = cellAnimated.GetComponent<RectTransform>().localPosition;

            if (Vector2.Distance(selfposicaoAbsoluta, posicaoAbsoluta) >= 10f)
            {
                rt2.localPosition = Vector2.Lerp(selfposicaoAbsoluta, posicaoAbsoluta, Tempo / 3);
                yield return new WaitForEndOfFrame();
            }


            cellNABARRA.GetComponent<Image>().enabled = true;
            foreach (Transform item in cellNABARRA.GetComponentInChildren<Transform>())
            {
                if (item.GetComponent<Text>())
                {
                    item.GetComponent<Text>().enabled = true;
                }
                if (item.GetComponent<Image>())
                {
                    item.GetComponent<Image>().enabled = true;
                }
            }

            Destroy(cellAnimated.gameObject);
            mov = 0;
            ligado = true;
        }

        else if (mov == 2)
        {
            ligado = false;
            Vector2 posicaoAbsoluta = gameObject.GetComponent<RectTransform>().localPosition;
            Transform parent = gameObject.transform.parent;
            posicaoAbsoluta.x += parent.GetComponent<RectTransform>().localPosition.x;
            posicaoAbsoluta.y += parent.GetComponent<RectTransform>().localPosition.y;

            Vector2 selfposicaoAbsoluta = cellAnimated.GetComponent<RectTransform>().localPosition;

            if (Vector2.Distance(selfposicaoAbsoluta, posicaoAbsoluta) >= 10f)
            {
                rt2.localPosition = Vector2.Lerp(selfposicaoAbsoluta, posicaoAbsoluta, Tempo);
                yield return new WaitForEndOfFrame();
            }

            naSeleção = true;
            gameObject.GetComponent<Image>().color = cor1;
            Destroy(cellAnimated.gameObject);
            mov = 0;
            ligado = true;
        }

        else if (mov == 3)
        {
            ligado = false;
            Vector2 posicaoAbsoluta = gameObject.GetComponent<RectTransform>().localPosition;
            Transform parent = gameObject.transform.parent;
            posicaoAbsoluta.x += parent.GetComponent<RectTransform>().localPosition.x;
            posicaoAbsoluta.y += parent.GetComponent<RectTransform>().localPosition.y;

            Vector2 selfposicaoAbsoluta = cellAnimated.GetComponent<RectTransform>().localPosition;

            if (Vector2.Distance(selfposicaoAbsoluta, posicaoAbsoluta) >= 10f)
            {
                rt2.localPosition = Vector2.Lerp(selfposicaoAbsoluta, posicaoAbsoluta, Tempo);
                yield return new WaitForEndOfFrame();
            }


            naSeleção = true;
            GetComponent<Image>().color = cor1;
            gameObject.GetComponent<Image>().color = cor1;
            Destroy(cellAnimated.gameObject);
            mov = 0;
            ligado = true;
        }

        else if (mov == 0)
        {
            yield break;
        }

        yield return new WaitForEndOfFrame();
        yield break;
    }

    private void Update()
    {

        if (!inGame)
        {
            return;
        }

        PreçoCard.text = "$" + preçoCarta.ToString();

        if (preçoCarta > gm.mm.ValorDinheiro)
        {
            GetComponent<Image>().color = cor4;
            travada = true;
        }
        else
        {
            GetComponent<Image>().color = cor1;
            travada = false;
        }


    }


}
