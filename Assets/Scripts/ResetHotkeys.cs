using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ResetHotkeys : MonoBehaviour
{
	public SceneAsset sceneLoadedWhenReset;
	public Player player;

	private SteamVR_Controller.Device[] controllers = null;

	private float resetTriggerStartTime = -1;
	private const float RESET_BUTTON_DURATION = 1f; // seconds


	private bool AreResetButtonsPressed()
	{
		if (controllers == null)
		{
			// The controller fields might be null while some VR stuff is starting up
			if (player.leftController == null || player.rightController == null)
				return false;

			controllers = new SteamVR_Controller.Device[] { player.leftController, player.rightController };
		}

		return controllers[0].GetPress(EVRButtonId.k_EButton_ApplicationMenu)
			&& controllers[1].GetPress(EVRButtonId.k_EButton_ApplicationMenu); // menu buttons on both left and right controllers
	}

	void Update()
	{
		if (AreResetButtonsPressed())
		{
			if (resetTriggerStartTime < 0f)
				resetTriggerStartTime = Time.time;
			else if (Time.time > resetTriggerStartTime + RESET_BUTTON_DURATION)
				LoadScene();
		} else if (resetTriggerStartTime > 0f)
			resetTriggerStartTime = -1;

		if (Input.GetKeyDown(KeyCode.R))
			LoadScene();
	}

	private void LoadScene()
	{
		SceneManager.LoadScene(sceneLoadedWhenReset.name);
	}
}
