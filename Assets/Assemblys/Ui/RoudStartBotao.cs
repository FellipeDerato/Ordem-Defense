using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoudStartBotao : MonoBehaviour {

   
    public WaveSpawner waveSpawner;




    // Start is called before the first frame update
    public void Iniciou()
    {

        waveSpawner = GetComponent<WaveSpawner>();
        waveSpawner.SpawnWave();
        Debug. Log ("oi");
    }

   
}
