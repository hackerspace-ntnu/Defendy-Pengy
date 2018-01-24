using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class MagicBook_Hover : MonoBehaviour
{
	public GameObject rune;

	private void OnHandHoverBegin(Hand hand)
	{
		rune.GetComponent<Renderer>().material.color = Color.yellow;
	}

	private void OnHandHoverEnd(Hand hand)
	{
		rune.GetComponent<Renderer>().material.color = Color.white;
	}
}
