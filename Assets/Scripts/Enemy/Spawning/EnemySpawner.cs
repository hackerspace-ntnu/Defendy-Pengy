//Timmy Chan

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public enum EnemyType {Wolf, Bear, Fish, Pig, Null};
	public Enemy[] enemyPrefabs;
	public Transform enemyManager;
	public Transform goal;
	public SpawnManager spawnManager;


	private bool isSpawning = false;
	private float speedMultiplier = 1;

	private EnemyWave spawningWave;
	private float timeToNextSpawn = 0f;

	// Update is called once per frame
	void Update () {
		//stop spawning when the game is over??
		if (isSpawning) {
			timeToNextSpawn -= Time.deltaTime;
			if (timeToNextSpawn <= 0f){
				if (!SpawnEnemy(spawningWave.GetNextEnemy(), speedMultiplier)){
					//no more enemies to spawn
					isSpawning = false;
					spawningWave = null;
					spawnManager.WaveEnded();
					return;
				}
				timeToNextSpawn += spawningWave.GetSpawnDelay ();
			}
		}
	}
	public bool SpawnEnemy(EnemyType enemyType,float speed){
		if (enemyType == EnemyType.Null){
			print("Enemy not defined");
			return false;
		}
		//replace the "prefab enemy variable" with a real enemy
		Enemy enemy = Instantiate (GetPrefab(enemyType).gameObject, transform.position, Quaternion.identity).GetComponent<Enemy>();
		enemy.transform.parent = enemyManager;
		enemy.goal = goal;
		return true;
	}

	public void StartSpawningWave(EnemyWave wave){ //spawns an enemy in next frame
		isSpawning = true;
		timeToNextSpawn = 0f;
		spawningWave = wave;
	}


	private Enemy GetPrefab(EnemyType enemyType){
		return enemyPrefabs [(int)enemyType];
	}

	public void StopSpawning(){
		isSpawning = false;
	}

}
