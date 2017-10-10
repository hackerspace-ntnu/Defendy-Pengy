//Timmy Chan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_level01 : MonoBehaviour, IGameManager {
	public SpawnManager spawnManager;
    public GameHealthManager gameHealthManager;

    public float timeToStart = 8f;
    public bool started = false;
    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update() {
        if(!started) { 
            if(timeToStart > 0f) {
                timeToStart -= Time.deltaTime;
            } else {
                started = true;
				print ("gameStart");
                GameStart();
            }
        }
	}

    public void GameLost() {
        print("You have lost the game");
        //change scene??
    }

    public void GameStart() {
        spawnManager.StartSpawningWaves();

    }

    public void GamePause() {
        throw new System.NotImplementedException();
    }

    public void GameRestart() {
        throw new System.NotImplementedException();
    }

    public void GameWin() {
        throw new System.NotImplementedException();
    }
}
