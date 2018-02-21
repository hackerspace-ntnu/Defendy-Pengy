using UnityEngine;

public class GameHealthIndicationPenguin : MonoBehaviour, IGameHealthIndicationItem
{
	public AudioClip[] deathSounds;
	public float deathVolume = 1f;
	public Vector2 randomPitchRange = new Vector2(1f, 1f);

	private int health = 1;

	private int deathHash = Animator.StringToHash("Death");

	public int GetHealthAmount()
	{
		return health;
	}

	public void Kill()
	{
		SoundManager.PlayRandomSound(this, deathSounds, randomPitchRange, deathVolume);
		GetComponent<Animator>().SetTrigger(deathHash);
		Destroy(gameObject, 2f);
	}

	public GameObject GetGameObject()
	{
		return gameObject;
	}
}
