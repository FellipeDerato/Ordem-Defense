using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posFinal_Mapa : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Criatura_Base"))
        {
            col.gameObject.GetComponent<Criatura_Base>().CriaturaPosFinal();
        }
    }
}
