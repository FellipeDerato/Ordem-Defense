using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente_Range_Area : MonoBehaviour
{
    public Agente_Base agtB;
    public Agente_Especifico agtE;


    private void OnEnable()
    {
        agtE.InvokeRepeating("Atualizar_Valores", 0, .2f);
    }
}
