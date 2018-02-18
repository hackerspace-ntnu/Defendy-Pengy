//Timmy Chan
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public EnemyManager enemyManager;
	public CrowSpawner crowSpawner;

	private bool firstEnemy = true;

	public void SpawnEnemy(Enemy enemy)
	{
		if (firstEnemy)
		{
			if (crowSpawner)
				crowSpawner.SpawnCrows();
			else
				Debug.LogError("Missing crow spawner for " + name);

			firstEnemy = false;
		}

		enemyManager.OnEnemySpawned(Instantiate(enemy.gameObject, transform).GetComponent<Enemy>());
	}
}
