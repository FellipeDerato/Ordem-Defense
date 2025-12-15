using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzFlick : MonoBehaviour
{
    public Color CorInicial;
    public Color CorFlick;
    public float Intensidade;
    public bool FlickAtivo = true;
    public float Delay = 0f;

    private void Start()
    {
        if (FlickAtivo)
        {
            StartCoroutine(Flick());
        }
    }


    IEnumerator Flick()
    {
        yield return new WaitForSeconds(Delay);
        Ligou();
        yield return new WaitForSeconds(1.5f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(1f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(.7f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(.2f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(.1f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(2f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(.7f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(1.1f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(1.9f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(1.2f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(.9f);
        Desligou();
        yield return new WaitForSeconds(.05f);
        Ligou();
        yield return new WaitForSeconds(1f);
        if (FlickAtivo)
        {
            StartCoroutine(Flick());
        }
    }

    public void Desligou()
    {
        if (GetComponent<Renderer>())
        {
            Material mat = GetComponent<Renderer>().material;
            mat.SetColor("_EmissionColor", (CorFlick) * 0);
        }
        GetComponent<Light>().enabled = false;
    }

    public void Ligou()
    {
        if (GetComponent<Renderer>())
        {
            Material mat = GetComponent<Renderer>().material;
            mat.SetColor("_EmissionColor", (CorInicial) * Intensidade);
        }
        GetComponent<Light>().enabled = true;
    }

}
