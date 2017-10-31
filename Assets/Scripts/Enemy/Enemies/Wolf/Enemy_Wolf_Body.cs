using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wolf_Body : MonoBehaviour, IDamagable {
	private Enemy thisEnemy;
	// Use this for initialization
	void Start () {
		thisEnemy = transform.parent.GetComponent<Enemy> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InflictDamage (float damage) {
		thisEnemy.InflictDamage (damage);
		
	}
}
