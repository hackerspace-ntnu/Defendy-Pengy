using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class QuitGame : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private Hand leftHand;
	private Hand rightHand;

	private GameObject collidingObject;
	private GameObject objectInHand;

	bool isHoldingDownButton;


	void Awake() {
		leftHand = Player.instance.leftHand;
		rightHand = Player.instance.rightHand;
		//trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update() {
		if(GetComponent<Collider>().bounds.Contains(leftHand.gameObject.transform.position)) {
			gameObject.transform.localScale = new Vector3 (0.006f, 0.006f, 0.006f);
			if (leftHand.GetStandardInteractionButtonDown()) {
				Application.Quit ();
			}
			return;
		} 
		if(GetComponent<Collider>().bounds.Contains(rightHand.gameObject.transform.position)) {
			gameObject.transform.localScale = new Vector3 (0.006f, 0.006f, 0.006f);
			if (rightHand.GetStandardInteractionButtonDown()) {
				Application.Quit ();
			}
			return;
		}
		//gameObject.transform.localScale = new Vector3 (0.005f, 0.005f, 0.005f);
	}

	private void SetCollidingObject(Collider col) {
		collidingObject = col.gameObject;
	}
	void OnCollisionEnter(){
		print("collision");
	}
	public void OnTriggerEnter(Collider other) {
		print ("tyrigger");
		SetCollidingObject(other);
	}

	public void OnTriggerStay(Collider other) {
		SetCollidingObject(other);
	}

	public void OnTriggerExit(Collider other) {
		if(!collidingObject) {
			return;
		}
		collidingObject = null;
	}

	private void GrabObject() {

		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	private FixedJoint AddFixedJoint() {
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject() {
		if(GetComponent<FixedJoint>()) {
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());


		}

		objectInHand = null;
	}
}
