using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameHealthManager gameHealthManager;

	public bool AreAllEnemiesDead()
	{
		return transform.childCount == 0;
	}

	public void EnemyReachedGoal(uint healthLost)
	{
		gameHealthManager.DecreaseGameHealth(healthLost);
	}
}
