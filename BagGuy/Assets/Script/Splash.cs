using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution (200, 200, false);
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Space) )
		{
			Application.LoadLevel (1);
		}
	}
}
