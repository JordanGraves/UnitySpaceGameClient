using UnityEngine;
using System.Collections;

public class PathCorrection : MonoBehaviour {

	Rigidbody rb;
	Transform t;

	int xConstrain = 5;

	// Use this for initialization
	void Start () {
		rb = this.rigidbody;
		t = this.transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (t.position.x > xConstrain) {
			rb.AddForce (-10, 0, 0);
		} else if (t.position.x < -xConstrain) {
			rb.AddForce(10, 0, 0);
		}
	}
}
