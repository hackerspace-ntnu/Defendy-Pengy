public class Enemy_Wolf : Enemy
{
	private float baseSpeed = 2f;
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
