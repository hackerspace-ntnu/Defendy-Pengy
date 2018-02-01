using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour {
	public float speed = 8f;
	public Transform goal;
	private float distanceToGoal;
	private Animator animator;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards (transform.position, goal.position, 10f, 0f));
		Destroy (gameObject, 15f); // Destroy in 30 seconds
		animator = gameObject.GetComponent<Animator> ();
		animator.speed = 4f;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, goal.position, step);
	}
}
