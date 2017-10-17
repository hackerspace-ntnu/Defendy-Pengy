using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wolf : Enemy
{
	public float speed = 4;

	protected override float IdleSoundFreq { get { return 5f; } }
	protected override float IdleSoundChance { get { return 0.1f; } }
}
