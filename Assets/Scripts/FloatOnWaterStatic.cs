using UnityEngine;

public class FloatOnWaterStatic : MonoBehaviour {
	public float moveAmount = 1f;
	private float originalPosY;

	void Start () {
		originalPosY = transform.position.y;
	}

	void Update () {
		transform.position = new Vector3 (transform.position.x,originalPosY + Mathf.Sin (Time.time)*0.04f*moveAmount,transform.position.z);
	}
}
