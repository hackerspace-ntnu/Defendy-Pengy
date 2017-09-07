using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public enum EnemyType {Wolf, Fish, IceBear, Pig};
	public GameObject enemy;
	public Transform goal;
	public float spawnFrequency = 3;

	public Enemy[] enemyPrefabs;

	private bool isSpawning = false;
	private float speed;

	private EnemyWave spawningWave;
	private float timeToNextSpawn = 0f;

	// Update is called once per frame
	void Update () {
		if (isSpawning) {
			timeToNextSpawn -= Time.deltaTime;
			if (timeToNextSpawn <= 0f){
				if (!spawnEnemy(spawningWave.GetNextEnemy(), spawningWave.speed)){
					//no more enemies to spawn
					isSpawning = false;
					spawningWave = null;
				}
				timeToNextSpawn += spawningWave.GetSpawnDelay ();
			}
		}
	}
	public bool spawnEnemy(Enemy enemy,float speed){
		if (enemy == null){
			print("Enemy not defined");
			return false;
		}
		//replace the "prefab enemy variable" with a real enemy
		enemy = Instantiate (enemy.gameObject, transform.position, Quaternion.identity).GetComponent<Enemy>();
		enemy.goal = goal;
		return true;
	}

	public void StartSpawningWave(EnemyWave wave){
		isSpawning = true;
		spawningWave = wave;
		spawnEnemy (wave.GetNextEnemy (), wave.speed);
	}
}
