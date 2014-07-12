using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigSuckyThing : MonoBehaviour {

	List<Rigidbody> rigidBodies = new List<Rigidbody>();
	public Transform singularity;
	//bool gameover;

	void OnTriggerEnter(Collider collider) {
		rigidBodies.Add (collider.rigidbody);
	}

	void OnTriggerExit(Collider collider) {
		rigidBodies.Remove (collider.rigidbody);
	}

	void FixedUpdate() {
		foreach (Rigidbody rb in rigidBodies) {
			Vector3 to = singularity.position - rb.transform.position;
			Debug.DrawLine (to, rb.transform.position, Color.white);
			rb.AddForce ((to * (100 - to.magnitude)) * .1f);
		}
	}

	public void gameOver() {
		//gameover = true;
	}

}
