//Timmy Chan and Arne-Martin
using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

public abstract partial class Enemy : MonoBehaviour, IDamagable
{
	UnityEngine.AI.NavMeshAgent agent;
	public Transform goal;
	public float health;
	private float Speed;
	private static float healthStatic;
	public GameObject HealthBarPrefab;
	public Transform HeadsetPosition;
	private GameObject HealthBar;

	void Start(){
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position; 

		//Player player=Valve.VR.InteractionSystem.Player;

		//initiate healthbar and finding headsetposition, Arne-Martin

		HealthBar = Instantiate(HealthBarPrefab, new Vector3(transform.position.x, transform.position.y+1.2f, transform.position.z),Quaternion.identity);
		HealthBar.transform.parent = gameObject.transform;
		HeadsetPosition=Valve.VR.InteractionSystem.Player.instance.trackingOriginTransform;
		healthStatic = health;


		//Vector3 left = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.LeftEye)) * InputTracking.GetLocalPosition(VRNode.LeftEye);
	}
	void Update () {

		//To scale healthbar to health, Arne-Martin
		var healthScale = this.health / healthStatic;
		HealthBarPrefab.transform.localScale = new Vector3 (healthScale,0.1f ,0.1f );
		//Rotate healthbar towards player, Arne-Martin
		Vector3 UpdatedHeadsetPosition=HeadsetPosition.position;
		HealthBar.transform.LookAt (UpdatedHeadsetPosition);


		//UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEnginenemy.gameObjecte.AI.NavMeshAgent>();
		if (agent.remainingDistance < 1f) {
			Destroy (gameObject);
		}
		if(health <= 0f){
			//play die animation
			//instantiate particles
			Destroy (gameObject);
		}

		HandleIdleSound();
	}

		
	public void SpeedMultiplier(float mult){
		Speed *= mult;
		agent.speed = Speed;
	}


	#region IDamagable implementation
	public void SetHealth(float health) { this.health = health; }

	public float GetHealth() { return health; }

	public void InflictDamage (float damage)
	{
		health -= damage;
		PlayHurtSound();
	}
	#endregion
}
