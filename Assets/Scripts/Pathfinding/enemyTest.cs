using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Enemy, IDamagable {
	
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
