using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Scripts")]
    public ComprarTorre cpt;
    public SeleçãoAgentes sA;
    public GizmoMouse gizmo;
    public AtalhosTeclado ata;
    public Mapa_Master mm;
    public Mapa_UI mu;

    [Header("GameObjects")]
    public GameObject AreaVisivel;
    public GameObject AreaCompravel;


    private void Start()
    {
        if (cpt != null) { cpt.gm = GetComponent<GameMaster>(); }
        if (gizmo != null) { gizmo.gm = GetComponent<GameMaster>(); }
        if (sA != null) { sA.gm = GetComponent<GameMaster>(); }
        if (mm != null) { mm.gm = GetComponent<GameMaster>(); }
        if (mu != null) { mu.gm = GetComponent<GameMaster>(); }
    }
}
