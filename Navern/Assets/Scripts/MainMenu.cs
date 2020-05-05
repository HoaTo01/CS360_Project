using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // Elements
    public string newGameScene;
    public string loadGameScene;

    public GameObject loadGameButton;

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("Current_Scene")) {
            loadGameButton.SetActive(true);
        }

        else {
            loadGameButton.SetActive(false);
        }

        // Play the background music.
        AudioManager.selfReference.PlayMusic(0);
    }

    // Update is called once per frame
    void Update() {

    }

    // Load a new game.
    public void NewGame() {
        SceneManager.LoadScene(newGameScene);
    }

    // Load the saved game.
    public void LoadGame() {
        SceneManager.LoadScene(loadGameScene);
    }

    // Exit game.
    public void ExitGame() {
        Application.Quit();
    }

    // Play the SFX for buttons.
    public void PlaySFXButtons() {
        AudioManager.selfReference.PlaySFX(4);
    }
}
