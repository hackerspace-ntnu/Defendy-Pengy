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
		if(health <= 0f)
		{
			//play die animation
			//instantiate particles
			Destroy(gameObject);
			PlayDeathSound();
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
	public void InflictDamage (float damage, Vector3 direction){
		health -= damage;
		var pos = transform.position;
		direction.y = 0f;
		direction = direction / 20f;
		transform.position = pos + direction;
		print(direction);

		

		PlayHurtSound();
	}
}
