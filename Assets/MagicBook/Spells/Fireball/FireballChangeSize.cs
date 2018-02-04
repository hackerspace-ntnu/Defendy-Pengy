using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireballChangeSize : MonoBehaviour {
    ParticleSystem ps;
    float curSize;
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        curSize = ps.main.startSize.constant;
	}
	
	// Update is called once per frame
	void Update () {
        var main = ps.main;
        if(Input.GetKey(KeyCode.Mouse0))
            curSize += 1f * Time.deltaTime;
        else if(Input.GetKey(KeyCode.Mouse1))
            curSize -= 1f * Time.deltaTime;

        main.startSize = curSize;
	}
}
