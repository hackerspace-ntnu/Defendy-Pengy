using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager THIS;

	public AudioClip stinger;
	public AudioClip crowSound;
	public AudioClip gameLost;
	public AudioClip gameWon;

	public Transform pointSoundPrefab;
	public AudioClip DEBUG_SOUND;

	private AudioSource audioSource;

	void Awake()
	{
		if (THIS == null)
			THIS = this;
		else if (THIS != this)
			Destroy(gameObject);
	}

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public static void PlayStinger()
	{
		THIS.audioSource.PlayOneShot(THIS.stinger);
	}

	public static void PlayCrowSound()
	{
		THIS.audioSource.PlayOneShot(THIS.crowSound);
	}

	public static void PlayLevelLostSound()
	{
		AudioSource levelMusic = THIS.GetComponentInChildren<AudioSource>();
		levelMusic.Stop();
		THIS.audioSource.PlayOneShot(THIS.gameLost);
	}

	public static void PlayWinFanfare()
	{
		AudioSource levelMusic = THIS.GetComponentInChildren<AudioSource>();
		levelMusic.Stop();
		THIS.audioSource.PlayOneShot(THIS.gameLost);
	}

	public static void PlaySoundAtPoint(AudioClip sound, Vector3 point, Transform parent = null)
	{
		AudioSource audio = CreatePointSound(point, parent);
		audio.clip = sound;
		audio.Play();
		Destroy(audio.gameObject, sound.length);
	}

	public static void PlayRandomSoundAtPoint(AudioClip[] sounds, Vector3 point, Transform parent = null)
	{
		AudioSource audio = CreatePointSound(point, parent);
		AudioClip sound = PlayRandomSound(audio, sounds);
		Destroy(audio.gameObject, sound.length);
	}

	public static AudioClip PlayRandomSound(Component gameObject, AudioClip[] sounds)
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
