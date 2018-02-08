public class Enemy_Wolf : Enemy
{
	public float baseSpeed = 2f;
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
