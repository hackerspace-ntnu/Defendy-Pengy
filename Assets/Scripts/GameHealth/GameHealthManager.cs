using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHealthManager : MonoBehaviour {
    public IGameManager gameManager;
    // when "gameHealth" gets decreased to 0, the player loses the game.
    private int gameHealth; // we should have 20 penguins on the water

	void Start() {
        foreach (Transform child in transform) {
			var comp = child.gameObject.GetComponent<GameHealthIndicationItem>();
        }
    }

    public void IncreaseGameHealth(uint health) { //should not be used yet
        gameHealth += (int)health;
        //generate penguins
    }

    public void DecreaseGameHealth(uint health) {
        gameHealth -= (int)health;
        //remove number of pengies by the value of health
    }
}
