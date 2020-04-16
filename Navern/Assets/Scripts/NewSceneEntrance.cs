using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneEntrance : MonoBehaviour {
    // Elements
    public string loadArea;
    public string transitEntranceName;
    public NewSceneTile newSceneTile;

    private bool isLoadAfterFade;
    public float waitingTimeToLoad = 0.1f;

    // Start is called before the first frame update
    void Start() {
        newSceneTile.transitEntranceName = transitEntranceName;
    }

    // Update is called once per frame
    void Update() {
        // Load to new area
        if (isLoadAfterFade) {
            waitingTimeToLoad -= Time.deltaTime;

            if (waitingTimeToLoad <= 0) {
                isLoadAfterFade = false;
                SceneManager.LoadScene(loadArea);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Fade into new area
        if (collision.tag == "Player") {
            isLoadAfterFade = true;

            // Let the Game Manager knows that it is in transition.
            GameManager.selfReference.isInTransition = true;

            FadeTransition.selfReference.fadeToBlack();

            PlayerControl.selfReference.transitEntranceName = transitEntranceName;
        }
    }
}
