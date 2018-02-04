using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public abstract class Spell : MonoBehaviour
{
	protected float fadeDuration = 0.3f;
	protected bool fired = false;
	public bool isInitSizing = true;
	public float delayBetweenSpawns = 1f;

	public abstract void Fire(Vector3 handDirection);


	public abstract void ShowPreview();
	public abstract void HidePreview();
	protected abstract void Start_Derived();
	protected abstract void Update_Derived();
	protected abstract void FixedUpdate_Derived();
	//public abstract void OnPlayerTakeSpell();
	public abstract void OnPlayerHoldSpell();

	void Start()
	{
		Start_Derived();
	}

	void Update()
	{
		Update_Derived();
	}

	void FixedUpdate()
	{
		FixedUpdate_Derived();
	}
}
