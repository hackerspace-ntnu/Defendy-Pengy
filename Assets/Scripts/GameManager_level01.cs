//Timmy Chan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager_level01 : MonoBehaviour, IGameManager {
	public SpawnManager spawnManager;
	public GameHealthManager gameHealthManager;
	public Transform enemyManager;
	public GameObject longbow;
	public GameUI_ImportantMessage importantMessage;

	private float timeToStart = 3f;
	public bool started = false;
	public bool levelEnded = false;
	// Use this for initialization
	void Start () {
		gameHealthManager.gameManager = (IGameManager)this;
	}

	// Update is called once per frame
	void Update() {
		// TEMPORARY DEVELOPER HOTKEY:
		if (Input.GetKeyDown(KeyCode.W))
			GameStart();

		if (!levelEnded) {
			if (!started) {
				if (Input.GetKeyUp(KeyCode.A))
					GameStart();
				if (Input.GetKeyUp(KeyCode.S))
					GameLost();
				if (timeToStart >= 0) {
					//timeToStart -= Time.deltaTime;
				} else {
					//GameStart ();
				}
			}
			if (started) {
				//if there are no more lives
				/*if (gameHealthManager.RemainingGameHealth () <= 0) {
					GameLost ();
					levelEnded = true;
				}*/
				//if there are no more enemies to be spawned
				if (spawnManager.RemainingWavesCount () == 0) {
					//if there are no more enemies alive
					if (enemyManager.childCount == 0) {
						GameWin ();
						levelEnded = true;
					}
				}
			}
		}
		else {
			//what to do?
		}
	}

	public void GameLost() {
		levelEnded = true;
		print("You have lost the game");
		spawnManager.StopSpawning();
		importantMessage.Show("Game Over");
		//change scene??
	}

	public void GameStart() {
		if (started)
			return;

		started = true;
		print ("Game Start!");
		spawnManager.StartSpawningWaves();
	}

	public void GamePause() {
		throw new System.NotImplementedException();
	}

	public void GameRestart() {
		throw new System.NotImplementedException();
	}

	public void GameWin() {
		levelEnded = true;
		print("You have won the game");
	}
}
