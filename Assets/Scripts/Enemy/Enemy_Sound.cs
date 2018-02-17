using UnityEngine;

abstract partial class Enemy
{
	public AudioClip[] idleSounds;
	public float idleVolume = 1f;
	public AudioClip[] hurtSounds;
	public float hurtVolume = 0.5f;
	public AudioClip[] deathSounds;
	public float deathVolume = 1f;

	public float idleSoundFreq_sec = 5f;
	public float idleSoundChance = 0.2f;

	public Vector2 randomPitchRange = new Vector2(0.8f, 1.2f);

	private float nextIdleSoundTime;

	void SoundStart()
	{
		nextIdleSoundTime = Time.time;
	}

	void SoundUpdate()
	{
		HandleIdleSound();
	}

	private void HandleIdleSound()
	{
		float currentTime = Time.time;
		if (currentTime >= nextIdleSoundTime)
		{
			if (Random.value <= idleSoundChance)
			{
				SoundManager.PlayRandomSound(this, idleSounds, randomPitchRange, idleVolume);
				// Longer timeout if enemy just played a sound
				nextIdleSoundTime = currentTime + idleSoundFreq_sec * 1.5f + Random.value * idleSoundFreq_sec;
			} else
				nextIdleSoundTime = currentTime + idleSoundFreq_sec * 0.5f + Random.value * idleSoundFreq_sec;
		}
	}

	private void PlayHurtSound()
	{
		SoundManager.PlayRandomSound(this, hurtSounds, randomPitchRange, hurtVolume);
	}

	private AudioClip PlayDeathSound()
	{
		return SoundManager.PlayRandomSound(this, deathSounds, randomPitchRange, deathVolume);
	}
}
