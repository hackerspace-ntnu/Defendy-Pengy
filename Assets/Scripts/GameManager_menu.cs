using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class GameManager_menu : MonoBehaviour
{
	public Hand leftHand;
	public Hand rightHand;

	private float resetTriggerStartTime = -1;
	private const float RESET_BUTTON_DURATION = 1f; // seconds

	private bool areResetButtonsPressed()
	{
		return leftHand.controller.GetPress(EVRButtonId.k_EButton_ApplicationMenu)
			&& rightHand.controller.GetPress(EVRButtonId.k_EButton_ApplicationMenu); // menu buttons on both left and right controllers
	}

	void Start()
	{

	}

	void Update()
	{
		// TEMPORARY DEVELOPER HOTKEYS:
		if (areResetButtonsPressed())
		{
			if (resetTriggerStartTime < 0f)
				resetTriggerStartTime = Time.time;
			else if (Time.time > resetTriggerStartTime + RESET_BUTTON_DURATION)
				SceneManager.LoadScene("level1");
		} else if (resetTriggerStartTime > 0f)
			resetTriggerStartTime = -1;
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene("level1");
		// END
	}
}
