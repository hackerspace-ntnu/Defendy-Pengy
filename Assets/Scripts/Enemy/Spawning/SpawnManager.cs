using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public enum EnemyType { Wolf, Bear, Fox, Seal, Muskox, Fish, Pig, Null };
	public TextAsset waveFile;
	private List<EnemyWave> waves;
	EnemyWave currentWave;
	private List<float> wavesDelay;

	public EnemyManager enemyManager;
	public List<EnemySpawner> spawners;
	private int currentWaveIndex = 0;
	private float timeToNextWave = 0f;
	private bool waveSpawning = false;
	private bool isSpawningStarted = false;
	private float timeToNextSpawn = 0f;
	private float speedMultiplier = 1;

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
					List<object> nextSpawn = currentWave.GetNextEnemy();
					int nextSpawnID = (int)nextSpawn[0];
					if (nextSpawnID == -1)
					{
						WaveEnded();
						return;
					}
					EnemySpawner.EnemyType nextEnemy = (EnemySpawner.EnemyType)nextSpawn[1];
					spawners[nextSpawnID].SpawnEnemy(nextEnemy, speedMultiplier);
					timeToNextSpawn += currentWave.GetSpawnDelay();
				}
				timeToNextSpawn -= Time.deltaTime;
			}
			timeToNextWave -= Time.deltaTime;
		}
	}

	public void StartSpawningWaves()
	{ //called from gameManager
		wavesDelay = new List<float>();
		waves = WaveParser.ParseWaveFile(waveFile);
		wavesDelay.Add(10f);
		isSpawningStarted = true;
	}

	float WaveDelay()
	{
		if (wavesDelay.Count <= currentWaveIndex)
			return wavesDelay[wavesDelay.Count - 1];

		return wavesDelay[currentWaveIndex];
	}

	public void WaveEnded()
	{ // always called from spawner, when the wave ends
		waveSpawning = false;
		timeToNextWave = WaveDelay();
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
	}

	public void StopSpawning()
	{
		isSpawningStarted = false;
	}
}
