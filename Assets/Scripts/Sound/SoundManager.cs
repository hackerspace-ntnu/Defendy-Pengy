using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public Transform enemyContainer;
	public Transform sound;

	void Start()
	{

	}

	void Update()
	{
		foreach (Transform child in enemyContainer)
		{
			Enemy_Wolf asdf = child.GetComponent<Enemy_Wolf>();
			Debug.Log(asdf.idleSounds[1]);
		}
	}
}
