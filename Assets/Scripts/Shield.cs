using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public float speed;

	float minHeight;

	// Use this for initialization
	void Start () {
		speed = Mathf.Lerp (speed, speed + speed/2, Difficulty.getDifPercent ());
		minHeight = -Camera.main.orthographicSize - transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.down * speed * Time.deltaTime);

		if (transform.position.y < minHeight) {
			Destroy (gameObject);
		}
	}

}
