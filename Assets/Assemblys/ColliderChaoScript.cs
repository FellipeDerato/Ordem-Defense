using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChaoScript : MonoBehaviour
{
    private void OnMouseEnter()
    {
        GetComponentInParent<Agente_Base>().mouseDentroDoObjeto = true;
    }

    private void OnMouseExit()
    {
        GetComponentInParent<Agente_Base>().mouseDentroDoObjeto = false;
    }
}
