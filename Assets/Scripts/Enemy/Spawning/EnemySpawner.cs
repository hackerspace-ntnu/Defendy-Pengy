//Timmy Chan
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public Transform enemyManager;
	public Transform goal;

	public CrowSpawner crowSpawner;
	private bool firstEnemy = true;

	public bool SpawnEnemy(Enemy enemy)
	{
		if (firstEnemy)
		{
			if (crowSpawner)
				crowSpawner.SpawnCrows();
			else
				Debug.LogError("Missing crow spawner for " + name);

			firstEnemy = false;
		}

		Enemy spawnedEnemy = Instantiate(enemy.gameObject, transform).GetComponent<Enemy>();
		spawnedEnemy.transform.parent = enemyManager;
		spawnedEnemy.goal = goal;
		return true;
	}
}
