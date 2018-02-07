public class Enemy_Fox : Enemy
{
	public float baseSpeed = 3.5f;
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
