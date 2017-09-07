using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public EnemySpawner spawner;
	public GameObject enemy;
	// Use this for initialization
	public int n = 100;
	public bool isSpawning = false;
	public float spawnDelay = 2f;
	public float timeAfterSpawn = 0f;
	void Start () {
		//EnemyWave wave = new EnemyWave (EnemySpawner.EnemyType.Wolf, 10, 2f);
		//spawner.StartSpawningWave (wave);
		isSpawning = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSpawning){
			timeAfterSpawn += Time.deltaTime;
			if (timeAfterSpawn >= spawnDelay){
				timeAfterSpawn -= spawnDelay;
				Instantiate (enemy, transform.position, Quaternion.identity);
			}
		}
	}
}
