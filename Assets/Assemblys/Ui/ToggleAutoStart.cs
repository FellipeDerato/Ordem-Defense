using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAutoStart : MonoBehaviour
{

    public GameObject SpawnerObj;



    void ClicouSim()
    {
        SpawnerObj.GetComponent<WaveSpawner>().AutoStartBOOL = true;
        
    }

    void ClicouNao()
    {
        SpawnerObj.GetComponent<WaveSpawner>().AutoStartBOOL = false;

    }


}
