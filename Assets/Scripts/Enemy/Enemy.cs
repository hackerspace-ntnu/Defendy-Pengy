//Timmy Chan and Arne-Martin
using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

public abstract partial class Enemy : MonoBehaviour{
	UnityEngine.AI.NavMeshAgent agent;
	public Transform goal;
	public float startHealth = 40f;
	public float health;
	private float Speed;
	public GameObject HealthBarPrefab;
	private Transform HeadsetPosition;
	private EnemyHealthBar healthBar;
	private EnemyManager enemyManager;
	private bool dying = false;
	private SkinnedMeshRenderer enemySkinnedMeshRenderer;

	void Start(){
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position; 
		enemyManager = transform.parent.GetComponent<EnemyManager> ();
		//Player player=Valve.VR.InteractionSystem.Player;

		//initiate healthbar and finding headsetposition, Arne-Martin
		healthBar = Instantiate(HealthBarPrefab, new Vector3(transform.position.x, transform.position.y+2.4f, transform.position.z),Quaternion.identity)
			.GetComponent<EnemyHealthBar>();
		healthBar.transform.parent = gameObject.transform;
		HeadsetPosition=Valve.VR.InteractionSystem.Player.instance.trackingOriginTransform;
		health = startHealth;

		//Vector3 left = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.LeftEye)) * InputTracking.GetLocalPosition(VRNode.LeftEye);
		SoundStart();

		float h, s, v; // hue, saturation, value (brightness)
		float oldV;
		enemySkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();

		for (int i = 0; i < enemySkinnedMeshRenderer.materials.Length; i++) {
			Color.RGBToHSV (enemySkinnedMeshRenderer.materials[i].color, out h, out s, out v);
			oldV = v;
			v = Random.Range (oldV*0.5f, oldV*1.5f);

			if (v > 1f) {
				v = 1f;
			}

			enemySkinnedMeshRenderer.materials[i].color = Color.HSVToRGB (h, s, v);
		}
	}

	void Update () {
		//To scale healthbar to health, Arne-Martin
		float healthPercentage = this.health / startHealth;
		healthBar.display (healthPercentage);
		//Rotate healthbar towards player, Arne-Martin
		Vector3 UpdatedHeadsetPosition=HeadsetPosition.position;
		healthBar.transform.LookAt (UpdatedHeadsetPosition);


		//UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEnginenemy.gameObjecte.AI.NavMeshAgent>();
		if (agent.remainingDistance < 1f) {
			print ("goal");
			enemyManager.ReachedGoal (1);
			Destroy (gameObject);
		}

		if(health <= 0f) {

			//play die animation
			//instantiate particles
			

			if (!dying) {
				gameObject.GetComponent<Animator>().Play("Die");
				gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0f;
				dying = true;
				gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
				gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
				Destroy (gameObject, 4f);
				PlayDeathSound ();
			}
		}

		HandleIdleSound();
	}

		
	public void SpeedMultiplier(float mult){
		Speed *= mult;
		agent.speed = Speed;
	}


	public void SetHealth (float health){
		this.health = health;
		startHealth = health;
	}
	public float GetHealth (){return health;}
	public void InflictDamage (float damage){
		health -= damage;
		
		PlayHurtSound();
	}
}
