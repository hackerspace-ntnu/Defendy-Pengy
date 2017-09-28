//Timmy Chan

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public enum EnemyType {Wolf, Fish, IceBear, Pig, Null};
	public Enemy[] enemyPrefabs;
	public Transform enemyContainer;
	public Transform goal;
	public float spawnFrequency = 3;


	private bool isSpawning = false;
	private float speedMultiplier = 1;

	private EnemyWave spawningWave;
	private float timeToNextSpawn = 0f;
	private float currentDelayBetweenSpawn=0f;

	// Update is called once per frame
	void Update () {
		if (isSpawning) {
			timeToNextSpawn -= Time.deltaTime;
			//print (timeToNextSpawn);
			if (timeToNextSpawn <= 0f){
				
				print ("problem");
				if (!SpawnEnemy(spawningWave.GetNextEnemy(), speedMultiplier)){
					//no more enemies to spawn
					isSpawning = false;
					spawningWave = null;
				}
				timeToNextSpawn += spawningWave.GetSpawnDelay ();
				/*
				if(spawningWave.IsArrayDelays){
					timeToNextSpawn += spawningWave.GetSpawnDelay ();  //Does not work, fixed with currentDelayBetweenSpawn saving the first delay
					//^^argument out of range exception
				}else{
					timeToNextSpawn = currentDelayBetweenSpawn;
					//print (spawningWave.GetSpawnDelay ());
				}*/



			}
		}
	}
	public bool SpawnEnemy(EnemyType enemyType,float speed){
		if (enemyType == EnemyType.Null){
			print("Enemy not defined");
			return false;
		}
		//replace the "prefab enemy variable" with a real enemy
		Enemy enemy = Instantiate(GetPrefab(enemyType).gameObject, transform.position, Quaternion.identity, enemyContainer).GetComponent<Enemy>();
		enemy.goal = goal;
		return true;
	}

	public void StartSpawningWave(EnemyWave wave){
		isSpawning = true;
		spawningWave = wave;
		SpawnEnemy (wave.GetNextEnemy (), speedMultiplier);
		timeToNextSpawn = wave.GetSpawnDelay ();
		currentDelayBetweenSpawn = wave.GetSpawnDelay ();
	}


	private Enemy GetPrefab(EnemyType enemyType){
		return enemyPrefabs [(int)enemyType];
	}



}
