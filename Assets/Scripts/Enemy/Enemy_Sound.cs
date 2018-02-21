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
	public float maxSilenceDuration_sec = 20f;

	public Vector2 randomPitchRange = new Vector2(0.8f, 1.2f);

	private float nextIdleSoundTime;
	private float lastSoundPlayTime;

	private AudioClip lastPlayedSound;

	void SoundStart()
	{
		nextIdleSoundTime = Time.time;
		lastSoundPlayTime = Time.time;

		enemyManager.EnemyPlaySoundTimeManaged(this, PlayIdleSound, maxSilenceDuration_sec);
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
				PlayIdleSound();
			else // Timeout till next time enemy should try playing
				nextIdleSoundTime = currentTime + idleSoundFreq_sec * 0.5f + Random.value * idleSoundFreq_sec;
		} else if (currentTime > lastSoundPlayTime + maxSilenceDuration_sec)
		{
			enemyManager.EnemyPlaySoundTimeManaged(this, PlayIdleSound, maxSilenceDuration_sec);
		}
	}

	private void PlayIdleSound()
	{
		float currentTime = Time.time;
		AudioClip clip = SoundManager.PlayRandomSound(this, idleSounds, randomPitchRange, idleVolume);
		enemyManager.OnEnemyPlayedSound(this);
		lastSoundPlayTime = currentTime;
		lastPlayedSound = clip;
		// Longer timeout when enemy has just played a sound
		nextIdleSoundTime = currentTime + idleSoundFreq_sec * 1.5f + Random.value * idleSoundFreq_sec;
	}

	private void PlayHurtSound(Component source)
	{
		float currentTime = Time.time;
		if (source is FireballRange)
		{
			if (lastPlayedSound != null
				&& currentTime <= lastSoundPlayTime + lastPlayedSound.length * 1.5f) // 50% extra silence time
				return;
		}

		AudioClip clip = SoundManager.PlayRandomSound(this, hurtSounds, randomPitchRange, hurtVolume);
		enemyManager.OnEnemyPlayedSound(this);
		lastSoundPlayTime = currentTime;
		lastPlayedSound = clip;
	}

	private AudioClip PlayDeathSound()
	{
		AudioClip clip = SoundManager.PlayRandomSound(this, deathSounds, randomPitchRange, deathVolume);
		enemyManager.OnEnemyPlayedSound(this);
		lastSoundPlayTime = Time.time;
		lastPlayedSound = clip;
		return clip;
	}
}
