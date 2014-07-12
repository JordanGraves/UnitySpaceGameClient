using UnityEngine;
using System.Collections;

public class MyCameraController : MonoBehaviour {

	public Transform follow;
	public float distanceZ = 3f;
	public float distanceY = 3f;
	bool playerdead = false;
	bool frozen = false;
	Vector3 gameOverPosition;
	Quaternion gameOverRotation;
	Quaternion startRotation;
	Vector3 startPosition;

	void Start() {
		this.camera.clearFlags = CameraClearFlags.Skybox;
		startRotation = this.transform.rotation;
		startPosition = this.transform.position;
		gameOverPosition = new Vector3 (6, 7, -12); 

	}

	void Update () {
		if (!frozen) {
				if (playerdead) {
						this.transform.position = Vector3.Lerp (this.transform.position, gameOverPosition, 0.1f);
						this.transform.LookAt (follow.position);
				} else {
						Vector3 temp = transform.position;
						temp.z = follow.position.z - distanceZ;
						temp.y = follow.position.y + distanceY;
						temp.x = follow.position.x;
						transform.position = temp; 
				}
		}
	}

	public void reset () {
		this.transform.rotation = startRotation;
		this.transform.position = startPosition;
		playerdead = false;
		frozen = false;
	}

	public void playerDead() {
		playerdead = true;
	}

	public void freeze() {
		frozen = true;
	}
}
