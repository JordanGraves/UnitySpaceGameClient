using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class HighScoreManager : MonoBehaviour {

	World mWorld;

	bool gameover = false;
	bool showScores = true;
	ScoreTable<Score> scoresTable = new ScoreTable<Score>();
	string highScores;
	public GUIText highScoresText;
	public GUIText scoreHolder;
	int currentScore;
	bool getName = false;
	GUIText pressSpace;
	public GUIText enterNameText;
	string name = "(Type your name)";
	string stringTypeYourName = "(Type your name)";

	void OnGui() {

	}

	// Use this for initialization
	void Start () {
		// This is just stupid
		pressSpace = GameObject.FindGameObjectWithTag ("PressSpace").GetComponent<GUIText>();
		mWorld = GameObject.FindGameObjectWithTag ("World").GetComponent<World> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (getName) {
			highScoresText.text = "";
			enterNameText.text = "New Highscore.\n\n" + name;
			foreach (char c in Input.inputString) {
				if (c == "\b"[0]) {
					if (name.Equals(stringTypeYourName)) {
						return;
					}
					if (name.Length != 0) {
						name = name.Substring(0, name.Length - 1);
						if (name.Equals(""))
							name = stringTypeYourName;
				
					}
				}
				else if (c == "\n"[0] || c == "\r"[0]) {
					if (name.Equals(stringTypeYourName))
						return;
					if (nameAccepted(name)) {
						enterNameText.text = "OK. " + name + ", hold on a sec.";
						mWorld.gettingName = false;
						getName = false;
						StartCoroutine(postNewScore(name, (float)currentScore));
					}
				}
				else {
					if (name.Equals(stringTypeYourName)) {
						name = "";
					}
					name += c;
				}
			}
		}
	}
	
	public bool nameAccepted(string _name) {
		return _name.Length > 0;
	}

	IEnumerator postNewScore(string _name, float _score) {
		string stringData = "{\"name\":\"" + _name + "\", \"score\": " + _score + " }";
		byte[] byteData = Encoding.ASCII.GetBytes(stringData.ToCharArray());
		print("Post new Score, data: " + byteData);
		Hashtable headers = new Hashtable();
		headers.Add("Content-Type", "application/json");
		WWW www = new WWW("http://rainbow-space-ninja.appspot.com/scores", byteData, headers);
		yield return www;
		Debug.Log ("Returned from POST: " + www.text + " headers: " + www.responseHeaders);

		highScores = www.text;
		JSONObject json = new JSONObject (highScores);
		List<JSONObject> scores = json.GetField ("scores").list;
		reset ();
		string textToShow = "Top 10:\n";
		foreach (JSONObject score in scores) {
			// scoresTable.Add(new Score(score.GetField("name").str, score.GetField("score").f));
			textToShow += score.GetField("name").str + "   " + score.GetField("score") + "\n";
		}
		print (textToShow);
		highScoresText.text = textToShow;
		pressSpace.text = "Press [SPACE] to play again";
	}

	IEnumerator StartRequest() {
		WWW www = new WWW ("http://rainbow-space-ninja.appspot.com/scores");
		highScoresText.text = "Getting high scores.\nPress [SPACE] to skip.";
		Debug.Log ("Requesting " + www.url);
		yield return www;
		highScores = www.text;
		Debug.Log ("Returned: " + highScores);
		JSONObject json = new JSONObject (highScores);
		List<JSONObject> scores = json.GetField ("scores").list;
		string textToShow = "Top 10:\n";
		foreach (JSONObject score in scores) {
			print (score);

			scoresTable.Add(new Score(score.GetField("name").str, score.GetField("score").f));
			textToShow += score.GetField("name").str + "   " + score.GetField("score") + "\n";
		}

		if (scoresTable.Count < 10) {
			getName = true;
			mWorld.gettingName = true;
			return true;
		}

		for(int i = 0; i < scoresTable.Count; i++) {
			Score score = scoresTable[i];
			if (currentScore > (int)score.score) {
				getName = true;
				mWorld.gettingName = true;
				name = stringTypeYourName;
				return true;
			}
		}
		highScoresText.text = textToShow;
		pressSpace.text = "Press [SPACE] to play again";
	}

	public void showHighScores() {
		StartCoroutine(StartRequest ());
	}

	public void gameOver() {
		print ("Score Holder: " + scoreHolder.text);
		currentScore = int.Parse(scoreHolder.text);
		gameover = true;
	}

	public void reset () {
		pressSpace.text = "";
		highScoresText.text = "";
		enterNameText.text = "";
		getName = false;
		name = stringTypeYourName;
	}

	class ScoreTable<Score> : List<Score> {

		public float highestScore() {
			return 0.0f;
		}
	}

	class Score {
		public string name;
		public float score;

		public Score(string _name, float _score) {
			this.name = _name;
			this.score = _score;
		}
	}
}
