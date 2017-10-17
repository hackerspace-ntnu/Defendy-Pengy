using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHealthIndicationPenguin : MonoBehaviour, GameHealthIndicationItem {
	private int health = 1;
    public int GetHealthAmount() {
        return health;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDestroy(){
		// FIXME:
		//transform.parent.GetComponent<GameHealthManager>().DecreaseGameHealth ((uint)health);
	}
	public GameObject GetGameObject() {
		return gameObject;
	}
}
