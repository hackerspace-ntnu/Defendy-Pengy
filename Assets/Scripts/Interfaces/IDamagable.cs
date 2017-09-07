using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable {
	void SetHealth (float health);
	float GetHealth ();
	void InflictDamage (float damage);
}