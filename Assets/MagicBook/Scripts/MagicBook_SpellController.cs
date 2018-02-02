using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class MagicBook_SpellController : MonoBehaviour {
	public Spell[] spells;
	private int curSpell = 0;
	private MagicBook mb;
	private Hand spellHand; //the hand holdning the spell


	private Spell instantiatedSpell;
	// Use this for initialization
	void Start () {
		mb = GetComponentInParent<MagicBook>();
		if (spells.Length == 0)
			print("Add Spells to the MagicBook_SpellController");
	}

	private void Update()
	{
		if (!instantiatedSpell)
			MakeSpell();
		if (spellHand)
		{
			instantiatedSpell.OnPlayerHoldSpell();
			if (spellHand.controller.GetHairTriggerUp())
			{
				instantiatedSpell.Fire(spellHand.transform.forward);
				spellHand = null;
				instantiatedSpell = null;
			}
		}
	}
	
	private void OnHandHoverEnd(Hand hand)
	{
	}

	private void HandHoverUpdate(Hand hand)
	{
		///instantiate a spell and it will act as a preview
		if (hand.controller.GetHairTriggerDown())
		{
			if(hand != GetComponentInParent<Hand>()) //if it is the other hand
				PlayerTakesSpell(hand);
		}

	}


	private void PlayerTakesSpell(Hand hand)
	{
		instantiatedSpell.transform.parent = hand.transform;
		spellHand = hand;
	}

	public void SetCurSpell(int n)
	{
		if (n >= spells.Length)
			print("Spell number is not valid");
		curSpell = n;
	}



	public void MakeSpell()
	{
		instantiatedSpell = Instantiate(spells[curSpell].gameObject).GetComponent<Spell>();
		//instantiatedSpell.transform.parent = transform;
		instantiatedSpell.transform.parent = transform;
		instantiatedSpell.transform.position = transform.position;
	}
}
