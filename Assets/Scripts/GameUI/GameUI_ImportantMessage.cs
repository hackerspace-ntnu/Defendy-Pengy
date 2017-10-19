using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI_ImportantMessage : MonoBehaviour {
	public GameObject textModel;
	public Transform cam;
	// Use this for initialization
	void Start () {
		var tA = Instantiate(textModel, transform);
		var tB = Instantiate(textModel, transform);
		var tC = Instantiate(textModel, transform);
		var tD = Instantiate(textModel, transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (cam != null) {
			var camPos = cam.transform.position;
			camPos = Vector3.Lerp(transform.position, camPos, 0.05f);
			transform.position = camPos;
			transform.Rotate(0, 0.1f, 0);
		}
	}

}
