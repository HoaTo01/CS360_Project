using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    // Elements
    public AudioSource[] music;
    public AudioSource[] sfx;

    public static AudioManager selfReference;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }

    // play a song in music.
    public void PlayMusic(int musicCode) {
        // Stop all the music before playing a new one.
        if (!music[musicCode].isPlaying) {
            StopMusic();

            if (musicCode < music.Length) {
                music[musicCode].Play();
            }
        }
    }

    // Play an SFX.
    public void PlaySFX(int sfxCode) {
        if (sfxCode < sfx.Length) {
            sfx[sfxCode].Play();
        }
    }

    // Stop currently playing music.
    public void StopMusic() {
        for (int i = 0; i < music.Length; i++) {
            music[i].Stop();
        }
    }
}
