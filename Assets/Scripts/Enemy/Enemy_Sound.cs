using UnityEngine;
using System.Collections;

abstract partial class Enemy
{
	public AudioClip[] idleSounds;
	public AudioClip[] hurtSounds;
	public AudioClip[] deathSounds;

	protected float lastIdleSoundTime = -1f;

	protected abstract float IdleSoundFreq { get; }
	protected abstract float IdleSoundChance { get; }

	protected void HandleIdleSound()
	{
		if (Time.time + IdleSoundFreq >= lastIdleSoundTime)
		{
			if (Random.value <= IdleSoundChance)
				SoundManager.PlayRandomSound(this, idleSounds);

			lastIdleSoundTime += IdleSoundFreq;
		}
	}

	protected void PlayHurtSound()
	{
		SoundManager.PlayRandomSound(this, hurtSounds);
	}

	protected void PlayDeathSound()
	{
		SoundManager.PlayRandomSoundAtPoint(deathSounds, transform.position, transform.parent);
	}
}
