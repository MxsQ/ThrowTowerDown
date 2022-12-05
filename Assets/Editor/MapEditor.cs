using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[CustomEditor(typeof(MapGeneretor))]
public class MapEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MapGeneretor map = target as MapGeneretor;

        if (DrawDefaultInspector())
        {

            map.BuildTower();
        }
    }
}
