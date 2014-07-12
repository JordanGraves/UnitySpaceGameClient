using UnityEngine;
using System.Collections;

public class PlayerBodyPart : MonoBehaviour {

	PlayerController player;

	void Start () {
		player = transform.root.GetComponent<PlayerController> ();
	}
	
	void OnCollisionEnter(Collision collision) {
		player.die (collision);
		foreach (ContactPoint contactPoint in collision.contacts) { 
			if (collision.rigidbody != null) {
				rigidbody.AddForceAtPosition(collision.rigidbody.velocity * 10, contactPoint.point);
			}
		}
	}
}
