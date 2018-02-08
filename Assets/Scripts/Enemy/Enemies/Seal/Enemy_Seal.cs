public class Enemy_Seal : Enemy
{
	public float baseSpeed = 1.2f;
	public float speedRange = 0.5f;

	protected override float GetBaseSpeed()
	{
		return baseSpeed;
	}

	protected override float GetSpeedRange()
	{
		return speedRange;
	}
}
