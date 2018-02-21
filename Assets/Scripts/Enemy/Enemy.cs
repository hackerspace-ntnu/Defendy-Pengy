//Timmy Chan and Arne-Martin
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Enemy_Animator))]
public abstract partial class Enemy : MonoBehaviour, IDamagable
{
	public enum Type
	{
		Wolf, Bear, Fox, Seal, PolarBear, Muskox
	}

	protected NavMeshAgent agent;
	public Transform goal;
	public float startHealth = 40f;
	protected float health;
	public GameObject healthBarPrefab;

	private Transform headsetPosition;
	private EnemyHealthBar healthBar;
	private EnemyManager enemyManager;
	private SkinnedMeshRenderer enemySkinnedMeshRenderer;

	public abstract Type type { get; }

	protected abstract float GetBaseSpeed();
	protected abstract float GetSpeedRange();

	void Start()
	{
		enemyManager = transform.parent.GetComponent<EnemyManager>();
		agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;
		SetSpeed();

		//initiate healthbar and finding headsetposition, Arne-Martin
		healthBar = Instantiate(healthBarPrefab, new Vector3(transform.position.x, transform.position.y + 2.4f, transform.position.z), Quaternion.identity)
			.GetComponent<EnemyHealthBar>();
		healthBar.transform.parent = transform;
		headsetPosition = Player.instance.trackingOriginTransform;
		health = startHealth;

		float h, s, v; // hue, saturation, value (brightness)
		float oldV;
		enemySkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

		for (int i = 0; i < enemySkinnedMeshRenderer.materials.Length; i++)
		{
			Color.RGBToHSV(enemySkinnedMeshRenderer.materials[i].color, out h, out s, out v);
			oldV = v;
			v = Random.Range(oldV * 0.5f, oldV * 1.5f);

			if (v > 1f)
				v = 1f;

			enemySkinnedMeshRenderer.materials[i].color = Color.HSVToRGB(h, s, v);
		}

		SoundStart();
	}

	protected void SetSpeed()
	{
		float baseSpeed = GetBaseSpeed();
		float speedRange = GetSpeedRange();
		float randomizedSpeed = Random.Range(baseSpeed - speedRange / 2, baseSpeed + speedRange / 2);
		agent.speed = randomizedSpeed;

		GetComponent<Enemy_Animator>().OnSetSpeed(baseSpeed, randomizedSpeed);
	}

	void Update()
	{
		if (agent.remainingDistance < 1f)
		{
			print(name + " reached goal");
			enemyManager.EnemyReachedGoal(1);
			Destroy(gameObject);
		}

		//To scale healthbar to health, Arne-Martin
		float healthPercentage = health / startHealth;
		healthBar.Display(healthPercentage);
		//Rotate healthbar towards player, Arne-Martin
		Vector3 UpdatedHeadsetPosition = headsetPosition.position;
		healthBar.transform.LookAt(UpdatedHeadsetPosition);

		SoundUpdate();
	}

	public void InflictDamage(float damage, Component source = null)
	{
		health -= damage;
		if (health > 0f)
			PlayHurtSound(source);
		else
			OnDeath();
	}

	private void OnDeath()
	{
		AudioClip chosenClip = PlayDeathSound();

		// Play death animation
		Enemy_Animator animator = GetComponent<Enemy_Animator>();
		animator.OnDeath();

		float timeTillDestroy = Mathf.Max(chosenClip.length, animator.GetDeathAnimationLength());

		// Stop movement and collision
		GetComponent<NavMeshAgent>().speed = 0f;
		GetComponentInChildren<MeshCollider>().enabled = false;

		Destroy(healthBar.gameObject);
		// Destroy game object after death animation has finished playing
		Destroy(gameObject, timeTillDestroy);
		// Destroy this script component to prevent further invocations of Update()
		Destroy(this);
	}
}
