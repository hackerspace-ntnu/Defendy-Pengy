public class Enemy_Muskox : Enemy
{
	private float baseSpeed = 0.9f;
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
