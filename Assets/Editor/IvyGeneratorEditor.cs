using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IvyGenerator))]
public class IvyGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        IvyGenerator ivyGenerator = (IvyGenerator)target;
        
        // array vector
        DrawDefaultInspector();

        bool okBtn = GUILayout.Button("Generate!");
        bool clearBtn = GUILayout.Button("I don't need ma' leafs no more...");

        if (okBtn)
        {
            ivyGenerator.SpawnLeafs();
        }

        if (clearBtn)
        {
            ivyGenerator.DestroyAllLeafs();
        }
    }
}
