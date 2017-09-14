using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (0, 1 + 0.1f * Mathf.Sin (0.2f * Time.time % (2 * Mathf.PI)), 0);
		transform.Rotate (Vector3.up * (10 * Time.deltaTime + 0.08f * Mathf.Sin(Time.time % (2 * Mathf.PI) ) ));
	}
}
