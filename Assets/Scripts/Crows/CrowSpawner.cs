using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSpawner : MonoBehaviour {
	public Crow crowPrefab;
	public Transform[] goals;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnCrows() {
		for (int i = 0; i < goals.Length; i++) {
			Crow crow = Instantiate (crowPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Crow> ();
			crow.transform.eulerAngles = new Vector3 (0f, -45f, 0f);
			crow.GetComponent<Crow> ().goal = goals [i].transform;
			crow.transform.parent = gameObject.transform;
		}
	}
	
}
