using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class _ButtonShowHighscores : MonoBehaviour {

	public AudioSource sound;

	public void LoadScene ()
	{
		sound.Play ();
		//SceneManager.LoadScene ("Highscores");

		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI ();

		}
	}
}
