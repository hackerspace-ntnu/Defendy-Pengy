public class Enemy_Bear : Enemy
{
	public float baseSpeed = 1.5f;
	public float speedRange = 1f;

	public override Type type
	{
		get { return Type.Bear; }
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
