using UnityEngine;

public class SpinAround : MonoBehaviour {
	void Start () {
		transform.Rotate (Vector3.up * Random.Range (0f, 360f));
	}
	
	void Update () {
		transform.position = new Vector3 (transform.position.x, 0.3f + 0.1f * Mathf.Sin (0.2f * Time.time) + 0.7f, transform.position.z);
		transform.Rotate (Vector3.up * (10 * Time.deltaTime + 0.08f * Mathf.Sin(Time.time) ));
	}
}
