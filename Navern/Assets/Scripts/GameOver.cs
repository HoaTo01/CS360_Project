using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    // Elements
    public string mainMenuScene;
    public string loadGameScene;

    // Start is called before the first frame update
    void Start() {
        AudioManager.selfReference.PlayMusic(4);

        PlayerControl.selfReference.gameObject.SetActive(false);
        //GameplayMenu.selfReference.gameObject.SetActive(false);
        BattleManager.selfReference.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    // Return to main menu.
    public void ReturnToMainMenu() {
        Destroy(GameManager.selfReference.gameObject);
        Destroy(PlayerControl.selfReference.gameObject);
        Destroy(GameplayMenu.selfReference.gameObject);
        Destroy(AudioManager.selfReference.gameObject);
        Destroy(BattleManager.selfReference.gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }

    // Load the most recent save.
    public void LoadMostRecentSave() {
        Destroy(GameManager.selfReference.gameObject);
        Destroy(PlayerControl.selfReference.gameObject);
        Destroy(GameplayMenu.selfReference.gameObject);
        Destroy(BattleManager.selfReference.gameObject);

        SceneManager.LoadScene(loadGameScene);
    }

    // Play the SFX for buttons.
    public void PlaySFXButtons() {
        AudioManager.selfReference.PlaySFX(4);
    }
}
