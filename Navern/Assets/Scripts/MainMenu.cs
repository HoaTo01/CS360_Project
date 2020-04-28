using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // Elements
    public string newGameScene;

    public GameObject loadGameButton;

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("Current_Scene")) {
            loadGameButton.SetActive(true);
        }

        else {
            loadGameButton.SetActive(false);
        }
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
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));

        GameManager.selfReference.LoadData();
        QuestManager.selfReference.LoadQuestData();
    }

    // Exit game.
    public void ExitGame() {
        Application.Quit();
    }
}
