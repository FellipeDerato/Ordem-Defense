using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bala1 : MonoBehaviour
{
    public float pierce = 1;
    Enemy nme;
    MenuUpgd menuU;
    Rigidbody rigido;
    public List<GameObject> Inimigos;
    public Transform transformAcerto;
    public GameObject impactEffect;
    public GameObject Cerne;
    public List<GameObject> enemies1;
    public List<GameObject> enemies2;
    Torre1 torre1;

    [Header("Seek")]
    public int DebuffDano = 0;
    private Transform target;
    public GameObject TargetAtual;


   
}
