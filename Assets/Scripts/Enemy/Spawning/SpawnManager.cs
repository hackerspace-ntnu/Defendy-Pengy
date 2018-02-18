using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	private static SpawnManager THIS;

	public TextAsset waveFile;
	public float timeBetweenWaves = 10f;
	private List<EnemyWave> waves;
	private EnemyWave currentWave;

	public EnemyManager enemyManager;
	public List<EnemySpawner> spawners;

	public Enemy[] enemyPrefabs;

	private int currentWaveIndex = 0;
	private float timeToNextWave = 0f;
	private bool waveSpawning = false;
	private bool isSpawningStarted = false;
	private float timeToNextSpawn = 0f;

	void Awake()
	{
		if (THIS == null)
		{
			THIS = this;
		} else if (THIS != this)
		{
			Debug.LogWarning("There's more than one SpawnManager in the scene!");
			Destroy(gameObject);
		}

		// Sort enemy prefabs

		if (enemyPrefabs == null || enemyPrefabs.Length < 1)
		{
			Debug.LogError(name + " is missing enemy prefabs to spawn.");
			Destroy(this);
			return;
		}

		if (Array.IndexOf(enemyPrefabs, null) > -1)
		{
			Debug.LogError(name + "'s enemy prefabs should not contain null values.");
			Destroy(this);
			return;
		}

		// Places each enemy in the original enemyPrefabs array on its respective index in the new sorted array
		Enemy[] enemyPrefabs_sorted = new Enemy[Enum.GetNames(typeof(Enemy.Type)).Length];
		foreach (Enemy enemy in enemyPrefabs)
			enemyPrefabs_sorted[(int)enemy.type] = enemy;

		enemyPrefabs = enemyPrefabs_sorted;
	}

	void Update()
	{
		if (isSpawningStarted)
		{
			if (timeToNextWave <= 0f && enemyManager.AreAllEnemiesDead())
			{
				StartNewWave();
			}
			if (waveSpawning)
			{
				if (timeToNextSpawn <= 0f)
				{
					SpawnNextEnemy();
				}
				timeToNextSpawn -= Time.deltaTime;
			}
			timeToNextWave -= Time.deltaTime;
		}
	}

	private void SpawnNextEnemy()
	{
		if (currentWave.HasNext())
		{
			EnemyWave.Spawn nextSpawn = currentWave.GetNextSpawn();
			spawners[nextSpawn.spawnerID].SpawnEnemy(enemyPrefabs[(int)nextSpawn.enemy]);
			timeToNextSpawn += nextSpawn.delay;
		} else
			WaveEnded();
	}

	public void StartSpawningWaves()
	{ //called from gameManager
		waves = WaveParser.ParseWaveFile(waveFile);
		isSpawningStarted = true;
	}

	public void WaveEnded()
	{ // always called from spawner, when the wave ends
		waveSpawning = false;
		timeToNextWave = timeBetweenWaves;
		currentWaveIndex++;

		if (RemainingWavesCount() <= 0)
			isSpawningStarted = false; //stop spawning
	}

	public int RemainingWavesCount()
	{
		return waves.Count - currentWaveIndex;
	}

	void StartNewWave()
	{
		currentWave = waves[currentWaveIndex];
		waveSpawning = true;
		enemyManager.OnNewWave();
	}

	public void StopSpawning()
	{
		isSpawningStarted = false;
	}
}
