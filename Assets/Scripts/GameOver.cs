using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameOver : MonoBehaviour {

	public GameObject gameOverScreen;
	public GameObject gameTimeObject;
	public GameObject newHighscore;
	public Text secondsSurvivedUI; 
	public Text gameTime;
	bool gameOver;

	AudioSource loose;

	string key;
	public float highscore = 0;

	long finalTime;

	// Use this for initialization
	void Start () {

		//subscribe gameover to on player death event
		FindObjectOfType<PlayerController> ().onPlayerDeath += OnGameOver;
		
		//Get the player name and current highscores
		key = PlayerPrefs.GetString("Player name");
		highscore = PlayerPrefs.GetFloat (key, 0.0f);

		loose = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		gameTime.text = Mathf.RoundToInt( Time.timeSinceLevelLoad).ToString ();

		if (gameOver) {
			if ((Input.GetKeyDown("space")) || (Input.touchCount ==1)){
				SceneManager.LoadScene ("Main");
			}

			if (Input.GetKeyDown("escape")){
				SceneManager.LoadScene ("Menu");
			}

		}
	}

	//display game over screen
	void OnGameOver (){
		loose.Play ();
		gameTimeObject.SetActive (false);
		gameOverScreen.SetActive (true);
		finalTime = Convert.ToInt64( Time.timeSinceLevelLoad * 100);
			secondsSurvivedUI.text = Time.timeSinceLevelLoad.ToString("0.00");
		gameOver = true;

		//set highscore
		if (Time.timeSinceLevelLoad > highscore) {
			//display the highscore message on canvas
			newHighscore.SetActive (true);
			PlayerPrefs.SetFloat (key, Time.timeSinceLevelLoad);
			PlayerPrefs.Save ();
		}

		Debug.Log (finalTime);

		//get achievement
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.ReportProgress (GPGSIds.achievement_first_game,100f, (bool success) => 
				{Debug.Log("(Lollygagger) Welcome Unlock: " + success);}
			);

			PlayGamesPlatform.Instance.ReportScore(finalTime, GPGSIds.leaderboard_highscores, (bool success1) =>
				{
					Debug.Log("(Lollygagger) Leaderboard update success: " + success1);
				});
		}


	}
}
