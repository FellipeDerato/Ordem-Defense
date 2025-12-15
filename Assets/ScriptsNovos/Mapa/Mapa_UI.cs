using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mapa_UI : MonoBehaviour
{
    [Header("Valores")]
    public GameObject ValorVida;
    public GameObject ValorSanidade;
    public GameObject ValorDinheiro;
    public GameObject ValorRound;

    [Header("Opi��es")]

    public GameObject Botão_Engrenagem;
    public GameObject Opções_Menu;
    public GameObject Pause_Principal;
    public GameObject Botão_VoltarMenu;
    public GameObject Botão_Recomeçar;

    [Header("Game Over")]

    public GameObject GameOverTela;

    [Header("Source Sons")]

    public AudioSource source_MousePorCima;
    public AudioSource source_Ambiente;
    public AudioSource source_Clique;
    public AudioSource source_Clique2;

    [Header("Seleção")]

    public GameObject Hud_Seleção;
    public GameObject Hud_Seleção_Começar;

    [Header("Hud Barra")]

    public GameObject Botão_Principal;
    public GameObject Hud_Barra;

    [Header("Outros")]

    public GameObject FundoPreto;
    public Color FundoPreto_cor1;
    public Color FundoPreto_cor2;
    public bool anim1 = false;
    public bool anim2 = false;
    public bool pretaAnim = false;

    [Header("Sistema")]

    public GameMaster gm;
    public Mapa_Master mm;

    bool bool_Pausar = false;
    bool bool_Recomeçar1 = false;
    bool bool_MenuPrincipal1 = false;


    public float cooldown_PadrãoUI = .8f;
    public float timer_GlobalUI = .8f;
    public bool AçãoPossível = true;

    public IEnumerator CooldownGlobal()
    {
        while (true)
        {
            if (timer_GlobalUI > 0.01f)
            {
                timer_GlobalUI -= Time.deltaTime;
                AçãoPossível = false;
                yield return new WaitForEndOfFrame();
            }
            else 
            {
                AçãoPossível = true;
                timer_GlobalUI = cooldown_PadrãoUI;
                yield break;
            }
        }
    }

    public IEnumerator TelaPretaFundo(bool Ligada)
    {
        // timer ou anim n�o acabou
        while (true)
        {
            if (pretaAnim)
            {
                yield break;
            }
            else
            {
                break;
            }
        }

        if (Ligada)
        {
            while (true)
            {
                if (anim2) { yield break; }
                //AçãoPossível = false;
                pretaAnim = true;
                if (FundoPreto.GetComponent<Image>().color.a <= FundoPreto_cor1.a - .05f)
                {

                    FundoPreto.SetActive(true);
                    anim1 = true;
                    Color cor = Color.Lerp(FundoPreto.GetComponent<Image>().color, FundoPreto_cor1, 5f * Time.deltaTime);
                    FundoPreto.GetComponent<Image>().color = cor;
                    yield return new WaitForEndOfFrame();
                }
                else 
                {
                    //AçãoPossível = true;
                    pretaAnim = false;
                    anim1 = false;
                    yield break; 
                }
            }
        }

        else if (!Ligada)
        {
            while (true)
            {
                if (anim1) { yield break; }
                //AçãoPossível = false;
                pretaAnim = true;
                if (FundoPreto.GetComponent<Image>().color.a >= FundoPreto_cor2.a + .05f)
                //if (FundoPreto.GetComponent<Image>().color != FundoPreto_cor2)
                {
                    anim2 = true;
                    Color cor = Color.Lerp(FundoPreto.GetComponent<Image>().color, FundoPreto_cor2, 5f * Time.deltaTime);
                    FundoPreto.GetComponent<Image>().color = cor;
                    yield return new WaitForEndOfFrame();
                }
                else 
                {
                    //AçãoPossível = true;
                    pretaAnim = false;
                    anim2 = false;
                    FundoPreto.SetActive(false);
                    yield break;
                }
            }
        }
        
    }

    public void Pause_Recomeçar1()
    {
        if (!AçãoPossível) { return; }
        StartCoroutine(CooldownGlobal());

        if (bool_Recomeçar1)
        {
            Pause_Principal.GetComponent<BotãoBasico>().Desativar_GameObject(); 
            Botão_Recomeçar.GetComponent<BotãoBasico>().Desativar_GameObject();
            bool_Recomeçar1 = false;
        }
        else if (!bool_Recomeçar1)
        {
            Pause_Principal.SetActive(true);
            Botão_Recomeçar.SetActive(true);
            bool_Recomeçar1 = true;
        }
    }

    public void Pause_MenuPrincipal1()
    {
        if (!AçãoPossível) { return; }
        StartCoroutine(CooldownGlobal());

        if (bool_MenuPrincipal1)
        {
            Opções_Menu.SetActive(false);
            Botão_VoltarMenu.SetActive(false);
            bool_MenuPrincipal1 = false;
        }
        else if (!bool_MenuPrincipal1)
        {
            Opções_Menu.SetActive(true);
            Botão_VoltarMenu.SetActive(true);
            bool_MenuPrincipal1 = true;
        }
    }

    public void BotãoPrincipal()
    {
        mm.BotãoPrincipal();
    }

    public void Botão_Pausar()
    {
        if (!AçãoPossível) { return; }
        StartCoroutine(CooldownGlobal());

        if (bool_Pausar)
        {
            StartCoroutine(TelaPretaFundo(false));
            Botão_VoltarMenu.GetComponent<BotãoBasico>().Desativar_GameObject();
            Botão_Recomeçar.GetComponent<BotãoBasico>().Desativar_GameObject();
            Pause_Principal.GetComponent<BotãoBasico>().Desativar_GameObject();
            bool_Pausar = false;
            bool_Recomeçar1 = false;
            bool_MenuPrincipal1 = false;
        }
        else if (!bool_Pausar)
        {
            StartCoroutine(TelaPretaFundo(true));
            Pause_Principal.SetActive(true);
            bool_Pausar = true;
        }
    }

}
