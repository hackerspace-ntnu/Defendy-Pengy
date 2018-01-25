using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class MagicBook_Hover : MonoBehaviour
{
	public MagicBook magicBookParent;

	private bool lastTriggerPress = false;

	private void OnHandHoverBegin(Hand hand)
	{
		magicBookParent.rune.GetComponent<Renderer>().material.color = Color.yellow;
	}

	private void HandHoverUpdate(Hand hand)
	{
		if (hand.controller.GetHairTrigger())
		{
			if (!lastTriggerPress)
			{
				magicBookParent.ChargeSpell(hand);
				lastTriggerPress = true;
			}
		} else
		{
			if (lastTriggerPress)
				lastTriggerPress = false;
		}
	}

	private void OnHandHoverEnd(Hand hand)
	{
		magicBookParent.rune.GetComponent<Renderer>().material.color = Color.white;
	}
}
