using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI_ImportantMessage : MonoBehaviour {
	public GameObject modelGameOver;
	public GameObject modelGameWin;
	public Transform cam;
	private bool isShowing = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isShowing){
			var camPos = cam.transform.position;
			camPos = new Vector3(camPos.x, camPos.y - 1f, camPos.z);
			camPos = Vector3.Lerp(transform.position, camPos, 0.01f* Mathf.Pow(((transform.position- camPos).magnitude),2));
			transform.position = camPos;
			transform.Rotate(0, 0.1f, 0);
		}
	}
	public void Show(string message){
		isShowing = true;
		transform.position = cam.position;
		GameObject textModel = null;
		if (message == "Game Over"){
			textModel = modelGameOver;
		}
		if (message == "Game Win")
		{
			textModel = modelGameWin;
		}

		if (cam != null && textModel != null){
			Instantiate(textModel, transform);
			Instantiate(textModel, transform);
			Instantiate(textModel, transform);
			Instantiate(textModel, transform);
		}
		else {
			print("Can't show ImportantMessage. Assign the needed GameObjects!");
		}
	}
	public void Hide(){
		isShowing = false;
		foreach (Transform child in transform){
			Destroy(child.gameObject);
		}
	}

}
