using System.Collections;
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
	private bool isChangingSpell = true;

	void Start()
	{
		if (spells.Length == 0)
		{
			Debug.LogError("No Spells added to the MagicBook_SpellController");
			Destroy(this);
			return;
		}
	}

	void Update()
	{
		if(!isOpened) {
			openDelay -= Time.deltaTime;
			if(openDelay < 0f) {
				isOpened = true;
				openDelay = 1.2f;

				StartCoroutine(PlayIgniteSound());
			}
			return;
		}
		if (!instantiatedSpell)
			MakeSpell(isChangingSpell);
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
		instantiatedSpell.transform.parent = hand.transform;
		spellHand = hand;
	}

	public void SetCurSpell(int n)
	{
		if (n >= spells.Length)
			Debug.LogError("Spell index " + n + " is not valid");
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
		isChangingSpell = true;

		StartCoroutine(PlayIgniteSound());
	}

	private IEnumerator PlayIgniteSound()
	{
		yield return new WaitForSeconds(0.2f);
		if (isOpened)
			SoundManager.PlaySoundAtPoint(spells[curSpell].igniteSound, transform.position);
	}

	public void MakeSpell(bool skipDelay = false)
	{
		instantiatedSpell = Instantiate(spells[curSpell].gameObject).GetComponent<Spell>();
		//instantiatedSpell.transform.parent = transform;
		instantiatedSpell.transform.parent = transform;
		instantiatedSpell.transform.position = transform.position;
		if (skipDelay)
			instantiatedSpell.delayBetweenSpawns = 0f;
		isChangingSpell = false;
	}
}
