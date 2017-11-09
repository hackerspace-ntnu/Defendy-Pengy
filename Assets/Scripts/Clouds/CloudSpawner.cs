using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {
	public Cloud[] cloudPrefabs;
	public int interval;
	private int framesToNextSpawn = 0;

	void Start () {
	}

	void Update () {
		if (framesToNextSpawn <= 0) {
			Cloud cloud = Instantiate (cloudPrefabs [0].gameObject, transform.position, Quaternion.identity).GetComponent<Cloud> ();
			cloud.transform.position = new Vector3(Random.Range(-400f, -100f), 80f, Random.Range(-400f, -100f));
			cloud.transform.eulerAngles = new Vector3(0f, -45f, 0f);
			cloud.transform.parent = GameObject.Find ("Decorations").transform;
			framesToNextSpawn += interval;
		}
		framesToNextSpawn -= 1;
		//print (framesToNextSpawn);
	}
}
