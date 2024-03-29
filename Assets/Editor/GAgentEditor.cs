﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using GOAP;
using GOAP.Actions;

[CustomEditor(typeof(GAgentVisual))]
[CanEditMultipleObjects]
public class GAgentVisualEditor : Editor
{


    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        GAgentVisual agent = (GAgentVisual)target;
        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GAgent>().currentAction.actionName);
        GUILayout.Label("Actions: ");
        foreach (GAction a in agent.gameObject.GetComponent<GAgent>()._actions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<WorldStateTypes, int> p in a.preConditions)
                pre += p.Key + ", ";
            foreach (KeyValuePair<WorldStateTypes, int> e in a.effects)
                eff += e.Key + ", ";

            GUILayout.Label("====  " + a.actionName + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> g in agent.gameObject.GetComponent<GAgent>().goals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<WorldStateTypes, int> sg in g.Key.sGoals)
                GUILayout.Label("=====  " + sg.Key);
        }

        GUILayout.Label("Beliefs: ");
        foreach (KeyValuePair<WorldStateTypes, int> b in agent.gameObject.GetComponent<GAgent>().beliefs.GetStates())
        {
            GUILayout.Label("====  " + b.Key + " : " + b.Value);
        }

        // GUILayout.Label("Inventory: ");
        // foreach (KeyValuePair<string, int> i in agent.gameObject.GetComponent<GAgent>().inventory.GetStates())
        // {
        //     GUILayout.Label("====  " + i.Key + " : " + i.Value);
        // }

        serializedObject.ApplyModifiedProperties();
    }
}