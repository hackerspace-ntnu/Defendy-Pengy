using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatOnWaterStatic : MonoBehaviour {
	public float moveAmount = 1f;
	private float originalPosY;
	// Use this for initialization
	void Start () {
		originalPosY = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x,originalPosY + Mathf.Sin (Time.time)*0.04f*moveAmount,transform.position.z);
	}
}
