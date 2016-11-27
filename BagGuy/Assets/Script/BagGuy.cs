using UnityEngine;
using System.Collections;

public class BagGuy : MonoBehaviour {

	public float speed = 10f;
	public float jumpStrength = 10f;
	public float maxVerticalSpeed = 200f;
	public float runMultiplier = 1.5f;
	public float jumpMultiplier = 1.5f;
	bool onFloor = false;
	bool crouched = false;
	public bool touchingLadder = false;
	public bool climbingLadder = false;
	float gravityScale = 0;
	public bool dead = false;

	Transform groundChecker;
	GameObject topCollision;
	Animator animator;

	public LayerMask layerMask;

	Rigidbody2D rb;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		onFloor = false;
		touchingLadder = false;
		climbingLadder = false;
		groundChecker = transform.Find ("GroundChecker");
		transform.Find ("TopCollision").GetComponent<CollisionChecker> ().setGuy (this);
		topCollision = transform.Find ("TopCollision").gameObject;
		gravityScale = 20;
	}

	void OnTriggerEnter2D( Collider2D collision )
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("ladder")) {
			touchingLadder = true;
		}
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("spike")) {
			dead = true;
		}
	}

	void OnTriggerStay2D( Collider2D collision )
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("ladder")) {
			touchingLadder = true;
		}
	}

	void OnTriggerExit2D( Collider2D collision )
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("ladder")) {
			touchingLadder = false;
			//climbingLadder = false;
			//rb.gravityScale = gravityScale;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 v = rb.velocity;

		Collider2D hit = Physics2D.OverlapCircle( new Vector2(groundChecker.position.x, groundChecker.position.y), 0.15f, layerMask);
		onFloor = hit != null;

		bool running = Input.GetKey (KeyCode.LeftShift);

		if (!crouched) {
			if (Input.GetKey (KeyCode.A)) {
				v.x = -speed * (running ? runMultiplier : 1);
				sr.flipX = true;
			} else if (Input.GetKey (KeyCode.D)) {
				v.x = speed * (running ? runMultiplier : 1);
				sr.flipX = false;
			} else {
				v.x = 0;
			}
		} else {
			v.x = 0;
		}
			
		if (onFloor && Input.GetKey (KeyCode.S)) {
			crouched = true;
		} else {
			crouched = false;
		}


		if (Input.GetKeyDown (KeyCode.W) && onFloor) {
			v.y = jumpStrength * ((running && Mathf.Abs(v.x) > 0) ? jumpMultiplier : 1);
		}

		if (Mathf.Abs (v.y) > maxVerticalSpeed) {
			v.y = maxVerticalSpeed * Mathf.Sign (v.y);
		}

		if (touchingLadder) {
			if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) && !climbingLadder) {
				climbingLadder = true;
				gravityScale = rb.gravityScale;
				rb.gravityScale = 0;
			}
		} else {
			climbingLadder = false;
		}

		if (!climbingLadder) {
			rb.gravityScale = gravityScale;
		} else {
			rb.gravityScale = 0;
		}

		if (touchingLadder && climbingLadder) {
			if (Input.GetKey (KeyCode.W)) {
				v.y = 20;
			} else if (Input.GetKey (KeyCode.S)) {
				v.y = -20;
			} else {
				v.y = 0;
			}
		} else {
			//rb.gravityScale = gravityScale;
		}

		if (crouched) {
			topCollision.SetActive (false);
		} else {
			topCollision.SetActive (true);
		}

		animator.SetFloat ("HorizontalSpeed", Mathf.Abs(v.x));
		animator.SetFloat ("VerticalSpeed", Mathf.Abs(v.y));
		animator.SetBool ("Climbing", climbingLadder);
		animator.SetBool ("Crouched", crouched);
		animator.SetBool ("OnFloor", onFloor);
		animator.SetBool ("TouchingLadder", touchingLadder);

		rb.velocity = v;
	}
}
