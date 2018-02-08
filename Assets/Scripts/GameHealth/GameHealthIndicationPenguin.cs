using UnityEngine;

public class GameHealthIndicationPenguin : MonoBehaviour, IGameHealthIndicationItem {
	private int health = 1;
	public AudioClip deathSound;
	private AudioSource audioSource;

	public int GetHealthAmount() {
		return health;
	}

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
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
