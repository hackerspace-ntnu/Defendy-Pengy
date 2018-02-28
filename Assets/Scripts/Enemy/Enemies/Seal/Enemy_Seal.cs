public class Enemy_Seal : Enemy
{
	public float baseSpeed = 1.2f;
	public float speedRange = 0.5f;

	public override Type type
	{
		get { return Type.Seal; }
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
