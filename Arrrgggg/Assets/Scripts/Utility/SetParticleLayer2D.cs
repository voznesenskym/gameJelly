using UnityEngine;
using System.Collections;

public class SetParticleLayer2D : MonoBehaviour {

	void Start() {
		particleSystem.renderer.sortingLayerName = "Foreground";
	}
}
