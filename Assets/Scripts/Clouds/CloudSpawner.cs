using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {
	public Cloud[] cloudPrefabs;
	public int frameInterval;
	private int framesToNextSpawn = 3000;

	void Start () {
		SpawnCloud (3);
	}

	void Update () {
		if (framesToNextSpawn <= 0) {
			SpawnCloud (1);
			framesToNextSpawn += frameInterval;
		}
		framesToNextSpawn -= 1;
		//print (framesToNextSpawn);
	}

	void SpawnCloud (int quantity) {
		for (int i = 0; i < quantity; i++) {
			Cloud cloud = Instantiate (cloudPrefabs [Random.Range (1, 2)].gameObject, transform.position, Quaternion.identity).GetComponent<Cloud> ();
			cloud.transform.position = new Vector3 (Random.Range (-400f, -100f), 80f, Random.Range (-400f, -100f));
			cloud.transform.eulerAngles = new Vector3 (-90f, -45f, 0f);
			cloud.transform.parent = GameObject.Find ("CloudManager").transform;
		}
	}

}
// Optimal position for level 1 is (-503, 134, -389)

