using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager INSTANCE;

	public Transform pointSoundPrefab;

	void Start()
	{
		INSTANCE = this;
	}

	void Update()
	{

	}

	protected static readonly float[] RANDOM_PITCH_RANGE = { 0.9f, 1.1f };

	/// <summary>
	/// Returns the AudioClip played.
	/// </summary>
	public static AudioClip PlayRandomSound(Component gameObject, AudioClip[] sounds)
	{
		AudioSource audio = gameObject.GetComponent<AudioSource>();

		int soundIndex = (int)Mathf.Round(Random.value * (sounds.Length - 1));
		audio.clip = sounds[soundIndex];

		audio.pitch = Random.value * (RANDOM_PITCH_RANGE[1] - RANDOM_PITCH_RANGE[0]) + RANDOM_PITCH_RANGE[0];

		audio.Play();
		return audio.clip;
	}

	public static void PlayRandomSoundAtPoint(AudioClip[] sounds, Vector3 point, Transform parent = null)
	{
		Transform pointSound;
		if (parent)
			pointSound = Instantiate(INSTANCE.pointSoundPrefab, point, Quaternion.identity, parent);
		else
			pointSound = Instantiate(INSTANCE.pointSoundPrefab, point, Quaternion.identity);

		AudioClip sound = PlayRandomSound(pointSound, sounds);
		Destroy(pointSound.gameObject, sound.length);
	}
}
