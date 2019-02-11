using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Kosmos.Modeling;

[CustomEditor(typeof(CombineMesh))]
public class MeshCombinerEditor : Editor 
{
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if(GUILayout.Button("Combine mesh"))
		{
			CombineMesh cm = (CombineMesh)target;
			cm.CombineMeshes ();
		}

		if(GUILayout.Button("Destroy child renderers"))
		{
			CombineMesh cm = (CombineMesh)target;
			cm.RemoveChildRenderers ();
		}
	}
}
