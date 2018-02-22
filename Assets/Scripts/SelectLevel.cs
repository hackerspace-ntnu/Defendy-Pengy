using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class SelectLevel : MonoBehaviour
{
	public AudioClip hoverSound;
	public AudioClip selectSound;

	public string level;

	private bool isLoadingLevel = false;

	void OnHandHoverBegin(Hand hand)
	{
		transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
		SoundManager.PlaySoundAtPoint(hoverSound, transform.position, 0.5f);
	}

	void HandHoverUpdate(Hand hand)
	{
		if (hand.controller.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
		{
			if (!isLoadingLevel)
			{
				SoundManager.PlaySoundAtPoint(selectSound, transform.position, 0.5f);
				Invoke("LoadLevel", selectSound.length);
				isLoadingLevel = true;
			}
		}
	}

	private void LoadLevel()
	{
		SceneManager.LoadScene(level);
	}

	void OnHandHoverEnd(Hand hand)
	{
		transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
