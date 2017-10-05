using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wolf : Enemy
{
	public float speed = 4;

	public AudioClip[] idleSounds;
	public AudioClip[] hurtSounds;
	private float lastIdleSoundTime = -1f;
	private float idleSoundFreq = 5f;
	private const float idleSoundChance = 0.1f;

	protected override void HandleSound()
	{
		if (Time.time + idleSoundFreq >= lastIdleSoundTime)
		{
			if (Random.value <= idleSoundChance)
				PlayRandomSound(idleSounds);

			lastIdleSoundTime += idleSoundFreq;
		}
	}
}
