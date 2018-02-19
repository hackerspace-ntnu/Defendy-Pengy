using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireballRange : MonoBehaviour
{
	private SphereCollider sCol;

	public ParticleSystemRenderer fireEffect;
	public ParticleSystem smokeEffect;
	public float attackDamage; // per second
	public float totalBurnDuration; //slow for seconds

	void Start()
	{
		#region changeDurationOfParticleSystems
			var ps = fireEffect.GetComponent<ParticleSystem>();
			ps.Stop();
			var main = fireEffect.GetComponent<ParticleSystem>().main;
			main.duration = totalBurnDuration;
			ps.Play();
			smokeEffect.Stop();
			main = smokeEffect.main;
			if (totalBurnDuration < 1)
				main.duration = 0.01f;
			else
				main.duration = totalBurnDuration - 1f;
			smokeEffect.Play();
		#endregion
		sCol = GetComponent<SphereCollider>();
		StartCoroutine(Chronology());
	}

	void OnTriggerStay(Collider col)
	{
		Enemy enemy = col.GetComponentInParent<Enemy>();
		if (enemy != null)
		{
			InflictDamage(enemy);
		}
	}


	private IEnumerator Chronology()
	{
		yield return new WaitForSeconds(totalBurnDuration + 1f); //1f is the life time of each particle
		//remove the AOE
		sCol.enabled = false;
		/*Color c = fireEffect.material.GetColor("_TintColor"); //make the object fade object by turning down its opacity gradually
		while (c.a > 0)
		{
			c.a *= 0.9f;
			if (c.a < 0.001f)
				break;
			fireEffect.material.SetColor("_TintColor", c);
			yield return null;
		}*/

		//make the fire transparent and remove it

		//wait till the smoke disappears
		yield return new WaitForSeconds(3f);

		Destroy(gameObject);
	}

	void InflictDamage(Enemy enemy)
	{
		enemy.InflictDamage(attackDamage*Time.deltaTime);
	}
}
