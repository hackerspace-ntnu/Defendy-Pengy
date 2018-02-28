public class Enemy_Wolf : Enemy
{
	public float baseSpeed = 2f;
	public float speedRange = 1f;

	public override Type type
	{
		get { return Type.Wolf; }
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
