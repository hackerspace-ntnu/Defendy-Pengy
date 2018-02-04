using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : Spell {
	public float speed = 7f;
	public float slowAmount = 0.8f; //decrease 80% speed
	public float slowRadius = 4f; //decrease 80% speed
	private Vector3 direction;
	public Light pointLight;
	public GameObject SlowRangePrefab2;

	public float maxAdditionalSlowRadius = 8f;

	#region ParticleSystem
	public ParticleSystem ps;
	private float maxScaleMultiplier = 7.1f; //double the size of iceball;
	private Vector3 maxAdditionalScale;

	#endregion

	protected override void Start_Derived()
	{
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

			audioSource.volume = (Time.time - playerHoldStartTime) / playerHoldScalingDuration;
			slowRadius += maxAdditionalSlowRadius * 1.3f * Time.deltaTime / playerHoldScalingDuration;
			//print(slowRadius);
		}

	}

	public override void Fire(Vector3 handDirection) {
		transform.parent = null;
		direction = handDirection;
		fired = true;
		StartCoroutine(LifeTimeOut());
		PlayThrowSound();
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.GetComponent<Spell>()) //don't collide with spells
			return;
		if (!fired)
			return;
		if (collider.GetComponent<SlowRange>()) //don't collide with spells
			return;
		var slowRange = Instantiate(SlowRangePrefab2);
		slowRange.transform.localScale *= slowRadius;
		slowRange.transform.position = transform.position;
		PlayImpactSound();
		Destroy(gameObject);
	}


	public override void ShowPreview() {
		StartCoroutine(Show());
	}
	public override void HidePreview() {
		StartCoroutine(HideAndDestroy());
	}
	private IEnumerator HideAndDestroy() {
		Vector3 scaleAtThatMoment = transform.localScale;
		float intensityAtThatMoment = pointLight.intensity;
		float curDuration = fadeDuration;
		while(curDuration > 0f) {
			var a = Time.deltaTime / fadeDuration;
			transform.localScale -= scaleAtThatMoment * a;
			pointLight.intensity -= intensityAtThatMoment * a;
			curDuration -= Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
	private IEnumerator Show()
	{
		Vector3 scaleAtThatMoment = transform.localScale;
		float intensityAtThatMoment = pointLight.intensity;
		transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
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


	
	protected IEnumerator LifeTimeOut() {
		yield return new WaitForSeconds(30);
		Destroy(gameObject);
	}

}
