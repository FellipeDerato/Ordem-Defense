using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptUpgrades : MonoBehaviour
{
    
    Torre1 torre1;
    MenuUpgd menuU;
    public GameMaster gm;
    public CelularS cell;
    public GameObject Celular;

    [Header("Preços N�vel 1")]

    public int up1Preço = 200;
    public int up2Preço = 350;
    public int up3Preço = 150;

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

    public Color VermelhoVar = new Color(215, 24, 22);
    public Color VerdeVar = new Color(71, 227, 63);
    public Color CinzaVar = new Color(119, 111, 130);


    [Header("Sistema")]
    public int Upgrade1 = 0;
    public int Upgrade2 = 0;
    public int Upgrade3 = 0;
    public int upsComprados = 0;
    public int DinheiroGasto;
    public int DinheiroDividido;
    public GameObject EfeitoAzul;
    public AudioSource UpSom;
           
    [Header("Bool Fechar ao comprar Ups")]
    public bool up1Fechou;
    public bool up2Fechou;
    public bool up3Fechou;


    private void Start()
    {
        torre1 = gameObject.GetComponent<Torre1>();
        menuU = gameObject.GetComponent<MenuUpgd>();

        DinheiroGasto = torre1.cardOrigem.preçoCarta;
        DinheiroDividido = (int)Mathf.Round(torre1.cardOrigem.preçoCarta - (torre1.cardOrigem.preçoCarta / 4));
    }




    //Caminho 1
    void AumentouCaminho1()
    {
        Image comprarS = cell.Upgrade1Comprar.GetComponent<Image>();
        Image atualS = cell.Upgrade1Atual.GetComponent<Image>();
        DinheiroGasto += up1Preço;
        gm.mm.ValorDinheiro -= up1Preço;

        //Arthur Upgrades
        if (menuU.agenteArthur)
        {
            if (Upgrade1 == 1)
            {
                torre1.fireRate += 1;
                //sistema

                upsComprados++;
                comprarS.sprite = sprite1x2;
                atualS.sprite = sprite1x1;
                up1Preço = up1Preço2;
            }
            else if (Upgrade1 == 2)
            {
                torre1.fireRate += 1;

                //sistema
                comprarS.sprite = sprite1x3;
                atualS.sprite = sprite1x2;
                up1Preço = up1Preço3;
            }
            else if (Upgrade1 == 3)
            {
                torre1.fireRate += 3;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite1x3;
            }
        }

        //Dante Upgrades
        if (menuU.agenteDante)
        {
            if (Upgrade1 == 1)
            {
                torre1.range += 2;

                //sistema
                upsComprados++;
                comprarS.sprite = sprite1x2;
                atualS.sprite = sprite1x1;
                up1Preço = up1Preço2;
            }
            else if (Upgrade1 == 2)
            {
                //feito no torre1

                //sistema
                comprarS.sprite = sprite1x3;
                atualS.sprite = sprite1x2;
                up1Preço = up1Preço3;
            }
            else if (Upgrade1 == 3)
            {
                //feito no torre1

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite1x3;
            }
        }

        //Samuel Upgrades
        if (menuU.agenteSamuel)
        {
            if (Upgrade1 == 1)
            {
                torre1.BonusDinheiro += 15;

                //sistema
                upsComprados++;
                comprarS.sprite = sprite1x2;
                atualS.sprite = sprite1x1;
                up1Preço = up1Preço2;
            }
            else if (Upgrade1 == 2)
            {
                torre1.BonusDinheiro += 30;

                //sistema
                comprarS.sprite = sprite1x3;
                atualS.sprite = sprite1x2;
                up1Preço = up1Preço3;
            }
            else if (Upgrade1 == 3)
            {
                torre1.BonusDinheiro += 30;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite1x3;
            }
        }

        //Carina Upgrades
        if (menuU.agenteCarina)
        {
            if (Upgrade1 == 1)
            {

                //sistema
                upsComprados++;
                comprarS.sprite = sprite1x2;
                atualS.sprite = sprite1x1;
                up1Preço = up1Preço2;
            }
            else if (Upgrade1 == 2)
            {

                //sistema
                comprarS.sprite = sprite1x3;
                atualS.sprite = sprite1x2;
                up1Preço = up1Preço3;
            }
            else if (Upgrade1 == 3)
            {

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite1x3;
            }
        }

        //Rubens Upgrades
        if (menuU.agenteRubens)
        {
            if (Upgrade1 == 1)
            {
                torre1.BonusVida += 1;


                //sistema
                upsComprados++;
                comprarS.sprite = sprite1x2;
                atualS.sprite = sprite1x1;
                up1Preço = up1Preço2;
            }
            else if (Upgrade1 == 2)
            {
                torre1.BonusVida += 2;

                //sistema
                comprarS.sprite = sprite1x3;
                atualS.sprite = sprite1x2;
                up1Preço = up1Preço3;
            }
            else if (Upgrade1 == 3)
            {
                torre1.BonusVida += 3;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite1x3;
            }
        }

        //Balu Upgrades
        if (menuU.agenteBalu)
        {
            if (Upgrade1 == 1)
            {

                //sistema
                upsComprados++;
                comprarS.sprite = sprite1x2;
                atualS.sprite = sprite1x1;
                up1Preço = up1Preço2;
            }
            else if (Upgrade1 == 2)
            {

                //sistema
                comprarS.sprite = sprite1x3;
                atualS.sprite = sprite1x2;
                up1Preço = up1Preço3;
            }
            else if (Upgrade1 == 3)
            {

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite1x3;
            }
        }



        UpEfect();
        DinheiroDividido = (int)Mathf.Round(DinheiroGasto - (DinheiroGasto / 4));
    }



    //Caminho 2
    void AumentouCaminho2()
    {
        Image comprarS = cell.Upgrade2Comprar.GetComponent<Image>();
        Image atualS = cell.Upgrade2Atual.GetComponent<Image>();
        DinheiroGasto += up2Preço;
        gm.mm.ValorDinheiro -= up2Preço;

        //Arthur Upgrades
        if (menuU.agenteArthur)
        {
            if (Upgrade2 == 1)
            {
                torre1.DanoTotal += 2;

                //sistema
                upsComprados++;
                comprarS.sprite = sprite2x2;
                atualS.sprite = sprite2x1;
                up2Preço = up2Preço2;
            }
            else if (Upgrade2 == 2)
            {
                torre1.DanoTotal += 2;

                //sistema
                comprarS.sprite = sprite2x3;
                atualS.sprite = sprite2x2;
                up2Preço = up2Preço3;
            }
            else if (Upgrade2 == 3)
            {
                torre1.DanoTotal += 3;
                menuU.Fisico = false;
                menuU.Energia = true;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite2x3;
            }
        }

        //Dante Upgrades
        if (menuU.agenteDante)
        {
            if (Upgrade2 == 1)
            {
                torre1.DanoTotal += 1;
                torre1.pierceBala += 1;

                //sistema
                upsComprados++;
                comprarS.sprite = sprite2x2;
                atualS.sprite = sprite2x1;
                up2Preço = up2Preço2;
            }
            else if (Upgrade2 == 2)
            {
                torre1.DanoTotal += 2;
                torre1.pierceBala += 2;

                //sistema
                comprarS.sprite = sprite2x3;
                atualS.sprite = sprite2x2;
                up2Preço = up2Preço3;
            }
            else if (Upgrade2 == 3)
            {
                torre1.DanoTotal += 2;
                torre1.pierceBala += 2;
                menuU.Morte = false;
                menuU.Energia = true;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite2x3;
            }
        }

        //Samuel Upgrades
        if (menuU.agenteSamuel)
        {
            if (Upgrade2 == 1)
            {
                torre1.BonusEnumeratorDinheiro += 1;

                //sistema
                upsComprados++;
                comprarS.sprite = sprite2x2;
                atualS.sprite = sprite2x1;
                up2Preço = up2Preço2;
            }
            else if (Upgrade2 == 2)
            {
                torre1.BonusEnumeratorDinheiro += 1;

                //sistema
                comprarS.sprite = sprite2x3;
                atualS.sprite = sprite2x2;
                up2Preço = up2Preço3;
            }
            else if (Upgrade2 == 3)
            {
                torre1.BonusEnumeratorDinheiro += 2;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite2x3;
            }
        }

        //Carina Upgrades
        if (menuU.agenteCarina)
        {
            if (Upgrade2 == 1)
            {

                //sistema
                upsComprados++;
                comprarS.sprite = sprite2x2;
                atualS.sprite = sprite2x1;
                up2Preço = up2Preço2;
            }
            else if (Upgrade2 == 2)
            {

                //sistema
                comprarS.sprite = sprite2x3;
                atualS.sprite = sprite2x2;
                up2Preço = up2Preço3;
            }
            else if (Upgrade2 == 3)
            {

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite2x3;
            }
        }

        //Rubens Upgrades
        if (menuU.agenteRubens)
        {
            if (Upgrade2 == 1)
            {
                //feito no torre1

                //sistema
                upsComprados++;
                comprarS.sprite = sprite2x2;
                atualS.sprite = sprite2x1;
                up2Preço = up2Preço2;
            }
            else if (Upgrade2 == 2)
            {
                //feito no torre1

                //sistema
                comprarS.sprite = sprite2x3;
                atualS.sprite = sprite2x2;
                up2Preço = up2Preço3;
            }
            else if (Upgrade2 == 3)
            {
                //Resto feito no Torre1
                cell.Habilidade1.SetActive(true);
                cell.Habilidade1Sprite.GetComponent<Image>().sprite = Habilidade1Sprite;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite2x3;
            }
        }

        //Balu Upgrades
        if (menuU.agenteBalu)
        {
            if (Upgrade2 == 1)
            {

                //sistema
                upsComprados++;
                comprarS.sprite = sprite2x2;
                atualS.sprite = sprite2x1;
                up2Preço = up2Preço2;
            }
            else if (Upgrade2 == 2)
            {

                //sistema
                comprarS.sprite = sprite2x3;
                atualS.sprite = sprite2x2;
                up2Preço = up2Preço3;
            }
            else if (Upgrade2 == 3)
            {

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite2x3;
            }
        }



        UpEfect();
        DinheiroDividido = (int)Mathf.Round(DinheiroGasto - (DinheiroGasto / 4));
    }



    //Caminho 3
    void AumentouCaminho3()
    {
        Image comprarS = cell.Upgrade3Comprar.GetComponent<Image>();
        Image atualS = cell.Upgrade3Atual.GetComponent<Image>();
        DinheiroGasto += up3Preço;
        gm.mm.ValorDinheiro -= up3Preço;

        //Arthur Upgrades
        if (menuU.agenteArthur)
        {
            if (Upgrade3 == 1)
            {
                torre1.range = torre1.range * 1.5f;
                torre1.pierceBala += 1;

                //sistema
                upsComprados++;
                comprarS.sprite = sprite3x2;
                atualS.sprite = sprite3x1;
                up3Preço = up3Preço2;
            }
            else if (Upgrade3 == 2)
            {
                torre1.range = torre1.range * 1.2f;
                torre1.pierceBala += 1;
                torre1.ocultoAcerta = true;

                //sistema
                comprarS.sprite = sprite3x3;
                atualS.sprite = sprite3x2;
                up3Preço = up3Preço3;
            }
            else if (Upgrade3 == 3)
            {
                torre1.range = torre1.range * 1.4f;
                torre1.velocidadeBala += 10f;
                torre1.pierceBala += 1;
                torre1.DanoTotal += 2;
                menuU.Fisico = false;
                menuU.Sangue = true;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite3x3;
            }
        }

        //Dante Upgrades
        if (menuU.agenteDante)
        {
            if (Upgrade3 == 1)
            {
                torre1.ocultoAcerta = true;

                //sistema
                upsComprados++;
                comprarS.sprite = sprite3x2;
                atualS.sprite = sprite3x1;
                up3Preço = up3Preço2;
            }
            else if (Upgrade3 == 2)
            {
                //Feito no torre1

                //sistema
                comprarS.sprite = sprite3x3;
                atualS.sprite = sprite3x2;
                up3Preço = up3Preço3;
            }
            else if (Upgrade3 == 3)
            {
                //Resto feito no Torre1
                cell.Habilidade1.SetActive(true);
                cell.Habilidade1Sprite.GetComponent<Image>().sprite = Habilidade1Sprite;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite3x3;
            }
        }

        //Samuel Upgrades
        if (menuU.agenteSamuel)
        {
            if (Upgrade3 == 1)
            {

                //sistema
                upsComprados++;
                comprarS.sprite = sprite3x2;
                atualS.sprite = sprite3x1;
                up3Preço = up3Preço2;
            }
            else if (Upgrade3 == 2)
            {

                //sistema
                comprarS.sprite = sprite3x3;
                atualS.sprite = sprite3x2;
                up3Preço = up3Preço3;
                UpEfect();
            }
            else if (Upgrade3 == 3)
            {

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite3x3;
            }
        }

        //Carina Upgrades
        if (menuU.agenteCarina)
        {
            if (Upgrade3 == 1)
            {

                //sistema
                upsComprados++;
                comprarS.sprite = sprite3x2;
                atualS.sprite = sprite3x1;
                up3Preço = up3Preço2;
            }
            else if (Upgrade3 == 2)
            {

                //sistema
                comprarS.sprite = sprite3x3;
                atualS.sprite = sprite3x2;
                up3Preço = up3Preço3;
            }
            else if (Upgrade3 == 3)
            {

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite3x3;
            }
        }

        //Rubens Upgrades
        if (menuU.agenteRubens)
        {
            if (Upgrade3 == 1)
            {
                torre1.DanoTotal += 1;
                torre1.pierceBala += 1;

                //sistema
                upsComprados++;
                comprarS.sprite = sprite3x2;
                atualS.sprite = sprite3x1;
                up3Preço = up3Preço2;
            }
            else if (Upgrade3 == 2)
            {
                torre1.DanoTotal += 2;
                torre1.pierceBala += 1;

                //sistema
                comprarS.sprite = sprite3x3;
                atualS.sprite = sprite3x2;
                up3Preço = up3Preço3;
            }
            else if (Upgrade3 == 3)
            {
                torre1.DanoTotal += 2;
                torre1.BonusSanidade += 2;
                torre1.pierceBala += 1;

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite3x3;
            }
        }

        //Balu Upgrades
        if (menuU.agenteBalu)
        {
            if (Upgrade3 == 1)
            {

                //sistema
                upsComprados++;
                comprarS.sprite = sprite3x2;
                atualS.sprite = sprite3x1;
                up3Preço = up3Preço2;
            }
            else if (Upgrade3 == 2)
            {

                //sistema
                comprarS.sprite = sprite3x3;
                atualS.sprite = sprite3x2;
                up3Preço = up3Preço3;
            }
            else if (Upgrade3 == 3)
            {

                //sistema
                comprarS.sprite = CaminhoCompleto;
                atualS.sprite = sprite3x3;
            }
        }



        UpEfect();
        DinheiroDividido = (int)Mathf.Round(DinheiroGasto - (DinheiroGasto / 4));
    }
    
    




    public void BotaoUP1()
    {
        if (gm.mm.ValorDinheiro >= up1Preço)
        {
            Upgrade1++; AumentouCaminho1();
        }
        
    }
        
    public void BotaoUP2()
    {
        if (gm.mm.ValorDinheiro >= up2Preço)
        {
            Upgrade2++; AumentouCaminho2();
        }
        
    }
    public void BotaoUP3()
    {
        if (gm.mm.ValorDinheiro >= up3Preço)
        {
            Upgrade3++; AumentouCaminho3();
        }
        
    }

    public void UpEfect()
    {
        GameObject effectIns = (GameObject)Instantiate(EfeitoAzul, transform.position, transform.rotation);
        effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 2, effectIns.transform.position.z);
        Destroy(effectIns, 0.25f);
        UpSom.Play();
    }

    private void Update()
    {

        //Fechar por comprar Outros Upgrades
        if (upsComprados >= 2 && Upgrade1 == 0) { cell.Upgrade1Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up1Fechou = true; cell.Botao1.GetComponent<Image>().color = CinzaVar; }
        else if (Upgrade2 >= 3 && Upgrade1 >= 2) { cell.Upgrade1Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up1Fechou = true; cell.Botao1.GetComponent<Image>().color = CinzaVar; }
        else if (Upgrade3 >= 3 && Upgrade1 >= 2) { cell.Upgrade1Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up1Fechou = true; cell.Botao1.GetComponent<Image>().color = CinzaVar; }
        else { up1Fechou = false; }

        if (upsComprados >= 2 && Upgrade2 == 0) { cell.Upgrade2Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up2Fechou = true; cell.Botao2.GetComponent<Image>().color = CinzaVar; }
        else if (Upgrade1 >= 3 && Upgrade2 >= 2) { cell.Upgrade2Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up2Fechou = true; cell.Botao2.GetComponent<Image>().color = CinzaVar; }
        else if (Upgrade3 >= 3 && Upgrade2 >= 2) { cell.Upgrade2Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up2Fechou = true; cell.Botao2.GetComponent<Image>().color = CinzaVar; }
        else { up2Fechou = false; }

        if (upsComprados >= 2 && Upgrade3 == 0) { cell.Upgrade3Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up3Fechou = true; cell.Botao3.GetComponent<Image>().color = CinzaVar; }
        else if (Upgrade1 >= 3 && Upgrade3 >= 2) { cell.Upgrade3Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up3Fechou = true; cell.Botao3.GetComponent<Image>().color = CinzaVar; }
        else if (Upgrade2 >= 3 && Upgrade3 >= 2) { cell.Upgrade3Comprar.GetComponent<Image>().sprite = CaminhoBloqueado; up3Fechou = true; cell.Botao3.GetComponent<Image>().color = CinzaVar; }
        else { up3Fechou = false; }


        //Upgrade 1 cor e comprar
        if (!up1Fechou)
        {
            cell.Botao1.GetComponent<Button>().enabled = true;
            if (Upgrade1 < 3)
            {
                if (gm.mm.ValorDinheiro < up1Preço)
                {
                    cell.Botao1.GetComponent<Image>().color = VermelhoVar;
                }
                else
                {
                    cell.Botao1.GetComponent<Image>().color = VerdeVar;
                }
                cell.up1TextPreço.text = "$" + up1Preço.ToString();
            }
            else if (Upgrade1 >= 3)
            {
                cell.Botao1.GetComponent<Image>().color = CinzaVar;
                cell.up1TextPreço.text = " ";
            }
        } else { cell.Botao1.GetComponent<Button>().enabled = false; cell.up1TextPreço.text = " "; }


        //Upgrade 2 cor e comprar
        if (!up2Fechou)
        {
            cell.Botao2.GetComponent<Button>().enabled = true;
            if (Upgrade2 < 3)
            {
                if (gm.mm.ValorDinheiro < up2Preço)
                {
                    cell.Botao2.GetComponent<Image>().color = VermelhoVar;
                }
                else
                {
                    cell.Botao2.GetComponent<Image>().color = VerdeVar;
                }
                cell.up2TextPreço.text = "$" + up2Preço.ToString();
            }
            else if (Upgrade2 >= 3)
            {
                cell.Botao2.GetComponent<Image>().color = CinzaVar;
                cell.up2TextPreço.text = " ";
            }
        } else { cell.Botao2.GetComponent<Button>().enabled = false; cell.up2TextPreço.text = " "; }


        //Upgrade 3 cor e comprar
        if (!up3Fechou)
        {
            cell.Botao3.GetComponent<Button>().enabled = true;
            if (Upgrade3 < 3)
            {
                if (gm.mm.ValorDinheiro < up3Preço)
                {
                    cell.Botao3.GetComponent<Image>().color = VermelhoVar;
                }
                else
                {
                    cell.Botao3.GetComponent<Image>().color = VerdeVar;
                }
                cell.up3TextPreço.text = "$" + up3Preço.ToString();
            }
            else if (Upgrade3 >= 3)
            {
                cell.Botao3.GetComponent<Image>().color = CinzaVar;
                cell.up3TextPreço.text = " ";
            }
        } else { cell.Botao3.GetComponent<Button>().enabled = false; cell.up3TextPreço.text = " "; }


        cell.verderText.text = "Vender por $" + DinheiroDividido;
    }
}
