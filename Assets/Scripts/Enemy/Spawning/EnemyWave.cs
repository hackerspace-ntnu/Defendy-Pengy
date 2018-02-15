using System.Collections.Generic;
//Timmy Chan

/* Directions:
 *  - spawnDelay is the delay in second after each spawn.
 */

public class EnemyWave
{
	public struct Spawn
	{
		public readonly int spawnerID;
		public readonly Enemy.Type enemy;
		public readonly float delay;

		public Spawn(int spawnerID, Enemy.Type enemy, float delay)
		{
			this.spawnerID = spawnerID;
			this.enemy = enemy;
			this.delay = delay;
		}
	}

	private Spawn[] spawns;

	private int currentSpawnIndex = -1;

	public EnemyWave(List<Spawn> spawns)
	{
		this.spawns = spawns.ToArray();
	}

	public bool HasNext()
	{
		return currentSpawnIndex + 1 < spawns.Length;
	}

	public Spawn GetNextSpawn()
	{
		currentSpawnIndex++;
		return spawns[currentSpawnIndex];
	}
}
