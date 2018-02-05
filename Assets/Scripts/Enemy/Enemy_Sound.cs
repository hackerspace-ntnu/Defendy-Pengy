using UnityEngine;

abstract partial class Enemy
{
	public AudioClip[] idleSounds;
	public float idleVolume = 1f;
	public AudioClip[] hurtSounds;
	public float hurtVolume = 0.5f;
	public AudioClip[] deathSounds;
	public float deathVolume = 1f;

	public float IdleSoundFreq_sec = 5f;
	public float IdleSoundChance = 0.1f;

	protected float lastIdleSoundTime;

	void SoundStart()
	{
		lastIdleSoundTime = Random.value * IdleSoundFreq_sec;
	}

	protected void HandleIdleSound()
	{
		if (Time.time + IdleSoundFreq_sec >= lastIdleSoundTime)
		{
			if (Random.value <= IdleSoundChance)
				SoundManager.PlayRandomSound(this, idleSounds);

			lastIdleSoundTime += IdleSoundFreq_sec;
		}
	}

	protected void PlayHurtSound()
	{
		SoundManager.PlayRandomSound(this, hurtSounds, hurtVolume);
	}

	protected void PlayDeathSound()
	{
		SoundManager.PlayRandomSoundAtPoint(deathSounds, transform.position, 1f, transform.parent);
	}
}
