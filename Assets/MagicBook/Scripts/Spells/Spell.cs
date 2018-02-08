using UnityEngine;

public abstract class Spell : MonoBehaviour
{
	public AudioClip igniteSound;
	public AudioClip loopSound;
	public AudioClip throwSound;
	public AudioClip impactSound;

	protected AudioSource audioSource;
	protected readonly float loopSoundStartVolume = 0.1f;

	protected bool spellStarted = false;

	protected float fadeDuration = 0.3f;
	protected bool fired = false;
	public bool isInitSizing = true;
	public float delayBetweenSpawns = 1f;
	public float playerHoldScalingDuration = 6f;

	protected float playerHoldStartTime;

	public abstract void Fire(Vector3 handDirection);

	public abstract void ShowPreview();
	public abstract void HidePreview();
	protected abstract void Start_Derived();
	protected abstract void Update_Derived();
	protected abstract void FixedUpdate_Derived();
	//public abstract void OnPlayerTakeSpell();
	public abstract void OnPlayerHoldSpell();

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Start()
	{
		Start_Derived();
	}

	void Update()
	{
		Update_Derived();
	}

	void FixedUpdate()
	{
		FixedUpdate_Derived();
	}

	protected void StartLoopSound()
	{
		audioSource.clip = loopSound;
		audioSource.volume = loopSoundStartVolume;
		audioSource.loop = true;
		audioSource.Play();

		playerHoldStartTime = Time.time;
	}

	protected void UpdateLoopSound()
	{
		if (!spellStarted)
		{
			StartLoopSound();
			spellStarted = true;
		}

		audioSource.volume = loopSoundStartVolume + (1 - loopSoundStartVolume) * (Time.time - playerHoldStartTime) / playerHoldScalingDuration;
	}

	protected void PlayThrowSound()
	{
		audioSource.clip = throwSound;
		audioSource.volume = 1f;
		audioSource.loop = false;
		audioSource.Play();
	}

	protected void PlayImpactSound()
	{
		SoundManager.PlaySoundAtPoint(impactSound, transform.position);
	}
}
