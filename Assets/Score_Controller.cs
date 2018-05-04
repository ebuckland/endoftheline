using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Controller : MonoBehaviour {

	private int Score = 0;

	private Text text;

	// Use this for initialization
	void Start () {
		text = this.GetComponent<Text> ();
	}

	public void updateText() {
		text.text = Score.ToString ();
	}

	public void updateText(int addToScore) {
		Score += addToScore;
		updateText();
	}
}
