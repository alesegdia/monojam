using UnityEngine;
using System.Collections;

public class ShooterTrap : MonoBehaviour {

	public bool up = false;
	public bool down = false;
	public bool left = false;
	public bool right = false;
	public float rate = 2f;
	public float shootSpeed = 30f;
	public float strikeNum = 6;
	float currentStrike = 0;
	public float strikeWait = 4f;

	float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			shoot ();
			timer += rate;
			currentStrike++;
		}

		if (currentStrike == strikeNum) {
			currentStrike = 0;
			timer = strikeWait;
		}
	}

	void shoot()
	{
		shoot (1, 0);
		shoot (-1, 0);
		shoot (0, -1);
		shoot (0, 1);
	}

	void shoot(int x, int y)
	{
		GameObject go = (GameObject)GameObject.Instantiate (Resources.Load ("shot"), this.transform.position, Quaternion.identity);
		Rigidbody2D rb = go.GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (x * shootSpeed, y * shootSpeed);
	}
}
