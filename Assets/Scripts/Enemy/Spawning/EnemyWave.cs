using System.Collections.Generic;
using UnityEngine;
//Timmy Chan

/* Directions:
 *  - different Enemy types can be put into the enemies[]
 *  - spawnDelay is the delay in second after each spawn.
 *  - spawnDelay needs at least one value
 */

public class EnemyWave{

	public List<EnemySpawner.EnemyType> enemies = new List<EnemySpawner.EnemyType>(); //Enemy component of prefabs of different enemies
	public List<float> spawnDelay = new List<float>();
	private int nextSpawnIndex = 0;  //index of the current spawned enemy

	public EnemyWave(EnemySpawner.EnemyType[] enemies, float[] spawnDelay){ //works
		foreach (EnemySpawner.EnemyType enemy in enemies)
			this.enemies.Add (enemy);
		foreach (float delay in spawnDelay)
			this.spawnDelay.Add(delay);
	}

	public EnemyWave(EnemySpawner.EnemyType enemyType, int size, float spawnDelay){ //works
		for(int i = 0; i < size; i++)
			enemies.Add (enemyType);
		this.spawnDelay.Add (spawnDelay);
	}
		
	public bool IsAllSpawned(){ //works
		return (nextSpawnIndex >= enemies.Count);
	}

	public float GetSpawnDelay(){ //works
		if (nextSpawnIndex - 1 >= spawnDelay.Count)
			return spawnDelay[spawnDelay.Count-1];
		return spawnDelay [nextSpawnIndex - 1];
	}
	public EnemySpawner.EnemyType GetNextEnemy(){
		EnemySpawner.EnemyType result = EnemySpawner.EnemyType.Null; //null for enums
		if (nextSpawnIndex < enemies.Count)
			result = enemies[nextSpawnIndex];
			nextSpawnIndex += 1;
		return result;
	}
}
