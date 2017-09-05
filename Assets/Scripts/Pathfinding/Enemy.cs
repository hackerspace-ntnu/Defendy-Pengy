 // MoveTo.cs
using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour{
	UnityEngine.AI.NavMeshAgent agent;
	public Transform goal;
	public float health;
	public float speed;

	public Enemy(float health, float speed){
		this.health=health;
		this.speed=speed;
		 agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position; 
		agent.speed = speed;
		print (goal.position);
	}
	void Update () {
		//UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		if (agent.remainingDistance < 1f) {
			Destroy (gameObject);
		}
	}
	public float getHealth(){
		return health;
	}
	public void setSpeed(float speed){
		this.speed = speed;
	}

		

}