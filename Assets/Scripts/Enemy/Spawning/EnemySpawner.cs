//Timmy Chan
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public enum EnemyType { Wolf, Bear, Fox, Seal, PolarBear, Muskox, Null };
	public Enemy[] enemyPrefabs;
	public Transform enemyManager;
	public Transform goal;

	public CrowSpawner crowSpawner;
	private bool firstEnemy = true;

	public bool SpawnEnemy(EnemyType enemyType, float speed)
	{
		if (enemyType == EnemyType.Null)
		{
			Debug.LogError("Enemy " + enemyType + " not defined");
			return false;
		}

		if (firstEnemy)
		{
			if (crowSpawner)
				crowSpawner.SpawnCrows();
			else
				Debug.LogError("Missing crow spawner for " + name);

			firstEnemy = false;
		}

		//replace the "prefab enemy variable" with a real enemy
		Enemy enemy = Instantiate(GetPrefab(enemyType).gameObject, transform).GetComponent<Enemy>();
		enemy.transform.parent = enemyManager;
		enemy.goal = goal;
		return true;
	}

	private Enemy GetPrefab(EnemyType enemyType)
	{
		return enemyPrefabs[(int)enemyType];
	}
}
