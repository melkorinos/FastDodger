using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ButtonQuit : MonoBehaviour {

	public AudioSource sound;

	// Use this for initialization
	public void QuitGame (){
		sound.Play ();
		Application.Quit ();
	}
}
