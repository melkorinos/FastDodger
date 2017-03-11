using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public event System.Action onPlayerDeath;

	public AudioSource shieldSound;
	public AudioSource pickupSound;
	public AudioSource teleport;

	public GameObject shieldSprite;

	public float speed = 7;
	public float maxSpeed = 30;

	float halfScreenSize ;

	public Rigidbody2D rb;

	public float clickDelta = 0.25f;
	public float teleportCooldown = 1f;
	public float teleportDistanceFactor = 3f;
	public float invTime = 1;
	public float smallTime =10;

	float previousClickTimeLeft,previousClickTimeRight, timeSinceLastClickLeft, timeSinceLastClickRight, timeSinceLastTeleport = 0;
	bool isShielded = false;

	Animator anim;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		clickDelta = PlayerPrefs.GetFloat ("Double tap MS", 0.25f);
		//get game space sizes
		float halfPlayerSize = transform.localScale.x / 2f;
		halfScreenSize = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerSize;
		anim = shieldSprite.GetComponent <Animator> ();
	}

	// Update is called once per frame
	void Update () {

		//Screen Wrap
		if (transform.position.x < -halfScreenSize) {
			transform.position = new Vector2 (halfScreenSize, transform.position.y);
		}

		if (transform.position.x > halfScreenSize) {
			transform.position = new Vector2 (-halfScreenSize, transform.position.y);
		}

		bool touchedLeft = false;
		bool touchedRight = false;
		// check if touch left or right
		if (Input.touchCount >0){
			if ((Input.GetTouch(0).phase == TouchPhase.Began)) {
				if (Input.GetTouch(0).position.x <= Screen.width / 2) {
					touchedLeft = true;
				} 
				else if (Input.GetTouch(0).position.x > Screen.width / 2) {
					touchedRight = true;
				}
			}
		}
			
		//Double Click Left/tap left
		if ((Input.GetKeyUp("left")) || ( touchedLeft )){
			timeSinceLastClickLeft = Time.time - previousClickTimeLeft;
			if ((timeSinceLastClickLeft < clickDelta && Time.time - timeSinceLastTeleport > teleportCooldown) || (touchedLeft)) {
				TeleportPlayer (-1);
				timeSinceLastTeleport = Time.time;
			}
			touchedLeft = false;
			previousClickTimeLeft = Time.time;

		}
		//Double Click Right/tap right
		if (Input.GetKeyUp("right")|| ( touchedRight )){
			timeSinceLastClickRight = Time.time - previousClickTimeRight;
			if ((timeSinceLastClickRight < clickDelta && Time.time - timeSinceLastTeleport > teleportCooldown) || (touchedRight)){
				TeleportPlayer(1);
				timeSinceLastTeleport = Time.time;
			}
			touchedRight = false;
			previousClickTimeRight = Time.time;
		}
	}

	void FixedUpdate (){
		//take the input 
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		float inputX = Input.GetAxis ("Horizontal");
		#elif UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		float inputX = Input.acceleration.x;
		inputX*=2;
		#endif

		//move the player
		rb.AddForce (inputX * speed * transform.right);
		if (rb.velocity.magnitude > maxSpeed){
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

	void OnTriggerEnter2D(Collider2D triggerCollider){
		//collide with enemy object
		if ((triggerCollider.tag == "Falling Object")) {
			if (isShielded) {
				GetComponent<PolygonCollider2D> ().enabled = false;
				anim.SetTrigger ("shield brake");
				shieldSound.Play ();
				StartCoroutine (ReactivateCollider (invTime));
			}
			if (!isShielded) {
				if (onPlayerDeath != null) {
					onPlayerDeath ();
				}
				Destroy (this.gameObject);
			}
		}

		//collide with shield
		if (triggerCollider.tag == "Shield") {
			pickupSound.Play ();
			isShielded = true;
			shieldSprite.SetActive (true);
			Destroy (triggerCollider.gameObject);
		}

		//collide with mushroom
		if (triggerCollider.tag == "Mushroom") {
			pickupSound.Play ();
			transform.localScale *= 0.5f;
			StartCoroutine (ReturnToNormalSize (smallTime));
			Destroy (triggerCollider.gameObject);
		}
	}

	//teleport player by X units
	void TeleportPlayer ( int dir){
		if (teleport!= null) teleport.Play ();
			float newXPos = transform.position.x + (dir * transform.localScale.x * teleportDistanceFactor);
			transform.position = new Vector2 (newXPos, transform.position.y);
			rb.velocity = rb.velocity.normalized;

	}

	//reactive collider after shield
	IEnumerator ReactivateCollider (float invTime){
		yield return new WaitForSeconds (invTime);
		GetComponent<PolygonCollider2D> ().enabled = true;
		shieldSprite.SetActive (false);
		isShielded = false;
	}

	//reactive collider after shield
	IEnumerator ReturnToNormalSize (float smallTime){
		yield return new WaitForSeconds (smallTime);
		transform.localScale *= 2;
	}

}