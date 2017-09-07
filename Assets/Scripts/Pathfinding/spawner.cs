using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject enemyTest;

	//private Vector3 pos= transform.position;
	private float testSpawnDelay;
	public Transform transformSpawner;
	private Vector3 pos;


	private bool isSpawning = false;
	private float speed;
	private float timeSinceSpawnWave = 0f;
	private int spawningDelay;
	private Enemy[] spawningWave;
	private int spawnListPosition = 0;


	// Use this for initialization
	void Start () {
		pos = transformSpawner.position;
	}
	
	// Update is called once per frame
	void Update () {
		testSpawnDelay = testSpawnDelay + 1f * Time.deltaTime * 60f;
		print (Time.deltaTime);
		if (testSpawnDelay > 120f) {
			testSpawnDelay = 0f;
			Instantiate (enemyTest, pos, Quaternion.identity);
		}
		if (isSpawning) {
			timeSinceSpawnWave += Time.deltaTime;
			if (timeSinceSpawnWave * 1000 > spawningDelay) {
				Instantiate (spawningWave [spawnListPosition], pos, Quaternion.identity);
				spawnListPosition += 1;
				if (spawnListPosition == spawningWave.Length) {
					isSpawning = false;

				}
			}
			
		}
	}



	void spawnEnemy(Enemy enemy,float speed){
		Instantiate (enemy, pos, Quaternion.identity);
		enemy.setSpeed (speed);
	}
	void spawnWave(Enemy[] wave, float speed,int delayInMs){
		timeSinceSpawnWave = 0f;
		spawnListPosition = 0;
		isSpawning = true;
		spawningWave = wave;
		spawningDelay = delayInMs;
	}
}
