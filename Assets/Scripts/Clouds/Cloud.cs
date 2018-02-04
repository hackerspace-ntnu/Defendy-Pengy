using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1000f);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate (0.02f, 0f, 0f);
	}
}
