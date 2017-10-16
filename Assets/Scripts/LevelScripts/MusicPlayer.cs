using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	private AudioSource aud;
	private int currentAudioIndex = 0;
	private bool musicIsPlaying = true;

	public AudioClip[] backgroundMusicChoices;
	public KeyCode musicPauseKey;
	public KeyCode musicSwitchKey;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource> ();
//		shuffleAudioOrder ();
		aud.clip = backgroundMusicChoices [currentAudioIndex];
		aud.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (musicPauseKey)) {
			if (musicIsPlaying) {
				aud.Pause ();
				musicIsPlaying = false;
			} else {
				aud.UnPause ();
				musicIsPlaying = true;
			}
		} else if ((Input.GetKeyDown(musicSwitchKey) && musicIsPlaying) || (!aud.isPlaying && musicIsPlaying)) {
			PlayNextSong ();
		}
	}

	private void PlayNextSong() {
		aud.Stop ();
		currentAudioIndex += 1;
		if (currentAudioIndex >= backgroundMusicChoices.Length) {
			currentAudioIndex = 0;
		}
		aud.clip = backgroundMusicChoices [currentAudioIndex];
		aud.Play ();
	}

	private void shuffleAudioOrder() {
		for (int i = backgroundMusicChoices.Length - 1; i > 0; i--) {
			int r = Random.Range(0, i);
			AudioClip tmp = backgroundMusicChoices[i];
			backgroundMusicChoices[i] = backgroundMusicChoices[r];
			backgroundMusicChoices[r] = tmp;
		}
	}
}
