using UnityEngine;
using System.Collections;

public class BagGuy : MonoBehaviour {

	public float speed = 10f;
	public float jumpStrength = 10f;
	public float maxVerticalSpeed = 200f;
	public float runMultiplier = 1.5f;
	public float jumpMultiplier = 1.5f;
	public bool onFloor = false;

	public LayerMask layerMask;

	Rigidbody2D rb;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		onFloor = false;
	}

	void OnCollisionStay2D( Collision2D collision)
	{
		foreach (ContactPoint2D contact in collision.contacts) {
			if (contact.normal.y > 0.98f) {
				onFloor = true;
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 v = rb.velocity;

		RaycastHit2D hit = Physics2D.Raycast (this.transform.position, new Vector2 (0, -1), 4, layerMask);
		Debug.Log (hit.collider);

		bool running = Input.GetKey (KeyCode.LeftShift);

		if (Input.GetKey (KeyCode.A)) {
			v.x = -speed * (running ? runMultiplier : 1);
			sr.flipX = true;
		} else if (Input.GetKey (KeyCode.D)) {
			v.x = speed * (running ? runMultiplier : 1);
			sr.flipX = false;
		} else {
			v.x = 0;
		}

		Debug.Log (onFloor);

		if (Input.GetKeyDown (KeyCode.W) && onFloor) {
			v.y = jumpStrength * ((running && Mathf.Abs(v.x) > 0) ? jumpMultiplier : 1);
		}

		if (Mathf.Abs (v.y) > maxVerticalSpeed) {
			v.y = maxVerticalSpeed * Mathf.Sign (v.y);
		}
		rb.velocity = v;
	}
}
