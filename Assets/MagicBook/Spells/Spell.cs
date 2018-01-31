using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public abstract class Spell : MonoBehaviour
{
	protected Hand hand;

	protected bool fired = false;

	public abstract void Fire();

	protected abstract void Update_Derived();
	protected abstract void FixedUpdate_Derived();

	void Start()
	{
		hand = GetComponentInParent<Hand>();
	}

	void Update()
	{
		if (!fired && !hand.controller.GetHairTrigger())
		{
			transform.parent = null;
			Fire();
			fired = true;
		}

		Update_Derived();
	}

	void FixedUpdate()
	{
		FixedUpdate_Derived();
	}
}
