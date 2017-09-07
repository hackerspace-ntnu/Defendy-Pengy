using System.Collections.Generic;

//Timmy Chan

/* Directions:
 *  - different Enemy types can be put into the enemies[]
 *  - spawnDelay is the delay in second after each spawn.
 *  - spawnDelay needs at least one value
 */

public class EnemyWave{
	public List<Enemy> enemies; //Enemy component of prefabs of different enemies
	public List<float> spawnDelay;

	public float speed; 

	private int spawnIndex = -1;  //index of the current spawned enemy



	public EnemyWave(Enemy[] enemies, float[] spawnDelay){
		foreach (Enemy enemy in enemies)
			this.enemies.Add (enemy);

		foreach (float delay in spawnDelay)
			this.spawnDelay.Add(delay);
	}

	public EnemyWave(Enemy enemyType, int size, float spawnDelay){
		for(int i = 0; i < size; i++){
			enemies.Add (enemyType);
		}
		this.spawnDelay.Add (spawnDelay);
	}
		
	public bool IsAllSpawned(){
		return (spawnIndex >= enemies.Count);
	}
	public float GetSpawnDelay(){
		if (spawnIndex >= spawnDelay.Count)
			return spawnDelay[spawnDelay.Count];
		return spawnDelay [spawnIndex];
	}
	public Enemy GetNextEnemy(){
		Enemy result = null; //null for enums
		if (spawnIndex < enemies.Count)
			spawnIndex += 1;
			result = enemies [spawnIndex];
		return result;
	}
	public int CurrentSpawnIndex(){
		return spawnIndex;
	}
}
