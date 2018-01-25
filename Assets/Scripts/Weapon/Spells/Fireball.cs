using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
	public float speed = 5f;

	private Vector3 direction;

	public override void Fire()
	{
		direction = hand.transform.forward;
	}

	protected override void Update_Derived()
	{
		if (fired)
			transform.Translate(direction * (speed * Time.deltaTime), Space.World);
	}

	protected override void FixedUpdate_Derived()
	{
		
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.layer == 0)
			Destroy(gameObject);
	}
}
