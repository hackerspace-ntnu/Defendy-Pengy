using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	public TextAsset waveFile;
	private List<EnemyWave> waves;
	private List<float> wavesDelay;

	public EnemyManager enemyManager;
	public EnemySpawner spawner;
	private int currentWaveIndex = 0;
	private float timeToNextWave = 0f;
	private bool isWaveEnded = true;
	private bool isSpawningStarted = false;

	void Update() {
		if(isSpawningStarted) {
			if(isWaveEnded) {
				if(timeToNextWave <= 0f && enemyManager.AreAllEnemiesDead())
				{
					spawner.StartSpawningWave(waves[currentWaveIndex]);
					isWaveEnded = false;
				}
				timeToNextWave -= Time.deltaTime;
			}
		}
	}

	public void StartSpawningWaves() { //called from gameManager
		wavesDelay = new List<float> ();
		waves = WaveParser.ParseWaveFile(waveFile);
		wavesDelay.Add(10f);
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
		currentWaveIndex++;
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
