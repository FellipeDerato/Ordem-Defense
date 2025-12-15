using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class AnimaçõesBasicas_UI : MonoBehaviour
{
    
    public bool in_anim_Zoom;
    public bool in_anim_Giro;
    public bool in_anim_Opacidade;

    public bool out_anim_Zoom;
    public bool out_anim_Giro;
    public bool out_anim_Opacidade;

    public bool mid_anim_Shake;
    public bool mid_anim_Pulse;
    public bool mid_anim_Wobble;

    public bool hover_anim_Shake;
    public bool hover_anim_Zoom;
    public bool hover_anim_Wobble;

    public bool click_anim_Shake;
    public bool click_anim_Zoom;



    public float VelocidadeAnimação_In_Out = 5;
    public float VelocidadeAnimação_Mid = 5;
    public float ForçaHover = 5;
    public float ForçaClick = 5;

    public bool MidAnim;
    public bool HoverAnim;
    public bool ClickAnim;
    Vector3 TamanhoInicial;
    Vector3 PosiçãoInicial;
    Quaternion RotaçãoInicial;

    RectTransform rt;

    private void OnEnable()
    {
        Ativar();
    }

    public void Ativar()
    {
        rt = GetComponent<RectTransform>();
        PosiçãoInicial = rt.localPosition;

        if (!in_anim_Giro) { RotaçãoInicial = rt.localRotation; }
        if (!in_anim_Zoom) { TamanhoInicial = rt.localScale; }

        if (hover_anim_Shake || hover_anim_Wobble || hover_anim_Zoom)
        {
            if (!GetComponent<BoxCollider>())
            {
                BoxCollider bc = gameObject.AddComponent<BoxCollider>();
                bc.size = new Vector3(rt.rect.width, rt.rect.height, 1);
            }
        }
        InAnimation();
    }

    public void Desativar()
    {
        MidAnim = false;
        OutAnimation();
    }

    private void OnMouseEnter()
    {
        if (hover_anim_Shake || hover_anim_Wobble || hover_anim_Zoom) { HoverAnimation(); }
    }
    private void OnMouseExit()
    {

        HoverAnim = false;
    }

    private void OnMouseUpAsButton()
    {
        ClickAnim = true;
    }


    #region Hover Animations

    public void HoverAnimation()
    {
        HoverAnim = true;
        
        if (hover_anim_Shake)
        {
            StartCoroutine(Hover_Shake());
        }

        if (hover_anim_Zoom)
        {
            StartCoroutine(Hover_Zoom());
        }

        if (hover_anim_Wobble)
        {
            StartCoroutine(Hover_Wobble());
        }
    }

    public IEnumerator Hover_Shake()
    {
        while (HoverAnim)
        {


            yield return null;
        }
    }

    public IEnumerator Hover_Zoom()
    {
        rt.localScale = new Vector3(1, 1, 1);
        Vector3 escalaDesejada1 = new Vector3(1.2f, 1.2f, 1.2f);
        Vector3 escalaDesejada2 = new Vector3(1f, 1f, 1f);
        while (HoverAnim)
        {
            if (rt.localScale.x < escalaDesejada1.x)
            {
                rt.localScale = Vector3.Slerp(rt.localScale, escalaDesejada1, ForçaHover * Time.deltaTime);
                yield return null;
            }
        }
        while (!HoverAnim)
        {
            if (rt.localScale.x > escalaDesejada2.x + .01f)
            {
                rt.localScale = Vector3.Slerp(rt.localScale, escalaDesejada2, ForçaHover * Time.deltaTime);
                yield return null; ;
            }
            else
            {
                rt.localScale = escalaDesejada2;
                break;
            }

        }

        yield return null;
    }

    public IEnumerator Hover_Wobble()
    {
        while (HoverAnim)
        {


            yield return null;
        }
    }


    #endregion



    #region In Animations

    public void InAnimation()
    {
        gameObject.SetActive(true);
        if (GetComponent<Text>())
        {
        Text tx = GetComponent<Text>();
        tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, 1);
        }
        if (GetComponent<Image>())
        {
            Image im = GetComponent<Image>();
            im.color = new Color(im.color.r, im.color.g, im.color.b, 1);
        }
        rt.localRotation = new Quaternion(0, 0, 0, 1);
        rt.localScale = new Vector3(1, 1, 1);

        if (in_anim_Zoom)
        {
            StartCoroutine(In_Zoom());
        }

        if (in_anim_Giro)
        {
            StartCoroutine(In_Giro());
        }

        if (in_anim_Opacidade)
        {
            StartCoroutine(In_Opacidade());
        }

    }

    public IEnumerator In_Zoom()
    {
        rt.localScale = new Vector3(0, 0, 0);
        Vector3 escalaDesejada = new Vector3(1, 1, 1);
        while (true)
        {
            if (rt.localScale.x < escalaDesejada.x - 0.05f)
            {
                rt.localScale = Vector3.Slerp(rt.localScale, escalaDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                rt.localScale = escalaDesejada;
                TamanhoInicial = new Vector3(1, 1, 1);

                MidAnimation();
                yield break;
            }
        }
    }

    public IEnumerator In_Giro()
    {
        rt.localRotation = new Quaternion(0, 0, 1, 0);
        Quaternion rotDesejada = new Quaternion(0, 0, 0, 1);
        while (true)
        {
            if (rt.localRotation.z > 0.05f)
            {
                rt.localRotation = Quaternion.Slerp(rt.localRotation, rotDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                rt.localRotation = rotDesejada;
                RotaçãoInicial = rt.localRotation;
                MidAnimation();
                yield break;
            }
        }
    }

    public IEnumerator In_Opacidade()
    {
        if (GetComponent<Image>())
        {
            Image im = GetComponent<Image>();
            im.color = new Color(im.color.r, im.color.g, im.color.b, 0);
            Color corDesejada = new Color(im.color.r, im.color.g, im.color.b, 1);

            while (true)
            {
                if (im.color.a < corDesejada.a - 0.05f)
                {
                    im.color = Color.Lerp(im.color, corDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    MidAnimation();
                    im.color = corDesejada;
                    yield break;
                }
            }
        }
        if (GetComponent<Text>())
        {
            Text tx = GetComponent<Text>();
            tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, 0);
            Color corDesejada = new Color(tx.color.r, tx.color.g, tx.color.b, 1);

            while (true)
            {
                if (tx.color.a < corDesejada.a - 0.05f)
                {
                    tx.color = Color.Lerp(tx.color, corDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    MidAnimation();
                    tx.color = corDesejada;
                    yield break;
                }
            }
        }
    }

    #endregion

    #region Out Animations

    public void OutAnimation()
    {

        if (out_anim_Zoom)
        {
            StartCoroutine(Out_Zoom());
        }

        if (out_anim_Giro)
        {
            StartCoroutine(Out_Grio());
        }

        if (out_anim_Opacidade)
        {
            StartCoroutine(Out_Opacidade());
        }

    }

    public IEnumerator Out_Zoom()
    {
        RectTransform rt = GetComponent<Image>().rectTransform;
        Vector3 escalaDesejada = new Vector3(0, 0, 0);
        while (true)
        {
            if (rt.localScale.x >= escalaDesejada.x + .01f)
            {
                rt.localScale = Vector3.Slerp(rt.localScale, escalaDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                rt.localScale = escalaDesejada;
                gameObject.SetActive(false);
                yield break;
            }
        }
    }

    public IEnumerator Out_Grio()
    {
        rt.localRotation = new Quaternion(0, 0, 0, 1);
        Quaternion rotDesejada = new Quaternion(0, 0, 1, 0);
        while (true)
        {
            if (rt.localRotation.w > 0.05f)
            {
                rt.localRotation = Quaternion.Slerp(rt.localRotation, rotDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                rt.localRotation = rotDesejada;
                gameObject.SetActive(false);
                yield break;
            }
        }
    }

    public IEnumerator Out_Opacidade()
    {
        if (GetComponent<Image>())
        {
            Image im = GetComponent<Image>();
            im.color = new Color(im.color.r, im.color.g, im.color.b, 1);
            Color corDesejada = new Color(im.color.r, im.color.g, im.color.b, 0);

            while (true)
            {
                if (im.color.a > corDesejada.a + 0.05f)
                {
                    im.color = Color.Lerp(im.color, corDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    im.color = corDesejada;
                    gameObject.SetActive(false);
                    yield break;
                }
            }
        }
        if (GetComponent<Text>())
        {
            Text tx = GetComponent<Text>();
            tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, 1);
            Color corDesejada = new Color(tx.color.r, tx.color.g, tx.color.b, 0);

            while (true)
            {
                if (tx.color.a > corDesejada.a + 0.05f)
                {
                    tx.color = Color.Lerp(tx.color, corDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    tx.color = corDesejada;
                    gameObject.SetActive(false);
                    yield break;
                }
            }
        }
    }

    #endregion

    #region Mid Animations

    public void MidAnimation()
    {
        MidAnim = true;

        if (mid_anim_Shake)
        {
            StartCoroutine(Mid_Shake());
        }

        if (mid_anim_Pulse)
        {
            TamanhoInicial = new Vector3(1, 1, 1);
            StartCoroutine(Mid_Pulse());
        }

        if (mid_anim_Wobble)
        {
            StartCoroutine(Mid_Wobble());
        }

    }

    public IEnumerator Mid_Shake()
    {
        if (HoverAnim || ClickAnim) { yield return null; }
        while (MidAnim)
        {
            if (HoverAnim) { yield return null; }
            //rt.localPosition = new Vector3(PosiçãoInicial.x + Random.Range(0f, 2f), PosiçãoInicial.y + Random.Range(0f, 2f), PosiçãoInicial.z);
            rt.localPosition = PosiçãoInicial + Random.insideUnitSphere * VelocidadeAnimação_Mid;
            yield return null;
        }
        rt.localPosition = PosiçãoInicial;
    }

    public IEnumerator Mid_Pulse()
    {
        if (HoverAnim || ClickAnim) { yield return null; }

        if (MidAnim)
        {
            while (true)
            {
                if (HoverAnim) { yield return null; }
                if (!MidAnim) {  yield break; }
                float animation = TamanhoInicial.x + Mathf.Sin(Time.time * VelocidadeAnimação_Mid) * TamanhoInicial.x / (VelocidadeAnimação_Mid - 1);
                rt.localScale = Vector3.one * animation;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            yield break;
        }
    }

    public IEnumerator Mid_Wobble()
    {
        if (HoverAnim || ClickAnim) { yield return null; }

        Quaternion rotDesejada;
        while (MidAnim)
        {
            if (HoverAnim) { yield return null; }
            rotDesejada = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * VelocidadeAnimação_Mid) * 10f);
            rt.localRotation = Quaternion.Slerp(rt.localRotation, rotDesejada, VelocidadeAnimação_In_Out * Time.deltaTime);
            yield return null;
        }
        rt.localRotation = RotaçãoInicial;        
    }

    #endregion

}
