using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {

	private static AudioSource menuMusic;
	private static AudioSource gameMusic;

	private static bool didMusicPull = false;

	public static int Menu = 0;
	public static int Game = 1;


	void Awake () {
	
		if (!didMusicPull) {

			AudioSource[] audioSources = this.GetComponents<AudioSource> ();

			menuMusic = audioSources [0];
			gameMusic = audioSources [1];

			didMusicPull = true;
			DontDestroyOnLoad (this.gameObject);

		}
	
	}

	public void playMusic(int MusicType) {
	
		if (MusicType == 0) {
		
			if (gameMusic.isPlaying) {
			
				gameMusic.Stop();
			
			}


			if (!menuMusic.isPlaying) {
			
				menuMusic.Play ();
			
			}
		} else if (MusicType == 1) {
		
		
			if (!gameMusic.isPlaying) {

				gameMusic.Play();

			}


			if (menuMusic.isPlaying) {

				menuMusic.Stop ();

			}
		}
	
	
	
	
	
	}

}
