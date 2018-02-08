using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager THIS;

	public AudioClip stinger;
	public AudioClip crowSound;
	public AudioClip gameLost;
	public AudioClip gameWon;

	public GameObject levelMusicChild;
	public Transform pointSoundPrefab;
	public AudioClip DEBUG_SOUND;

	private AudioSource audioSource;

	void Awake()
	{
		if (THIS == null)
		{
			THIS = this;
			audioSource = GetComponent<AudioSource>();
		} else if (THIS != this)
			Destroy(gameObject);
	}

	public static void PlayStinger()
	{
		THIS.audioSource.PlayOneShot(THIS.stinger);
	}

	public static void PlayCrowSound(Vector3 point)
	{
		AudioSource audio = CreatePointSound(point);
		audio.clip = THIS.crowSound;
		// FIXME: Magic numbers and code repeated in PlaySoundAtPoint()
		audio.minDistance = 10f;
		audio.maxDistance = 100f;
		audio.Play();
		Destroy(audio.gameObject, THIS.crowSound.length);
	}

	public static void PlayLevelLostSound()
	{
		THIS.levelMusicChild.GetComponent<AudioSource>().Stop();
		THIS.audioSource.PlayOneShot(THIS.gameLost);
	}

	public static void PlayWinFanfare()
	{
		THIS.levelMusicChild.GetComponent<AudioSource>().Stop();
		THIS.audioSource.PlayOneShot(THIS.gameWon);
	}

	public static void PlaySoundAtPoint(AudioClip sound, Vector3 point, float volume = 1f, Transform parent = null)
	{
		AudioSource audio = CreatePointSound(point, parent);
		audio.clip = sound;
		audio.volume = volume;
		audio.Play();
		Destroy(audio.gameObject, sound.length);
	}

	public static void PlayRandomSoundAtPoint(AudioClip[] sounds, Vector3 point, float volume = 1f, Transform parent = null)
	{
		AudioSource audio = CreatePointSound(point, parent);
		AudioClip sound = PlayRandomSound(audio, sounds, volume);
		Destroy(audio.gameObject, sound.length);
	}

	public static AudioClip PlayRandomSound(Component gameObject, AudioClip[] sounds, float volume = 1f)
	{
		AudioSource audio = gameObject.GetComponent<AudioSource>();

		AudioClip sound = null;
		try
		{
			sound = ChooseRandomAudioClip(sounds);
		} catch (System.IndexOutOfRangeException)
		{
			Debug.LogError(audio.gameObject.name + " is missing some sounds. Playing \"" + THIS.DEBUG_SOUND.name + "\" instead.");
		}
		if (sound)
			audio.clip = sound;
		else
			audio.clip = THIS.DEBUG_SOUND;

		audio.volume = volume;
		audio.Play();
		return audio.clip;
	}

	private static AudioSource CreatePointSound(Vector3 point, Transform parent = null)
	{
		Transform pointSound;
		if (parent)
			pointSound = Instantiate(THIS.pointSoundPrefab, point, Quaternion.identity, parent);
		else
			pointSound = Instantiate(THIS.pointSoundPrefab, point, Quaternion.identity);

		AudioSource audio = pointSound.GetComponent<AudioSource>();
		return audio;
	}

	private static readonly float[] RANDOM_PITCH_RANGE = { 0.9f, 1.1f };

	private static AudioClip ChooseRandomAudioClip(AudioClip[] sounds)
	{
		int soundIndex = (int)Mathf.Round(Random.value * (sounds.Length - 1));
		return sounds[soundIndex];
	}

	private static float GetRandomPitch()
	{
		return Random.value * (RANDOM_PITCH_RANGE[1] - RANDOM_PITCH_RANGE[0]) + RANDOM_PITCH_RANGE[0];
	}
}
