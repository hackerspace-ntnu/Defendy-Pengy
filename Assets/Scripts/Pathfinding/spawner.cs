using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
	public GameObject enemyTest;

	//private Vector3 pos= transform.position;
	private float a;
	public Transform transformSpawner;
	private Vector3 pos;
	// Use this for initialization
	void Start () {
		pos = transformSpawner.position;
	}
	
	// Update is called once per frame
	void Update () {
		a = a + 1f;
		print (Time.deltaTime);
		if (a > 120f) {
			a = 0f;
			Instantiate (enemyTest, pos, Quaternion.identity);
		}
	}
}
