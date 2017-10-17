using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Enemy, IDamagable
{
	protected override float IdleSoundFreq { get { throw new System.NotImplementedException(); } }
	protected override float IdleSoundChance { get { throw new System.NotImplementedException(); } }

	void Start () {
		//EnemyTest(100f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region IDamagable implementation

	public float GetHealth ()
	{
		throw new System.NotImplementedException ();
	}

	public float DecreaseHealth ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
