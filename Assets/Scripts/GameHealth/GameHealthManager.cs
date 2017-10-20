using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHealthManager : MonoBehaviour {
	public IGameManager gameManager;
	// when "gameHealth" gets decreased to 0, the player loses the game.
	private int gameHealth; // we should have 20 penguins on the water
	private List<IGameHealthIndicationItem> gameHealthIndicationItems = new List<IGameHealthIndicationItem>();

	void Start() {
		gameHealth = transform.childCount;
		foreach (Transform child in transform) {
			var comp = child.gameObject.GetComponent<IGameHealthIndicationItem>();
			gameHealthIndicationItems.Add (comp);
			//gameHealthIndicationItems.Add (child.gameObject);
		}
	}
	public void SetGameHealth(uint health) { //should not be used yet
		gameHealth = (int)health;
	}

	public void IncreaseGameHealth(uint health) { //should not be used yet
		gameHealth += (int)health;
		//generate penguins
	}

	public void DecreaseGameHealth(uint health) {
		if (gameHealthIndicationItems.Count <= 0)
			return;

		Destroy (gameHealthIndicationItems [0].GetGameObject());
		gameHealthIndicationItems.RemoveAt(0);
		gameHealth -= (int)health;

		if (gameHealth <= 0)
			gameManager.GameLost ();
		//remove number of pengies by the value of health
	}
	public int RemainingGameHealth(){
		return gameHealth;
	}
}
