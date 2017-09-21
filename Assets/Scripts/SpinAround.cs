using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.Rotate (Vector3.up * Random.Range (0f, 360f));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, 1 + 0.1f * Mathf.Sin (0.2f * Time.time) + 0.7f, transform.position.z);
		transform.Rotate (Vector3.up * (10 * Time.deltaTime + 0.08f * Mathf.Sin(Time.time) ));
	}
}
