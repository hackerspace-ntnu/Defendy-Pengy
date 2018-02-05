public class Enemy_Seal : Enemy
{
	private float baseSpeed = 1.2f;
	private float speedRange = 0.5f;

	protected override float GetBaseSpeed()
	{
		return baseSpeed;
	}

	protected override float GetSpeedRange()
	{
		return speedRange;
	}
}
