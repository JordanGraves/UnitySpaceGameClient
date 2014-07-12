using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	public GUIText timerText;
	float time = 0.0f;
	bool ticking = true;
	bool gameover = false;
	Vector3 gameOverPosition;
	Vector3 startPosition;
	bool showScores = true;
	public HighScoreManager mHighScoreManager;

	// Use this for initialization
	void Start () {
		ticking = false;
		startPosition = guiText.transform.position;
		gameOverPosition = new Vector3 (MapInterval((float)(Screen.width / 2),
		                                            0, (float)Screen.width, 
		                                            0, 1) , 
		                                MapInterval((float)(Screen.height * .9), 
		            								0, (float)Screen.height, 
		            								0, 1), 
		                                0);
	}
	
	// Update is called once per frame
	void Update () {
		if (ticking) {
			time += Time.deltaTime;
			int t = (int)time;
			timerText.text = t.ToString ();
		} else if (gameover) {
			moveToGameOverPosition();
			if (showScores && this.transform.position.magnitude - gameOverPosition.magnitude < 5) {
				showScores = false;
				mHighScoreManager.showHighScores();
			}
		}
	}

	public void start() {
		ticking = true;
	}

	public void stop() {
		ticking = false;
	}

	public void gameOver() {
		int t = (int)time;
		timerText.text =  "Your score: " + t;
		gameover = true;
	}

	public void moveToGameOverPosition() {
		timerText.transform.position = Vector3.Lerp (timerText.transform.position, gameOverPosition, .05f);
	}

	float MapInterval(float val, float srcMin, float srcMax, float dstMin, float dstMax) {
		if (val>=srcMax) return dstMax;
		if (val<=srcMin) return dstMin;
		return dstMin + (val-srcMin) / (srcMax-srcMin) * (dstMax-dstMin);
	}  

	public void reset() {
		time = 0;
		gameover = false;
		showScores = true;
		ticking = true;
		timerText.transform.position = startPosition;
	}
}
