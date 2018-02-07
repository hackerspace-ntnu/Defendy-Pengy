﻿//Timmy Chan
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public enum EnemyType { Wolf, Bear, Fox, Seal, Muskox, PolarBear, Null };
	public Enemy[] enemyPrefabs;
	public Transform enemyManager;
	public Transform goal;
	public SpawnManager spawnManager;
	public int spawnID;

	public bool SpawnEnemy(EnemyType enemyType, float speed)
	{
		if (enemyType == EnemyType.Null)
		{
			Debug.LogError("Enemy " + enemyType + " not defined");
			return false;
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
