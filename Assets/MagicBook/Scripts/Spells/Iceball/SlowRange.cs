using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class SlowRange : MonoBehaviour
{
	private SphereCollider sCol;
	private float increaseDuration = 2f;
	private float staticDuration = 2f;

	public float slowAmount = 0.5f; // slow till speed percentage (0.1 = 10%) on enemy hit
	public float slowDuration = 8f; //slow for seconds

	private static List<Enemy> allSlowedEnemies = new List<Enemy>();
	private List<Enemy> slowedEnemies = new List<Enemy>();

	// Use this for initialization
	void Start()
	{
		sCol = GetComponent<SphereCollider>();
		StartCoroutine(GraduallyIncreaseCollider());
	}

	void OnTriggerEnter(Collider col)
	{
		Enemy enemy = col.GetComponentInParent<Enemy>();
		if (enemy != null)
		{
			SlowDownEnemy(enemy);
			print("Slowed down enemy " + enemy.name);
		}
	}

	private IEnumerator GraduallyIncreaseCollider()
	{
		float targetRadius = sCol.radius; //assumes that the scale is evenly increased
		float curDuration = increaseDuration;
		//assumes that the scale is evenly increased
		sCol.radius = 0f; //get make the collider size back to its original
		while (curDuration > 0f)
		{
			var a = Time.deltaTime / increaseDuration;
			sCol.radius += targetRadius * a;
			curDuration -= Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds(staticDuration);
		//remove the AOE
		sCol.enabled = false;

		//after the slowDuration, remove the buff
		yield return new WaitForSeconds(slowDuration);
		RemoveEffect();
		Destroy(gameObject);
	}

	void SlowDownEnemy(Enemy enemy)
	{
		if (allSlowedEnemies.Contains(enemy))//if enemy is already being affected, do nothing;
			return;
		allSlowedEnemies.Add(enemy);
		AddEffectToEnemy(enemy);
		slowedEnemies.Add(enemy);
	}

	void RemoveEffect()
	{
		foreach (var enemy in slowedEnemies)
		{
			RemoveEffectOnEnemy(enemy);
			allSlowedEnemies.Remove(enemy);
		}
	}

	void AddEffectToEnemy(Enemy enemy)
	{
		var navMesh = enemy.GetComponent<NavMeshAgent>();
		navMesh.speed *= slowAmount;
	}

	void RemoveEffectOnEnemy(Enemy enemy)
	{
		var navMesh = enemy.GetComponent<NavMeshAgent>();
		navMesh.speed /= slowAmount;
	}
}