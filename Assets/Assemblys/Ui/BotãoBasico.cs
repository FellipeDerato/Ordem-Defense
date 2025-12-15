using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BotãoBasico : MonoBehaviour
{
    [Header("Tipo de Animação")]
    public bool animação_Scale;
    public bool animação_Tremida;

    [Space]
    public bool PrecisaDeCollider;
    [Space]

    public bool Start_End_Scale;
    public bool Start_End_Tremida;
    public bool Start_End_Alpha;

    public float TempoStartAnim;
    public float alpha_desejado;
    public float alpha_inicial;

    public bool start_anim;
    public bool end_anim;

    [Header("Parametros da Animação")]
    public float TempoAnim;
    public float ProporçãoAnim;
    public float ProporçãoClickAnim;
    int intLegal;
    public bool animação_ocorrendo;

    [Header("Som da Animação")]

    public bool som_enter;
    public bool som_click;

    [Space]

    public float volume_Enter;
    public float volume_Click;

    [Space]

    public AudioClip audio_Enter;
    public AudioClip audio_Click;

    public AudioMixerGroup mixer;

    [Header("Sistema")]

    public bool Imagem_e_outra;
    public Image imagem_botao;
    public bool funciona_por_script;
    public bool travado;
    public bool Travar_Outros;
    public bool trav_o1;
    public bool trav_o2;
    public List<GameObject> Botões;
    RectTransform rt1;
    AudioSource as1;
    public bool tem_função;
    public GameObject objetoParaExecutarFuncao;
    public string nomeDaFuncaoParaExecutar;
    public bool PrecisaSerReferenciado = false;




    public bool anim_Enter = false;
    public bool anim_Exit = false;
    public bool anim_Click = false;

    [Space]

    public Vector3 scale_inicial;
    public Vector2 position_inicial;

    Vector3 scale_Atual;

    private void Start()
    {
        anim_Enter = false;
        anim_Exit = false;
        anim_Click = false;

        if (!Imagem_e_outra)
        {
            rt1 = GetComponent<Image>().rectTransform;
        }
        else if (Imagem_e_outra)
        {
            rt1 = imagem_botao.rectTransform;
        }
        
        scale_inicial = rt1.localScale;
        position_inicial = rt1.localPosition;

        if (som_enter || som_click)
        {
            as1 = gameObject.AddComponent<AudioSource>();
            as1.outputAudioMixerGroup = mixer;
        }

        if (PrecisaDeCollider)
        {
            if (!GetComponent<BoxCollider>())
            {
                BoxCollider bc = gameObject.AddComponent<BoxCollider>();
                bc.size = new Vector3(rt1.sizeDelta.x, rt1.sizeDelta.y, 1);
                if (rt1.sizeDelta.y == 0) { bc.size = new Vector3(rt1.sizeDelta.x, rt1.sizeDelta.x, 1); }
                bc.isTrigger = true;
            }
        }

        if (Start_End_Alpha) { start_anim = true; Image image = GetComponent<Image>(); image.color = new Color(image.color.r, image.color.g, image.color.b, alpha_inicial); }
        if (Start_End_Scale) { start_anim = true; rt1.localScale = new Vector3(rt1.localScale.x / 5, rt1.localScale.y / 5, rt1.localScale.z / 5); }
        if (Start_End_Tremida) { start_anim = true; }
    }

    public void TRAVAR_BOTÃO(bool estado)
    {
        travado = estado;
    }


    public void TRAVAR_OUTROS_BOTÕES()
    {
        foreach (GameObject botão in Botões)
        {
            if (botão.GetComponent<CardAgente>())
            {
                botão.GetComponent<CardAgente>().travada = true;
            }
            if (botão.GetComponent<BotãoBasico>())
            {
                botão.GetComponent<BotãoBasico>().TRAVAR_BOTÃO(true);
            }
            if (botão.GetComponent<Button>())
            {
                GetComponent<Button>().interactable = false;
            }
        }
    }
    public void DESTRAVAR_OUTROS_BOTÕES()
    {
        foreach (GameObject botão in Botões)
        {
            if (botão.GetComponent<CardAgente>())
            {
                botão.GetComponent<CardAgente>().travada = false;
            }
            if (botão.GetComponent<BotãoBasico>())
            {
                botão.GetComponent<BotãoBasico>().TRAVAR_BOTÃO(false);
            }
            if (botão.GetComponent<Button>())
            {
                GetComponent<Button>().interactable = true;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (travado) { return; }

        anim_Enter = true;
        anim_Exit = false;

        if (som_enter) { as1.volume = volume_Enter; as1.PlayOneShot(audio_Enter); }
    }

    private void OnMouseExit()
    {
        if (travado) { return; }

        anim_Enter = false;
        anim_Exit = true;
    }

    private void OnMouseUpAsButton()
    {
        if (travado) { return; }

        anim_Click = true;
        if (som_click) { as1.volume = volume_Click; as1.PlayOneShot(audio_Click); }
        if (tem_função)
        {
            if (PrecisaSerReferenciado)
            {
                objetoParaExecutarFuncao.SendMessage(nomeDaFuncaoParaExecutar, gameObject);
            }
            else
            {
                objetoParaExecutarFuncao.SendMessage(nomeDaFuncaoParaExecutar);
            }
        }
    }

    public void ClickBotão()
    {
        if (travado) { return; }
        animação_ocorrendo = true;
        anim_Click = true;
        if (som_click) { as1.volume = volume_Click; as1.PlayOneShot(audio_Click); }
        if (funciona_por_script) { GetComponent<Button>().onClick.Invoke(); }
        if (tem_função)
        {
            objetoParaExecutarFuncao.SendMessage(nomeDaFuncaoParaExecutar);
        }
    }

    private void Update()
    {
        if (travado) { return; }

        if (Travar_Outros)
        {
            if (trav_o1)
            {
                TRAVAR_OUTROS_BOTÕES();
                trav_o1 = false;
            }
            else if (trav_o2)
            {
                DESTRAVAR_OUTROS_BOTÕES();
                trav_o2 = false;
            }
        }


        if (Start_End_Scale)
        {
            if (start_anim)
            {
                if (Vector3.Distance(rt1.localScale, scale_inicial) >= scale_inicial.x * 0.1f)                
                {
                    rt1.localScale = Vector3.Lerp(rt1.localScale, scale_inicial, TempoStartAnim * Time.deltaTime);
                    return;
                }
                rt1.localScale = scale_inicial;
                start_anim = false;
                trav_o1 = true;
            }

            if (end_anim)
            {
                if (Vector3.Distance(rt1.localScale, scale_inicial / 5) >= scale_inicial.x / 3)
                {
                    rt1.localScale = Vector3.Lerp(rt1.localScale, scale_inicial / 5, TempoStartAnim * Time.deltaTime);
                    return;
                }
                end_anim = false;
                start_anim = true;
                gameObject.SetActive(false);
            }
        }

        if (Start_End_Tremida)
        {
            if (start_anim)
            {
                if (intLegal < 5)
                {
                    Vector3 pos1 = new Vector3(rt1.localPosition.x + ProporçãoAnim, rt1.localPosition.y + ProporçãoAnim, rt1.localPosition.z);
                    Vector3 pos2 = new Vector3(rt1.localPosition.x - (ProporçãoAnim * 2), rt1.localPosition.y - (ProporçãoAnim * 2), rt1.localPosition.z);

                    if (intLegal >= 4 || intLegal <= 1)
                    {
                        rt1.localPosition = pos1;
                    }
                    else
                    {
                        rt1.localPosition = pos2;
                    }
                    intLegal++;
                    return;
                }
                trav_o1 = true;
                start_anim = false;
                intLegal = 0;
            }

            if (end_anim)
            {
                if (intLegal < 5)
                {
                    Vector3 pos1 = new Vector3(rt1.localPosition.x + ProporçãoAnim, rt1.localPosition.y + ProporçãoAnim, rt1.localPosition.z);
                    Vector3 pos2 = new Vector3(rt1.localPosition.x - (ProporçãoAnim * 2), rt1.localPosition.y - (ProporçãoAnim * 2), rt1.localPosition.z);

                    if (intLegal >= 4 || intLegal <= 1)
                    {
                        rt1.localPosition = pos1;
                    }
                    else
                    {
                        rt1.localPosition = pos2;
                    }
                    intLegal++;
                    return;
                }
                end_anim = false;
                intLegal = 0;
                gameObject.SetActive(false);
            }
        }

        if (Start_End_Alpha)
        {
            Image image = GetComponent<Image>();
            if (end_anim)
            {
                if (image.color.a - 0.1f > alpha_inicial)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, alpha_inicial, TempoStartAnim * 1.5f));
                    return;
                }
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha_inicial);
                start_anim = true;
                end_anim = false;
                gameObject.SetActive(false);
            }
            if (start_anim)
            {
                if (image.color.a + 0.1f < alpha_desejado)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, alpha_desejado, TempoStartAnim));
                    return;
                }
                trav_o1 = true;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha_desejado);
                start_anim = false;
            }
        }




        if (animação_Scale)
        {
            if (anim_Click)
            {
                rt1.localScale = new Vector3(rt1.localScale.x * ProporçãoClickAnim, rt1.localScale.y * ProporçãoClickAnim, rt1.localScale.z * ProporçãoClickAnim);
                anim_Click = false;
                animação_ocorrendo = false;
            }
            if (anim_Enter)
            {
                scale_Atual = rt1.localScale;
                if (rt1.localScale != scale_inicial * ProporçãoAnim)
                {
                    rt1.localScale = Vector3.Lerp(rt1.localScale, scale_inicial * ProporçãoAnim, TempoAnim * Time.deltaTime);
                    return;
                }
                anim_Enter = false;
            }
            if (anim_Exit)
            {
                scale_Atual = rt1.localScale;
                if (rt1.localScale != scale_inicial)
                {
                    rt1.localScale = Vector3.Lerp(rt1.localScale, scale_inicial, TempoAnim * Time.deltaTime);
                    return;
                }
                anim_Exit = false;
            }
        }

        if (animação_Tremida)
        {
            if (anim_Click)
            {
                if (intLegal < 5)
                {
                    Vector3 pos1 = new Vector3(rt1.localPosition.x + ProporçãoClickAnim, rt1.localPosition.y + ProporçãoClickAnim, rt1.localPosition.z);
                    Vector3 pos2 = new Vector3(rt1.localPosition.x - (ProporçãoClickAnim * 2), rt1.localPosition.y - (ProporçãoClickAnim * 2), rt1.localPosition.z);

                    if (intLegal >= 4 || intLegal <= 1)
                    {
                        rt1.localPosition = pos1;
                    }
                    else
                    {
                        rt1.localPosition = pos2;
                    }
                    intLegal++;
                    return;
                }
                anim_Click = false;
            }
            else if (anim_Enter)
            {

                if (intLegal < 5)
                {
                    Vector3 pos1 = new Vector3(rt1.localPosition.x + ProporçãoAnim, rt1.localPosition.y + ProporçãoAnim, rt1.localPosition.z);
                    Vector3 pos2 = new Vector3(rt1.localPosition.x - (ProporçãoAnim * 2), rt1.localPosition.y - (ProporçãoAnim * 2), rt1.localPosition.z);

                    if (intLegal >= 4 || intLegal <= 1)
                    {
                        rt1.localPosition = pos1;
                    }
                    else
                    {
                        rt1.localPosition = pos2;
                    }
                    intLegal++;
                    return;
                }
                anim_Enter = false;
            }
            else
            {
                rt1.localPosition = position_inicial;
                intLegal = 0;
            }

        }
    }

    public void Desativar_GameObject()
    {
        if (Travar_Outros)
        {
            trav_o2 = true;
        }
        if (Start_End_Alpha)
        {
            end_anim = true;
        }

        if (Start_End_Scale)
        {
            end_anim = true;
        }

        if (Start_End_Tremida)
        {
            end_anim = true;
        }
    }




}
