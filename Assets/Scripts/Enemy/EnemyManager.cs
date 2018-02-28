using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private static EnemyManager THIS;

	public GameHealthManager gameHealthManager;
	public Transform enemyGoal;

	private float[] enemyLastSoundPlayTime = new float[Enum.GetNames(typeof(Enemy.Type)).Length];

	void Awake()
	{
		if (THIS == null)
		{
			THIS = this;
		} else if (THIS != this)
		{
			Debug.LogWarning("There's more than one EnemyManager in the scene!");
			Destroy(gameObject);
		}
	}

	public void OnNewWave()
	{
		Array.Clear(enemyLastSoundPlayTime, 0, enemyLastSoundPlayTime.Length);
	}

	public void OnEnemySpawned(Enemy enemy)
	{
		enemy.transform.parent = transform;
		enemy.goal = enemyGoal;
	}

	public void EnemyPlaySoundTimeManaged(Enemy enemy, Action soundFunc, float maxSilenceDuration)
	{
		float currentTime = Time.time;
		float lastSoundPlayTime = enemyLastSoundPlayTime[(int)enemy.type];

		if (lastSoundPlayTime == 0f // Play sound if the enemy is the first of its type this wave,
			|| currentTime > lastSoundPlayTime + maxSilenceDuration) // or if it's been more than maxSilenceDuration since an enemy of its type last played
		{
			soundFunc();
			enemyLastSoundPlayTime[(int)enemy.type] = currentTime;
		}
	}

	public void OnEnemyPlayedSound(Enemy enemy)
	{
		enemyLastSoundPlayTime[(int)enemy.type] = Time.time;
	}

	public bool AreAllEnemiesDead()
	{
		return transform.childCount == 0;
	}

	public void EnemyReachedGoal(uint healthLost)
	{
		gameHealthManager.DecreaseGameHealth(healthLost);
	}
}
