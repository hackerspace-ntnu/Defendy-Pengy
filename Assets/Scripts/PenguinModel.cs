using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinModel : MonoBehaviour
{
	public Transform cameraTransform;

	private float relativeY;

	void Start()
	{
		relativeY = this.transform.position.y;
	}

	void LateUpdate()
	{
		Vector3 position = cameraTransform.position;
		position.y += relativeY;
		this.transform.position = position;

		Quaternion rotation = this.transform.rotation;
		Vector3 eulerRotation = rotation.eulerAngles;
		eulerRotation.y = cameraTransform.rotation.eulerAngles.y;
		rotation.eulerAngles = eulerRotation;
		this.transform.rotation = rotation;
	}
}
