using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuadradinhosScript : MonoBehaviour
{
    Agente_UI celular;
    public GameObject Celular;
    [Header("Quadradinhos")]
    public GameObject q11;
    public GameObject q12;
    public GameObject q13;

    public GameObject q21;
    public GameObject q22;
    public GameObject q23;

    public GameObject q31;
    public GameObject q32;
    public GameObject q33;

    private void Awake()
    {
        celular = Celular.GetComponent<Agente_UI>();

        celular.q11 = q11;
        celular.q12 = q12;
        celular.q13 = q13;

        celular.q21 = q21;
        celular.q22 = q22;
        celular.q23 = q23;

        celular.q31 = q31;
        celular.q32 = q32;
        celular.q33 = q33;

    }
}
