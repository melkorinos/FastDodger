using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ButtonClearHighscores : MonoBehaviour {

	public void ClearHighscores (){
		PlayerPrefs.DeleteAll ();

	}
}
