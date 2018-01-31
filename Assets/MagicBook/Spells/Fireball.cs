using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
	public float speed = 5f;

	public float damage = Mathf.Ceil(200f / 3f); // 200 is the amount of health bears currently have

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
		IDamagable damagable = collider.gameObject.GetComponent<IDamagable>();
		if (damagable != null)
		{
			damagable.InflictDamage(damage);
			Destroy(gameObject);
		} else
		{
			// To ignore collisions with MagicBook's own collider; layer 0 is the Default layer
			if (collider.gameObject.layer == 0)
				Destroy(gameObject);
		}
	}
}
