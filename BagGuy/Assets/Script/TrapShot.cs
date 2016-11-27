using UnityEngine;
using System.Collections;

public class TrapShot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer ("solid") ) {
			Destroy (this.gameObject);
		}

		if (collider.gameObject.layer == LayerMask.NameToLayer ("player")) {
			GameObject.FindGameObjectWithTag ("bag_guy").GetComponent<BagGuy> ().dead = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
