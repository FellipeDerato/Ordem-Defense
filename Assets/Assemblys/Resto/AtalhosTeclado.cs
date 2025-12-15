using System.Collections;
using System.Collections.Generic;
//using UnityEditor.XR;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtalhosTeclado : MonoBehaviour
{
    public GameMaster gm;
    public float cd1;


    private void Start()
    {
        gm = GetComponent<GameMaster>();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("pertou esc");
            gm.mu.Botão_Pausar(); 
        }


    }    
}
