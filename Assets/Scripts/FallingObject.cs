using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {

	public Vector2 speedMinMax;
	float speed;
	float minHeight;

	// Use this for initialization
	void Start () {
		speed = Mathf.Lerp (speedMinMax.x, speedMinMax.y, Difficulty.getDifPercent ());

		if (transform.localScale.x > 0.9)
			speed *= 0.75f;

		minHeight = -Camera.main.orthographicSize - transform.localScale.y;
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector2.down * speed * Time.deltaTime);

		if (transform.position.y < minHeight) {
			Destroy (gameObject);
		}
	}
}
