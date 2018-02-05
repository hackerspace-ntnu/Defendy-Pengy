public class Enemy_Fox : Enemy
{
	private float baseSpeed = 3.5f;
	private float speedRange = 1f;

	protected override float GetBaseSpeed()
	{
		return baseSpeed;
	}

	protected override float GetSpeedRange()
	{
		return speedRange;
	}
}
