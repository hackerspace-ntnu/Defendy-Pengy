public class Enemy_PolarBear : Enemy
{
	public float baseSpeed = 1.5f;
	public float speedRange = 1f;

	protected override float GetBaseSpeed()
	{
		return baseSpeed;
	}

	protected override float GetSpeedRange()
	{
		return speedRange;
	}
}
