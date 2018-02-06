using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagicBall_SlowDownEnemy : MonoBehaviour {
	public GameObject splashEffect;
	public float slowAmount = 0.1f; // slow till speed on enemy hit
	public float timeSpan = 5f; // last for 5 seconds
	public List<Enemy> enemies;
	public float radius = 8f;

	private float timeSinceStartOfEffect = 0f;
	private bool isOnEffect = false;

	// Update is called once per frame
	void Update () {
		if (isOnEffect)
		{
			timeSinceStartOfEffect += Time.deltaTime;

			if (timeSinceStartOfEffect >= timeSpan)
			{
				RemoveEffect();
				Destroy(gameObject);
			}
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			//SlowDownEnemy();
		}
	}

    void OnTriggerEnter(Collider col)
    {
        print("something");
        var enemy = LoopParentFindEnemy(col.transform);
        if (enemy != null)
        {
            print("enemy");
            SlowDownEnemy(enemy);
            isOnEffect = true;
        }
    }

    Enemy LoopParentFindEnemy(Transform t)
    {
        var enemy = t.GetComponent<Enemy>();
        if (enemy != null)
            return enemy;
        else
        {
            if (t.parent != null)
                return LoopParentFindEnemy(t.parent);
            else
                return null;
        }
    }


    void SlowDownEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))//if enemy is already being affected, do nothing;
            return;
        else
            enemies.Add(enemy);
        AddEffectToEnemy(enemy);
    }
    
	void RemoveEffect()
	{
		foreach (var enemy in enemies)
		{
			RemoveEffectOnEnemy(enemy);
		}
	}


	void AddEffectToEnemy(Enemy enemy)
	{
		var navMesh = enemy.GetComponent<NavMeshAgent>();
        print(navMesh.speed * slowAmount);
		navMesh.speed *= slowAmount;
	}

	void RemoveEffectOnEnemy(Enemy enemy)
	{
		var navMesh = enemy.GetComponent<NavMeshAgent>();
		navMesh.speed /= slowAmount;
	}

}
