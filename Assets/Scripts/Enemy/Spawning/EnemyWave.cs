using System.Collections.Generic;
//Timmy Chan

/* Directions:
 *  - different Enemy types can be put into the enemies[]
 *  - spawnDelay is the delay in second after each spawn.
 *  - spawnDelay needs at least one value
 */

public class EnemyWave
{
	public List<int> spawnID = new List<int>();
	public List<EnemySpawner.EnemyType> enemies = new List<EnemySpawner.EnemyType>(); //Enemy component of prefabs of different enemies
	public List<float> spawnDelay = new List<float>();
	private int nextSpawnIndex = 0;  //index of the current spawned enemy

	public EnemyWave(int[] spawnerID, EnemySpawner.EnemyType[] enemies, float[] spawnDelay)
	{ //works
		foreach (int spawnID in spawnerID)
			this.spawnID.Add(spawnID);
		foreach (EnemySpawner.EnemyType enemy in enemies)
			this.enemies.Add(enemy);
		foreach (float delay in spawnDelay)
			this.spawnDelay.Add(delay);
	}

	public bool IsAllSpawned()
	{ //works
		return (nextSpawnIndex >= enemies.Count);
	}

	public float GetSpawnDelay()
	{ //works
		if (nextSpawnIndex - 1 >= spawnDelay.Count)
			return spawnDelay[spawnDelay.Count - 1];
		return spawnDelay[nextSpawnIndex - 1];
	}
	public List<object> GetNextEnemy()
	{
		EnemySpawner.EnemyType nextEnemy = EnemySpawner.EnemyType.Null; //null for enums
		int nextSpawnID = -1; //default
		List<object> result = new List<object>();
		if (nextSpawnIndex < enemies.Count)
		{
			nextSpawnID = spawnID[nextSpawnIndex];
			nextEnemy = enemies[nextSpawnIndex];
			nextSpawnIndex += 1;
		}

		result.Add(nextSpawnID);
		result.Add(nextEnemy);

		return result;
	}
}
