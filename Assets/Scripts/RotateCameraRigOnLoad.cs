using UnityEngine;

public class RotateCameraRigOnLoad : MonoBehaviour
{
	public Camera VRCamera_eye;
	public Transform lookAtObject;

	void Start()
	{
		// Rotates the camera rig to look at lookAtObject, saves the rotated vector, and resets back to its starting rotation
		Vector3 startRotation = transform.forward;
		transform.LookAt(lookAtObject);
		Vector3 lookAtVector = transform.forward;
		transform.forward = startRotation;

		Vector3 eyeVector = VRCamera_eye.transform.forward;
		// Discards the y component to only rotate the camera rig horizontally
		lookAtVector.y = 0;
		eyeVector.y = 0;
		// Measures the angle between where the eye is looking and where the eye would have been looking if it was looking towards lookAtObject
		float angle = Vector3.SignedAngle(eyeVector, lookAtVector, Vector3.up);
		transform.Rotate(Vector3.up, angle);
	}
}
