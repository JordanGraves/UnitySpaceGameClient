using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public Rigidbody[] rigidBodies;
	public Animator animator;
	float movementSpeed = .075f;	
	float thresholdRight = 10f;
	float thresholdLeft = -10f;

//	BoxCollider oneHandJumpCollider; 
	List<Collider> jumpOverCandidates = new List<Collider> ();
	public World mWorld;

	Vector3 playerStartPosition;
	Quaternion playerStartRotation;

	void Start() {
//		oneHandJumpCollider = this.GetComponent<BoxCollider>();
		animator = this.GetComponent<Animator> ();
		playerStartPosition = this.transform.position;
		playerStartRotation = this.transform.rotation;
	}

	void Update () {
		if (Input.GetKey (KeyCode.DownArrow)) {
			this.slide();
		}
		else 
			animator.SetBool ("Slide", false);
		if (Input.GetKey (KeyCode.UpArrow)) {
			this.jump ();
		} else {
			animator.SetBool ("Jump", false);
			animator.SetBool("OneHandedJump", false);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			this.moveTo (this.transform.position.x - 1, this.transform.position.y, this.transform.position.z); 
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			this.moveTo(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z); 
		}
		if (!Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow)) {
			animator.SetFloat("Direction", Mathf.Lerp(animator.GetFloat("Direction"), .5f, .3f));
		}
	}

	void slide() {
		animator.SetBool ("Slide", true);
	}

	void jump() {
		foreach (Collider jumpOverCandidate in jumpOverCandidates) {
			bool jumpOver = true;
			Vector3[] vertices = new Vector3[8];	
			Matrix4x4 thisMatrix = jumpOverCandidate.transform.localToWorldMatrix;
			Quaternion storedRotation = jumpOverCandidate.transform.rotation;
			jumpOverCandidate.transform.rotation = Quaternion.identity;
			Vector3 extents = jumpOverCandidate.bounds.extents;
			vertices[0] = thisMatrix.MultiplyPoint3x4(extents);
			vertices[1] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z));
			vertices[2] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, -extents.z));
	        vertices[3] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, -extents.z));
	        vertices[4] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, extents.z));
	        vertices[5] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, -extents.y, extents.z));
	        vertices[6] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, -extents.z));
	        vertices[7] = thisMatrix.MultiplyPoint3x4(-extents);
			jumpOverCandidate.transform.rotation = storedRotation;
			foreach (Vector3 point in vertices) {
				if(point.y > 2.4)
					jumpOver = false;
			}
			if (jumpOver == true) {
				jumpOverCandidate.transform.tag = "Interacting";
				animator.SetBool ("OneHandedJump", true);
				return;
			}
		}
		animator.SetBool ("Jump", true);
	}

	void OnTriggerEnter(Collider collider) {
		//jumpOverCandidates.Add (collider);
	}
	void OnTriggerExit(Collider collider) {
		//jumpOverCandidates.Remove (collider);
	}

	void moveTo(Vector3 pos) {
		if (pos.x - this.transform.position.x >= 0) {
			animator.SetFloat("Direction", Mathf.Lerp(animator.GetFloat("Direction"), 1f, .1f));
		}
		else if (pos.x - this.transform.position.x <= 0) {
			animator.SetFloat("Direction", Mathf.Lerp(animator.GetFloat("Direction"), 0f, .1f));
		}
		if (this.transform.position.x + pos.x > thresholdRight) {
			return;
		}
		if (this.transform.position.x + pos.x < thresholdLeft) {
			return;
		}
		this.transform.position = Vector3.Lerp(this.transform.position, pos, movementSpeed);
	}

	void moveTo(float x, float y, float z) {
		moveTo(new Vector3(x,y,z));
	}	

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag.Equals("Interacting"))
		    return;
		die (collision);
	}

	void OnCollisionExit(Collision collision) {
		if (collision.collider.tag.Equals("Interacting"))
			collision.collider.tag = "Untagged";
	}

	public void die(Collision collision) {
		mWorld.playerDead ();
		animator.enabled = false;
		foreach (Rigidbody rb in rigidBodies) {
			rb.isKinematic = false;
		}
		rigidBodies [rigidBodies.Length - 1].AddForce (Vector3.forward * 100);
	}

	public void getSuckedIntoBlackHole() {
		//this.transform.position = new Vector3(0, 1000, 1000);
		foreach (Rigidbody rb in rigidBodies) {
			try {
				rb.isKinematic = false;
				rb.velocity = new Vector3(0, 0, 0);
				rb.angularVelocity = new Vector3(0,0,0);

			} catch {
				continue;
			}
			rb.constraints = RigidbodyConstraints.FreezeAll;
			rb.isKinematic = true;
		}
	}

	public void reset() {
		this.transform.position = playerStartPosition;
		this.transform.rotation = playerStartRotation;
		animator.enabled = true;
		animator.SetBool ("Dead", false);
		foreach (Rigidbody rb in rigidBodies) {
			rb.constraints = RigidbodyConstraints.None;
			rb.isKinematic = true;
		}
	}
	
}
