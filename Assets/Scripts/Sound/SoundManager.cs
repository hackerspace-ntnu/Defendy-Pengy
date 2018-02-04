using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager THIS;

	public AudioClip stinger;
	public AudioClip crowSound;

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

	private static readonly float[] RANDOM_PITCH_RANGE = { 0.9f, 1.1f };

	/// <summary>
	/// Returns the AudioClip played.
	/// </summary>
	public static AudioClip PlayRandomSound(Component gameObject, AudioClip[] sounds)
	{
		AudioSource audio = gameObject.GetComponent<AudioSource>();

		int soundIndex = (int)Mathf.Round(Random.value * (sounds.Length - 1));
		try
		{
			audio.clip = sounds[soundIndex];
		} catch (System.IndexOutOfRangeException)
		{
			Debug.LogError(gameObject.name + " is missing some sounds. Playing \"" + THIS.DEBUG_SOUND.name + "\" instead.");
			audio.clip = THIS.DEBUG_SOUND;
		}

		audio.pitch = Random.value * (RANDOM_PITCH_RANGE[1] - RANDOM_PITCH_RANGE[0]) + RANDOM_PITCH_RANGE[0];

		audio.Play();
		return audio.clip;
	}

	public static void PlayRandomSoundAtPoint(AudioClip[] sounds, Vector3 point, Transform parent = null)
	{
		Transform pointSound;
		if (parent)
			pointSound = Instantiate(THIS.pointSoundPrefab, point, Quaternion.identity, parent);
		else
			pointSound = Instantiate(THIS.pointSoundPrefab, point, Quaternion.identity);

		AudioClip sound = PlayRandomSound(pointSound, sounds);
		Destroy(pointSound.gameObject, sound.length);
	}
}
