using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Controls : MonoBehaviour {

	public AudioSource sound;
	public GameObject playerPrefab;

	GameObject doubleTapBox;
	InputField doubleTapMS;

	PlayerController playerCont;
	GameObject testShip;

	// Use this for initialization
	void Start () {
		doubleTapBox = GameObject.Find ("Double tap entry");
		doubleTapMS = doubleTapBox.GetComponent<InputField> ();

		 testShip =  Instantiate (playerPrefab, transform.position, Quaternion.identity) as GameObject;
		playerCont = testShip.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// the custom double tap speed functionality
		if (Input.GetKeyDown("return")  || Input.GetKeyDown("enter")){
			float ms = Single.Parse (doubleTapMS.text);
			if ((ms > 0) && (ms <= 1)) {
				PlayerPrefs.SetFloat ("Double tap MS", ms);
				playerCont.clickDelta = ms;
			} else
				doubleTapMS.text = " 0 < value <= 1";
		}

		if (Input.GetKeyDown("escape")){
			sound.Play ();
			SceneManager.LoadScene ("Menu");

		}

	}
}
