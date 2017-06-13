using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class FpsController : MonoBehaviour
{
	public float mouseSensitivity_x = 1.0f;
	public float mouseSensitivity_y = 1.0f;

	public float walkSpeed = 10.0f;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;

	public float jumpForce = 220.0f;
	bool grounded;
	public LayerMask groundedMask;

	Transform cameraT;
	float verticalLookRotation;

	Rigidbody rigidbodyR;

	bool cursorVisible;

	void Start()
	{
		cameraT = Camera.main.transform;
		rigidbodyR = GetComponent<Rigidbody>();
		LockMouse();
	}

	void Update()
	{
		// Camera movement
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity_x);
		verticalLookRotation += Input.GetAxis ("Mouse Y") * mouseSensitivity_y;
		verticalLookRotation = Mathf.Clamp (verticalLookRotation, -60, 60);
		cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

		// Movement control
		Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		Vector3 targetMoveAmount = moveDirection * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f);

		// Jump control
		if (Input.GetButtonDown("Jump"))
		{
			if (grounded)
				rigidbodyR.AddForce(transform.up * jumpForce);
		}

		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1 + 0.1f, groundedMask))
			grounded = true;
		else
			grounded = false;

		// Lock/unlock mouse on click
		if (Input.GetMouseButtonUp(0))
		{
			if (!cursorVisible)
				UnlockMouse();
			else
				LockMouse();
		}
	}

	void FixedUpdate()
	{
		rigidbodyR.MovePosition(rigidbodyR.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
	}

	void UnlockMouse()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		cursorVisible = true;
	}

	void LockMouse()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cursorVisible = false;
	}
}
