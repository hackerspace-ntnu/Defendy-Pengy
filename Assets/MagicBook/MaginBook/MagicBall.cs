using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagicBall : MonoBehaviour {
	public GameObject splashEffect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		//explode
		float radius = 2f;
		Instantiate(splashEffect, transform.position, Quaternion.identity, transform.parent);
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

		foreach(Collider c in hitColliders){
			var enemy = c.gameObject.GetComponent<Enemy>();
			if (enemy != null){ 
				c.gameObject.GetComponent<NavMeshAgent>().speed = 0f; //stop the enemy from moving
				//enemy.knockback()
				enemy.InflictDamage(30f);
			}
			
		}


		Destroy(gameObject);
	}

	List<Enemy> getEnemiesInRadius(float radius)
	{
		var list = new List<Enemy>();



		return list;
	}
}
