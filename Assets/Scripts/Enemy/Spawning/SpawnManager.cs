using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	private List<EnemyWave> waves;
	private List<float> wavesDelay;
	public EnemySpawner spawner;
	private int currentWaveIndex = 0;
	private float timeToNextWave = 0f;
	private bool isWaveEnded = true;
	private bool isSpawningStarted = false;
	public int level = 1;

	void Update() {
		if(isSpawningStarted) {
			if(isWaveEnded) {
				if(timeToNextWave <= 0f) {
					spawner.StartSpawningWave(waves[currentWaveIndex]);
					isWaveEnded = false;
				}
				timeToNextWave -= Time.deltaTime;
			}
		}
	}

	public void StartSpawningWaves() { //called from gameManager
		waves = new List<EnemyWave>();
		wavesDelay = new List<float> ();
		if(level == 1) {
			waves = WaveParser.ParseWaveFile(@"Assets\Waves\level1.txt");
			wavesDelay.Add(10f);
			/*
			waves.Add(new EnemyWave(EnemySpawner.EnemyType.Wolf, 3, 2f));
			wavesDelay.Add(4f);
			waves.Add(new EnemyWave(EnemySpawner.EnemyType.Wolf, 6, 2f));
			wavesDelay.Add(10f);
			waves.Add(new EnemyWave(EnemySpawner.EnemyType.Wolf, 6, 2f));
			wavesDelay.Add(10f);
			waves.Add(new EnemyWave(EnemySpawner.EnemyType.Wolf, 6, 2f));
			wavesDelay.Add(10f);
			waves.Add(new EnemyWave(EnemySpawner.EnemyType.Wolf, 6, 2f));
			*/
		}
		isSpawningStarted = true;
	}

	float WaveDelay() {
		if (wavesDelay.Count <= currentWaveIndex)
			return wavesDelay[wavesDelay.Count - 1];
		return wavesDelay[currentWaveIndex];
	}
	public void WaveEnded() { // always called from spawner, when the wave ends
		isWaveEnded = true;
		timeToNextWave = WaveDelay();
		currentWaveIndex += 1;
		if (RemainingWavesCount() <= 0)
			isSpawningStarted = false; //stop spawning
	}

	public int RemainingWavesCount() {
		return waves.Count - currentWaveIndex;
	}

	public void StopSpawning(){
		isSpawningStarted = false;
		spawner.StopSpawning();
	}

}
