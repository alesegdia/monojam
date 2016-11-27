using UnityEngine;
using System.Collections;

public class CollisionChecker : MonoBehaviour {

	BagGuy bg;

	public void setGuy( BagGuy bg )
	{
		this.bg = bg;
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer ("spike")) {
			bg.dead = true;
		}
	}

	void OnTriggerEnter2D( Collider2D collider )
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer ("spike")) {
			bg.dead = true;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
