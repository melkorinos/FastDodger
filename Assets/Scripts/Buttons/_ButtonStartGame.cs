using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class _ButtonStartGame : MonoBehaviour {

	public AudioSource sound;

	GameObject playerNameBox;
	InputField playerName;
	public GameObject loginElement;
	Text loginButtonText;

	void Start (){
		//get the input field 
		playerNameBox = GameObject.Find ("Player Name");
		playerName = playerNameBox.GetComponent<InputField> ();
		//loginElement = GameObject.Find ("Login");
		loginButtonText = GetComponentInChildren<Text> ();

		PlayGamesClientConfiguration clientConfig = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.DebugLogEnabled = true;

		PlayGamesPlatform.InitializeInstance (clientConfig);
		PlayGamesPlatform.Activate ();

		PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
	}

	void Update() {
		if (Input.GetKeyDown("return")  || Input.GetKeyDown("enter")) SignIn();
	}


	public void SignIn (){
		// if player gave a name , save his name and load the game
		sound.Play ();

		if (playerName.text != "" && playerName.text != "Please enter a name") {
			
			PlayerPrefs.SetString ("Player name", playerName.text);
			PlayerPrefs.Save ();

			// LOAD THE SCENE EVENTUALLY OR FROM ANOTHER BUTTON
			//SceneManager.LoadScene ("Main");


			if (!PlayGamesPlatform.Instance.localUser.authenticated) {
				PlayGamesPlatform.Instance.Authenticate (SignInCallback, false);
			} else {
				PlayGamesPlatform.Instance.SignOut ();
				loginButtonText.text = "Log In";
				playerName.text = "Signed Out";
			}
				
		} else
			playerName.text = "Please enter a name";
	}

	public void SignInCallback(bool success) {
		if (success) {
			Debug.Log("(Lollygagger) Signed in!");

			// Change sign-in button text
			loginButtonText.text = "Log Out";

			// Show the user's name
			playerName.text = "Signed in as: " + Social.localUser.userName;
		} else {
			Debug.Log("(Lollygagger) Sign-in failed...");

			// Show failure message
			loginButtonText.text = "Log In";
			playerName.text = "Sign-in failed";
		}
	}




}
