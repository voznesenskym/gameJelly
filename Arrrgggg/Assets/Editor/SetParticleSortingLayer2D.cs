using UnityEngine;
using UnityEditor;
using System.Collections;

public class SetParticleSortingLayer2D : EditorWindow {
	[MenuItem("Utility/Set 2D Sorting Layer")]
	public static void SetParticleLayer2D() {
		GameObject selection = Selection.activeObject as GameObject;
		if (selection.particleSystem) {
			selection.particleSystem.renderer.sortingLayerName = "Foreground";
		}else {
			selection.renderer.sortingLayerName = "Foreground";
		}
	}
}
