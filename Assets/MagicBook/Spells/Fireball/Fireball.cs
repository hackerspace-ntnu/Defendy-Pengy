using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
	public float speed = 10f;
	public float damage = 67f;
	public float maxAdditionalDamage = 200f;
	private Vector3 direction;
	public Light pointLight;

	#region ParticleSystem
	public ParticleSystem ps;
	private float targetStartSize;


	private float maxScaleMultiplier = 7.1f; //double the size of fireball;
	private Vector3 maxAdditionalScale;


	#endregion

	protected override void Start_Derived()
	{
		targetStartSize = ps.main.startSize.constant;
		maxAdditionalScale = transform.lossyScale * maxScaleMultiplier;
		StartCoroutine(Show());
	}

	protected override void Update_Derived()
	{
		if (Input.GetKey(KeyCode.Space))
			OnPlayerHoldSpell();
		if (fired)
		{
			transform.Translate(direction * (speed * Time.deltaTime), Space.World);
			// TODO: edit gravity of fireball to get the moving feel
		}
	}

	protected override void FixedUpdate_Derived() { }

	public override void OnPlayerHoldSpell()
	{
		//move itself towards the hand position
		if (transform.parent)
			transform.position = Vector3.Lerp(transform.position, transform.parent.position + transform.parent.forward * 0.2f, 0.2f);
		if (!isInitSizing) //wait till the spell is on a ready size. Then increase the size;
		{
			if (transform.lossyScale.x >= 0.5f)
				return;
			transform.parent.GetComponent<Valve.VR.InteractionSystem.Hand>().controller.TriggerHapticPulse(500);
			transform.localScale += maxAdditionalScale * Time.deltaTime / playerHoldScalingDuration;

			UpdateLoopSound();

			//print(transform.localScale);
			damage += maxAdditionalDamage * Time.deltaTime/playerHoldScalingDuration;
			//print(damage);
		}
	}

	public override void Fire(Vector3 handDirection)
	{
		transform.parent = null;
		direction = handDirection;
		fired = true;
		StartCoroutine(LifeTimeOut());
		PlayThrowSound();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<Spell>()) //don't collide with spells
			return;
		if (!fired)
			return;
		if (collider.GetComponent<SlowRange>()) //don't collide with spells
			return;
		IDamagable damagable = collider.gameObject.GetComponentInParent<IDamagable>();
		if (damagable != null)
		{
			damagable.InflictDamage(damage);
			PlayImpactSound();
			Destroy(gameObject);
		}
		else
		{
			// To ignore collisions with MagicBook's own collider; layer 0 is the Default layer
			if (collider.gameObject.layer == 0)
			{
				PlayImpactSound();
				Destroy(gameObject);
			}
		}
	}



	public override void ShowPreview()
	{
		StartCoroutine(Show());
	}

	public override void HidePreview()
	{
		StartCoroutine(HideAndDestroy());
	}


	private IEnumerator HideAndDestroy()
	{ //gradually decrease the size of the spell and finally destroy
		Vector3 scaleAtThatMoment = transform.localScale;
		float intensityAtThatMoment = pointLight.intensity;
		float curDuration = fadeDuration;
		while (curDuration > 0f)
		{
			var a = Time.deltaTime / fadeDuration;
			transform.localScale -= scaleAtThatMoment * a;
			pointLight.intensity -= intensityAtThatMoment * a;
			curDuration -= Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}


	private IEnumerator Show()
	{ //gradually increase the size of the spell
		Vector3 scaleAtThatMoment = transform.localScale;
		float intensityAtThatMoment = pointLight.intensity;
		transform.localScale = Vector3.zero;
		pointLight.intensity = 0f;
		float curDuration = fadeDuration;
		yield return new WaitForSeconds(delayBetweenSpawns);
		while (curDuration > 0f)
		{
			var a = Time.deltaTime / fadeDuration;
			transform.localScale += scaleAtThatMoment * a;
			pointLight.intensity += intensityAtThatMoment * a;
			curDuration -= Time.deltaTime;
			yield return null;
		}
		isInitSizing = false;
	}

	protected IEnumerator LifeTimeOut()
	{
		yield return new WaitForSeconds(30);
		Destroy(gameObject);
	}
}
