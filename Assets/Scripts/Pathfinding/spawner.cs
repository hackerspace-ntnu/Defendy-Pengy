using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
	public GameObject penguin;
	private Vector3 pos= new Vector3(-1f,2.88f,-16.33f);
	private int a;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		a++;
		if (a > 120) {
			a = 0;
			Instantiate (penguin, pos, Quaternion.identity);
		}
	}
}
