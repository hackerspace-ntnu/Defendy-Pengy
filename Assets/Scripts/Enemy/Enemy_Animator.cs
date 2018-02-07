using UnityEngine;

public class Enemy_Animator : MonoBehaviour
{
	private Animator animator;
	private int speedMultiplierHash = Animator.StringToHash("SpeedMultiplier");
	private int deathHash = Animator.StringToHash("Death");

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void OnSetSpeed(float baseSpeed, float setSpeed)
	{
		// Sets the animation speed multiplier to the ratio between current movement speed and base movement speed.
		animator.SetFloat(speedMultiplierHash, setSpeed / baseSpeed);
	}

	public void OnDeath()
	{
		animator.SetTrigger(deathHash);
	}
}
