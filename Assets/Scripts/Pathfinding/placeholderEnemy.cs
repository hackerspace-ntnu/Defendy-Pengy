using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeholderEnemy : MonoBehaviour {
	UnityEngine.AI.NavMeshAgent agent;
	public float health;
	private float speed;
	public Transform goal;
	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position; 


	}
	
	// Update is called once per frame
	void Update () {
		if (this.health < 0f) {
			Destroy (gameObject);
		}
	//		if (agent.remainingDistance < 1f) {
	//		Destroy (gameObject);
	//	}
	}
	public void dealDamage(float damage){
		this.health = this.health - damage;
	}
}
