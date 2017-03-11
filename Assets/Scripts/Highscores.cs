using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Highscores : MonoBehaviour {

	public Text highscoreText;
	public AudioSource sound;

	string key;
	float highscore;

	// Use this for initialization
	void Start () {
		key = PlayerPrefs.GetString("Player name");
		highscore = PlayerPrefs.GetFloat (key);
		if (highscore > 0) {
			highscoreText.text = (key + " : " + highscore);
		} else
			highscoreText.text = "No Highscore";
	}

	void Update (){
		if (Input.GetKeyDown("escape")){
			sound.Play ();
			SceneManager.LoadScene ("Menu");

		}
	}


}
