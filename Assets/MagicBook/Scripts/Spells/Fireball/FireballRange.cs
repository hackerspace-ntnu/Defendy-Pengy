using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireballRange : MonoBehaviour
{
	public ParticleSystemRenderer fireEffect;
	public ParticleSystem smokeEffect;

	public AudioClip burnSound;

	public float attackDamage; // per second
	public float totalBurnDuration; //slow for seconds

	private float particleLifetime;

	void Start()
	{
		#region changeDurationOfParticleSystems
		ParticleSystem particleSystem = fireEffect.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule main = particleSystem.main;
		particleLifetime = main.startLifetime.constant;

		particleSystem.Stop();
		main.duration = totalBurnDuration;
		particleSystem.Play();
		smokeEffect.Stop();
		main = smokeEffect.main;
		if (totalBurnDuration < particleLifetime)
			main.duration = 0.01f;
		else
			main.duration = totalBurnDuration - particleLifetime;
		smokeEffect.Play();
		#endregion

		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = burnSound;
		audio.Play();

		StartCoroutine(Chronology());
	}

	void OnTriggerStay(Collider col)
	{
		Enemy enemy = col.GetComponentInParent<Enemy>();
		if (enemy != null)
		{
			InflictDamage(enemy);
		}
	}

	private IEnumerator Chronology()
	{
		yield return new WaitForSeconds(totalBurnDuration);

		StartCoroutine(FadeAudio(particleLifetime));
		yield return new WaitForSeconds(particleLifetime);

		//remove the AOE
		GetComponent<SphereCollider>().enabled = false;
		/*Color c = fireEffect.material.GetColor("_TintColor"); //make the object fade object by turning down its opacity gradually
		while (c.a > 0)
		{
			c.a *= 0.9f;
			if (c.a < 0.001f)
				break;
			fireEffect.material.SetColor("_TintColor", c);
			yield return null;
		}*/

		//wait till the smoke disappears
		yield return new WaitForSeconds(smokeEffect.main.duration);

		Destroy(gameObject);
	}

	private IEnumerator FadeAudio(float fadeTime)
	{
		AudioSource audio = GetComponent<AudioSource>();
		float startVolume = audio.volume;

		while (audio.volume > 0)
		{
			audio.volume -= startVolume * Time.deltaTime / fadeTime;
			yield return null;
		}

		audio.Stop();
	}

	void InflictDamage(Enemy enemy)
	{
		enemy.InflictDamage(attackDamage * Time.deltaTime, this);
	}
}
