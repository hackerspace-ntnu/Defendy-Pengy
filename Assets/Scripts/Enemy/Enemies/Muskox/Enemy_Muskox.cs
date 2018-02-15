public class Enemy_Muskox : Enemy
{
	public float baseSpeed = 0.9f;
	public float speedRange = 0.5f;

	public override Type type
	{
		get { return Type.Muskox; }
	}

	protected override float GetBaseSpeed()
	{
		return baseSpeed;
	}

	protected override float GetSpeedRange()
	{
		return speedRange;
	}
}
