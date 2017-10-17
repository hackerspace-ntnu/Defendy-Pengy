using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	public GameHealthManager gameHealthManager;
	// Use this for initialization
	public void ReachedGoal(uint n) {
		gameHealthManager.DecreaseGameHealth (n);
	}
}
