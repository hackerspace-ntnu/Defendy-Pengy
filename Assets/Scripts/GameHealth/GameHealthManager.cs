using System.Collections.Generic;
using UnityEngine;

public class GameHealthManager : MonoBehaviour
{
	private static GameHealthManager THIS;

	public IGameManager gameManager;

	public float warnPlayer_enemyDistance = 20f;

	// when "gameHealth" gets decreased to 0, the player loses the game.
	private int gameHealth; // we should have 20 penguins on the water
	private List<IGameHealthIndicationItem> gameHealthIndicationItems = new List<IGameHealthIndicationItem>();

	public static float GetWarnPlayerDistance()
	{
		return THIS.warnPlayer_enemyDistance;
	}

	void Awake()
	{
		if (THIS == null)
		{
			THIS = this;
		} else if (THIS != this)
		{
			Debug.LogWarning("There's more than one GameHealthManager in the scene!");
			Destroy(gameObject);
		}
	}

	void Start()
	{
		gameHealth = transform.childCount;
		foreach (Transform child in transform)
		{
			var comp = child.gameObject.GetComponent<IGameHealthIndicationItem>();
			gameHealthIndicationItems.Add(comp);
			//gameHealthIndicationItems.Add (child.gameObject);
		}
	}

	//should not be used yet
	public void SetGameHealth(uint health)
	{
		gameHealth = (int)health;
	}

	//should not be used yet
	public void IncreaseGameHealth(uint health)
	{
		gameHealth += (int)health;
		//generate penguins
	}

	public void DecreaseGameHealth(uint health)
	{
		if (gameHealthIndicationItems.Count <= 0)
			return;

		gameHealthIndicationItems[0].Kill();
		gameHealthIndicationItems.RemoveAt(0);
		gameHealth -= (int)health;

		if (gameHealth <= 0)
			gameManager.GameLost();
		// TODO: remove number of pengies by the value of health
	}

	public int RemainingGameHealth()
	{
		return gameHealth;
	}
}
