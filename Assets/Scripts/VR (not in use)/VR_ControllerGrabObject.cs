using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    //private Transform VRRigTransform;

    private GameObject collidingObject;
    private GameObject objectInHand;

    bool isHoldingDownButton;


    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //VRRigTransform = this.transform.root;
    }

    void Update() {
        if(Controller.GetHairTriggerDown()) {
            if(collidingObject) {
                GrabObject();
            }
        } 

        if(Controller.GetHairTriggerUp()) {
            if(objectInHand) {
                ReleaseObject();
            }
        }
    }

    private void SetCollidingObject(Collider col) {
        if(collidingObject || !col.GetComponent<Rigidbody>()) {

            return;
        }
        else {
            collidingObject = col.gameObject;
        }

    }

    public void OnTriggerEnter(Collider other) {
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



            var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            if(origin != null) {
                objectInHand.GetComponent<Rigidbody>().velocity = origin.TransformVector(Controller.velocity);
                objectInHand.GetComponent<Rigidbody>().angularVelocity = origin.TransformVector(Controller.angularVelocity);
            } else {
                objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
                objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
            }

        }

        objectInHand = null;
    }
}
