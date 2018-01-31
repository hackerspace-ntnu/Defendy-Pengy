using UnityEngine;

public class Enemy_Animator : MonoBehaviour
{
	private Animator animator;
	private int deathHash = Animator.StringToHash("Death");

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void OnDeath()
	{
		animator.SetTrigger(deathHash);
	}
}
