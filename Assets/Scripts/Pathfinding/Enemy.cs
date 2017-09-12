//Timmy Chan and Arne-Martin
using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour, IDamagable{

	UnityEngine.AI.NavMeshAgent agent;
	public Transform goal;
	public float health;
	private float Speed;

	public GameObject HealthBarPrefab;

	void Start(){
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position; 
		//agent.speed = Speed;
		//print (health);

		//initiate healthbar:

		GameObject HealthBar = Instantiate(HealthBarPrefab, new Vector3(transform.position.x, transform.position.y+1.2f, transform.position.z),Quaternion.identity);
		HealthBar.transform.parent = gameObject.transform;


	}
	void Update () {
		//print (health);
		HealthBarPrefab.transform.localScale = new Vector3 (GetHealth () / 100f,0.1f ,0.1f );
		//print("hi");
		//UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEnginenemy.gameObjecte.AI.NavMeshAgent>();
		if (agent.remainingDistance < 1f) {
			Destroy (gameObject);
		}
		if(health <= 0f){
			//play die animation
			//instantiate particles
			Destroy (gameObject);
		}
	}

		
	public void SpeedMultiplier(float mult){
		Speed *= mult;
		agent.speed = Speed;
	}


	#region IDamagable implementation
	public void SetHealth (float health){this.health = health;}
	public float GetHealth (){return health;}
	public void InflictDamage (float damage){health -= damage;}
	#endregion

}