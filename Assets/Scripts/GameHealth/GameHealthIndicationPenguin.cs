using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHealthIndicationPenguin : MonoBehaviour, IGameHealthIndicationItem {
	private int health = 1;
    public AudioClip deathSound;
    private AudioSource audioSource;
	public int GetHealthAmount() {
		return health;
	}

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDestroy(){
		// FIXME:
		//transform.parent.GetComponent<GameHealthManager>().DecreaseGameHealth ((uint)health);
	}
	public void Kill(){
        audioSource.PlayOneShot(deathSound);
		gameObject.GetComponent<Animator> ().Play ("Die");
		Destroy (gameObject, 2f);
	}
	public GameObject GetGameObject() {
		return gameObject;
	}
}
