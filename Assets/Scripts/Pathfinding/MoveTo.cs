 // MoveTo.cs
using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	UnityEngine.AI.NavMeshAgent agent;
	public Transform goal;

	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position; 
		print (goal.position);
	}
	void Update () {
		//UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		if (agent.remainingDistance < 1f) {
			Destroy (gameObject);
		}
	}
}