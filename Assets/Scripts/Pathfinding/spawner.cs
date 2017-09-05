using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
	public GameObject enemyTest;
	public Transform goal;
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
		a += Time.deltaTime;
		if (a > 2f) {
			print("new enemy coming!");
			a -= 2f;
			var newEnemy = Instantiate (enemyTest, pos, Quaternion.identity);
			newEnemy.GetComponent<MoveTo>().goal = goal;
		}
	}
}
