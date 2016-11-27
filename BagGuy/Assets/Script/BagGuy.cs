using UnityEngine;
using System.Collections;

public class BagGuy : MonoBehaviour {

	public float speed = 10f;
	public float jumpStrength = 10f;
	public float maxVerticalSpeed = 200f;
	public float runMultiplier = 1.5f;
	public float jumpMultiplier = 1.5f;
	public bool onFloor = false;
	public bool crouched = false;

	Transform groundChecker;
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
		groundChecker = transform.Find ("GroundChecker");
	}

	// Update is called once per frame
	void Update () {
		Vector2 v = rb.velocity;

		Debug.Log (groundChecker);
		Collider2D hit = Physics2D.OverlapCircle( new Vector2(groundChecker.position.x, groundChecker.position.y), 0.15f, layerMask);
		onFloor = hit != null;
		animator.SetBool ("OnFloor", onFloor);

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

		animator.SetFloat ("HorizontalSpeed", Mathf.Abs(v.x));
		animator.SetBool ("Crouched", crouched);

		if (Input.GetKeyDown (KeyCode.W) && onFloor) {
			v.y = jumpStrength * ((running && Mathf.Abs(v.x) > 0) ? jumpMultiplier : 1);
		}

		if (Mathf.Abs (v.y) > maxVerticalSpeed) {
			v.y = maxVerticalSpeed * Mathf.Sign (v.y);
		}
		rb.velocity = v;
	}
}
