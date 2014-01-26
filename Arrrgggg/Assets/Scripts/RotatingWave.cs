using UnityEngine;
using System.Collections;

public class RotatingWave : MonoBehaviour {

	Vector3 startPos;
	public float magicOffset;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Sin(Time.time + magicOffset) * 0.5f;
		float y = Mathf.Cos(Time.time + magicOffset) * 0.5f;
		transform.position = startPos + new Vector3(x,y,0);
	}
}
