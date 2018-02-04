using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class MagicBook_SpellController : MonoBehaviour {
	public Spell[] spells;
	private int curSpell = 0;
	private Hand spellHand; //the hand holdning the spell

    private float openDelay = 1.2f;
    private bool isOpened = false;
	private Spell instantiatedSpell;
	// Use this for initialization
	void Start () {
		if (spells.Length == 0)
			print("Add Spells to the MagicBook_SpellController");
	}

	private void Update()
	{
        if(!isOpened) {
            openDelay -= Time.deltaTime;
            if(openDelay < 0f) {
                isOpened = true;
                openDelay = 1.2f;
            }
            return;
        }
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
            if (hand != GetComponentInParent<Hand>() && instantiatedSpell)
            { //if it is the other hand
                if(!instantiatedSpell.isInitSizing)
                    PlayerTakesSpell(hand);
            }
		}

	}


	private void PlayerTakesSpell(Hand hand)
	{
        print("PlayerTakeSpell");
		instantiatedSpell.transform.parent = hand.transform;
		spellHand = hand;
	}

	public void SetCurSpell(int n)
	{
		if (n >= spells.Length)
			print("Spell number is not valid");
		curSpell = n;
	}

    public void ChangeSpell(int page) {
        if (page == 2) {
            curSpell = 0;
        } else if (page == 3) {
            curSpell = 1;
        }
        if(instantiatedSpell)
            instantiatedSpell.HidePreview();
    }


	public void MakeSpell()
	{
		instantiatedSpell = Instantiate(spells[curSpell].gameObject).GetComponent<Spell>();
		//instantiatedSpell.transform.parent = transform;
		instantiatedSpell.transform.parent = transform;
		instantiatedSpell.transform.position = transform.position;
	}
}
