using UnityEngine;
using System.Collections;
//using JSONObject;

public class World : MonoBehaviour {

	public int portalZ;
	public Transform[] flyingObjects;
	public PlayerController player;
	public MyCameraController mCamera;
	public BigSuckyThing mBigSuckyThing;
	public ResetArea mResetArea;
	public GUIController mTimer;
	public HighScoreManager mHighScoreManager;
	public bool gameover = false;
	public GameObject flyingObstaclesRoot;
	public GUIText startText;
	public float colorSpeed;
	public bool gettingName = false;
	int colorDirection = 1;

	bool started = false;

	void Start () {
		Rigidbody[] rigidBodies = flyingObstaclesRoot.GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody rb in rigidBodies) {
			rb.isKinematic = true;
		}
		portalZ = (int)GameObject.FindGameObjectWithTag("Portal").transform.position.z;
		//player = (PlayerController)GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!started) {
			if (Input.GetKey(KeyCode.Space)) {
			    started = false;
				reset();
				return;
			}
			float delta = colorSpeed * Time.deltaTime;
			if (startText.color.g > .9) {
				colorDirection = -1;
			}
			if (startText.color.g < .3 ) {
				colorDirection = 1;
			}
			Color tempColor = startText.color;
			tempColor.g += delta*colorDirection; 
			startText.color = tempColor;
		}

		if (gameover && !gettingName && Input.GetKey(KeyCode.Space)) {
			reset ();
		}

// 		Use dis to be cheatin'
//		if (Input.GetKey(KeyCode.R)) {
//			reset();
//		}
	}

	void reset() {
		started = true;
		startText.text = "";
		player.reset ();
		mCamera.reset();
		mTimer.reset ();
		mHighScoreManager.reset ();
		gameover = false;
		Rigidbody[] rigidBodies = flyingObstaclesRoot.GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody rb in rigidBodies) {
			rb.isKinematic = false;
			rb.position = new Vector3 (Random.Range(-7, 7), Random.Range(3, 7), Random.Range(portalZ + 10, portalZ + 100));
			rb.velocity = new Vector3 (0, 0, 0);
			rb.rotation = new Quaternion (0, 0, 0, 1);
		}
	}

	public void gameOver() {
		if (!gameover) {
			mResetArea.gameOver ();
			mBigSuckyThing.gameOver ();
			player.getSuckedIntoBlackHole ();
			mCamera.freeze ();
			mHighScoreManager.gameOver ();
			// Timer.gameOver must be called after mScoreManager
			mTimer.gameOver ();
			gameover = true;
		}
	}

	public void playerDead() {
		mTimer.stop ();
		mCamera.playerDead();
	}

	IEnumerator StartRequest() {
		WWW www = new WWW ("http://rainbow-space-ninja.appspot.com/scores");
		Debug.Log ("Retquesting " + www.url);
		yield return www;
		Debug.Log ("Returned");
		Debug.Log ("Text: " + www.text);
		Debug.Log ("Bytes: " + www.bytes);
	}

}
