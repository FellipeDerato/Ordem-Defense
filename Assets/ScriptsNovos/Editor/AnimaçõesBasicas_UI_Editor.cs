using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


[CustomEditor(typeof(AnimaçõesBasicas_UI))]
public class AnimaçõesBasicas_UI_Editor : Editor
{
    public bool showFoldout1 = false;
    public bool showFoldout2 = false;
    void OnGUI()
    {
        if (showFoldout1)
        {
            showFoldout1 = false;
        }
        else if (!showFoldout1)
        {
            showFoldout1 = true;
        }
        if (showFoldout2)
        {
            showFoldout2 = false;
        }
        else if (!showFoldout2)
        {
            showFoldout2 = true;
        }
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    public override void OnInspectorGUI()
    {
        AnimaçõesBasicas_UI coisa = (AnimaçõesBasicas_UI)target;
        GUILayout.BeginHorizontal();
        showFoldout1 = EditorGUILayout.BeginFoldoutHeaderGroup(showFoldout1, "In / Out / Mid" );
        EditorGUILayout.EndFoldoutHeaderGroup();
        showFoldout2 = EditorGUILayout.BeginFoldoutHeaderGroup(showFoldout2, "Hover / Click");
        EditorGUILayout.EndFoldoutHeaderGroup();
        GUILayout.EndHorizontal();



        if (showFoldout1)
        {
            GUILayout.Space(10);


            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            coisa.in_anim_Zoom = GUILayout.Toggle(coisa.in_anim_Zoom, "In Zoom");
            coisa.in_anim_Giro = GUILayout.Toggle(coisa.in_anim_Giro, "In Giro");
            coisa.in_anim_Opacidade = GUILayout.Toggle(coisa.in_anim_Opacidade, "In Opacidade");
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            coisa.out_anim_Zoom = GUILayout.Toggle(coisa.out_anim_Zoom, "Out Zoom");
            coisa.out_anim_Giro = GUILayout.Toggle(coisa.out_anim_Giro, "Out Giro");
            coisa.out_anim_Opacidade = GUILayout.Toggle(coisa.out_anim_Opacidade, "Out Opacidade");
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            coisa.mid_anim_Pulse = GUILayout.Toggle(coisa.mid_anim_Pulse, "Mid Pulse");
            coisa.mid_anim_Shake = GUILayout.Toggle(coisa.mid_anim_Shake, "Mid Shake");
            coisa.mid_anim_Wobble = GUILayout.Toggle(coisa.mid_anim_Wobble, "Mid Wobble");
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            coisa.VelocidadeAnimação_In_Out = EditorGUILayout.Slider(("Velô In/Out"), coisa.VelocidadeAnimação_In_Out, 0.01f, 30f);
            coisa.VelocidadeAnimação_Mid = EditorGUILayout.Slider(("Velô Mid"), coisa.VelocidadeAnimação_Mid, 0.01f, 30f);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Ativar GameObject"))
            {
                coisa.Ativar();
            }
            if (GUILayout.Button("Desativar GameObject"))
            {
                coisa.Desativar();
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
        if (showFoldout2)
        {
            GUILayout.Space(10);


            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            coisa.hover_anim_Shake = GUILayout.Toggle(coisa.hover_anim_Shake, "Hover Shake");
            coisa.hover_anim_Zoom = GUILayout.Toggle(coisa.hover_anim_Zoom, "Hover Zoom");
            coisa.hover_anim_Wobble = GUILayout.Toggle(coisa.hover_anim_Wobble, "Hover Wobble");
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            coisa.click_anim_Shake = GUILayout.Toggle(coisa.click_anim_Shake, "Click Shake");
            coisa.click_anim_Zoom = GUILayout.Toggle(coisa.click_anim_Zoom, "Click Zoom");
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            coisa.ForçaHover = EditorGUILayout.Slider(("Força Anim Hover"), coisa.ForçaHover, 0.01f, 30f);
            coisa.ForçaClick = EditorGUILayout.Slider(("Força Anim Click"), coisa.ForçaClick, 0.01f, 30f);

            GUILayout.Space(10);

        }


        GUILayout.Space(50);

        //base.OnInspectorGUI();
    }
}
