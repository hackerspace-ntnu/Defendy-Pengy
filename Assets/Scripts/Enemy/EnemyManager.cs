using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameHealthManager gameHealthManager;

	public bool AreAllEnemiesDead()
	{
		return transform.childCount == 0;
	}

	public void ReachedGoal(uint n)
	{
		gameHealthManager.DecreaseGameHealth(n);
	}
}
