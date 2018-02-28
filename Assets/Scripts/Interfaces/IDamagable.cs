using UnityEngine;

public interface IDamagable
{
	void InflictDamage(float damage, Component source = null);
}
