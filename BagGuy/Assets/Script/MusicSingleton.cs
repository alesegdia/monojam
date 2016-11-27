using UnityEngine;
using System.Collections;

public class MusicSingleton : MonoBehaviour {

	private static MusicSingleton instance = null;
	public AudioSource sfxWin;
	public AudioSource sfxLose;

	public int sfx = 0;

	public static MusicSingleton Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	void Update()
	{
		if (sfx == 1) {
			sfxWin.Play ();
			sfx = 0;
		} else if (sfx == 2) {
			sfxLose.Play ();
			sfx = 0;
		}
	}

	public void PlaySfx( int i )
	{
		sfx = i;
	}
}