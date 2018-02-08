using UnityEngine;

public class CloudSpawner : MonoBehaviour {
	public Cloud[] cloudPrefabs;
	public int frameInterval;
	private int framesToNextSpawn = 0;

	void Start () {
		SpawnCloud (3);
	}

	void Update () {
		if (framesToNextSpawn <= 0) {
			SpawnCloud (1);
			framesToNextSpawn += frameInterval;
		}
		framesToNextSpawn -= 1;
	}

	void SpawnCloud (int quantity) {
		for (int i = 0; i < quantity; i++) {
			Cloud cloud = Instantiate (cloudPrefabs [Random.Range (1, 2)].gameObject, transform.position, Quaternion.identity).GetComponent<Cloud> ();
			//cloud.transform.position = new Vector3 (transform.position.x + Random.Range (-400f, -100f), transform.position.y, Random.Range (-400f, -100f));
			float offset = Random.Range(-250f, 250f);
			cloud.transform.position = new Vector3 (transform.position.x + offset, transform.position.y, transform.position.z - offset);
			cloud.transform.eulerAngles = new Vector3 (-90f, -45f, 0f);
			cloud.transform.parent = GameObject.Find ("CloudSpawner").transform;
		}
	}

}
// Optimal position for level 1 is (-503, 134, -389)
