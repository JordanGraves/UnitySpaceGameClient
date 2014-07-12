using UnityEngine;
using System.Collections;

public class ResetArea: MonoBehaviour {

	public World world;
	int portalZ;	
	int xMin;
	int xMax;
	int yMin;
	int yMax;
//	bool gameover = false;

	void Start() {
		portalZ = (int)GameObject.FindGameObjectWithTag("Portal").transform.position.z;;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			world.gameOver();
			return;
		 }
		other.rigidbody.position = new Vector3 (Random.Range(-7, 7), Random.Range(3, 7), Random.Range(portalZ + 10, portalZ - 10));
		other.rigidbody.velocity = new Vector3 (0, 0, 0);
		other.rigidbody.rotation = new Quaternion (0, 0, 0, 1);
	}

	public void gameOver() {
//		gameover = true;
	}
}
