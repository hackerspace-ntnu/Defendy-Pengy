using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private static EnemyManager THIS;

	public GameHealthManager gameHealthManager;

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

	public bool AreAllEnemiesDead()
	{
		return transform.childCount == 0;
	}

	public void EnemyReachedGoal(uint healthLost)
	{
		gameHealthManager.DecreaseGameHealth(healthLost);
	}
}
