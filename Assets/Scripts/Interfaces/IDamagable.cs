using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable {
	void InflictDamage (float damage, Vector3 direction);
}