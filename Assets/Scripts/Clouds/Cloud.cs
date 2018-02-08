using UnityEngine;

public class Cloud : MonoBehaviour {
	void Start () {
		Destroy (gameObject, 1000f);
	}
	
	void Update () {
		gameObject.transform.Translate (0.02f, 0f, 0f);
	}
}
