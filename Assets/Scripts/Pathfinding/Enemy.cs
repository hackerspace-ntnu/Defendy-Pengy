 // MoveTo.cs
using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour, IDamagable{

	UnityEngine.AI.NavMeshAgent agent;
	public Transform goal;
	public float health;
	private float Speed;

	void Start(){
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position; 
		//agent.speed = Speed;
		print (goal.position);
	}
	void Update () {
		//UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEnginenemy.gameObjecte.AI.NavMeshAgent>();
		if (agent.remainingDistance < 1f) {
			Destroy (gameObject);
		}
		/*if(health<=0f){
			Destroy (gameObject);
		}*/
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

	void OnCollisionEnter(Collision col){
		//print ("hit");
		//if col.
	}

}