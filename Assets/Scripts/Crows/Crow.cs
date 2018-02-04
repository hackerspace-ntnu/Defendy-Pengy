using UnityEngine;

public class Crow : MonoBehaviour
{
	public float speed = 15f;
	public Transform goal;
	private float distanceToGoal;

	void Start()
	{
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, goal.position, 10f, 0f));
		Destroy(gameObject, 15f); // Destroy in 15 seconds
	}

	void Update()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, goal.position, step);
	}
}
