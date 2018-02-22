using UnityEngine;

public class Butterfly_randomFlapping : StateMachineBehaviour
{
	public float speedVariabilityPercentage = 5f;

	private int speedMultiplierHash = Animator.StringToHash("SpeedMultiplier");

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// Produces a value between -5 and 5
		float speedMultiplier_percentage = Random.value * (speedVariabilityPercentage * 2) - speedVariabilityPercentage;
		// Translates to between 0.95 and 1.05
		float speedMultiplier = 1f + speedMultiplier_percentage / 100f;
		animator.SetFloat(speedMultiplierHash, speedMultiplier);
	}
}
