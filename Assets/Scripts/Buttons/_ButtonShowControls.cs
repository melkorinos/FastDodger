using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class _ButtonShowControls : MonoBehaviour {

	public AudioSource sound;

	public void LoadScene ()
	{
		sound.Play ();
		//SceneManager.LoadScene ("Controls");

		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowAchievementsUI ();

		}
	}
}
