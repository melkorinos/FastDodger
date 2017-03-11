/* 
*** Copyright Amsterimi ***
*** melkorinos@gmail.com ***
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class _ButtonBeginPlay : MonoBehaviour {

	//public AudioSource sound;



	void Start (){
	}

	public void LoadScene (){
			//sound.Play ();
			SceneManager.LoadScene ("Main");
		/*if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowAchievementsUI();
		}*/
	}
}
