using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {
    // Elements
    public GameObject player;
    public GameObject canvas;
    public GameObject partyManager;
    public GameObject gameManager;
    public GameObject audioManager;

    // Start is called before the first frame update
    void Start() {
        // Instantiate all essential objects
        if (PlayerControl.selfReference == null) {
            PlayerControl clone = Instantiate(player).GetComponent<PlayerControl>();
            PlayerControl.selfReference = clone;
        }

        if (FadeTransition.selfReference == null) {
            FadeTransition clone = Instantiate(canvas).GetComponent<FadeTransition>();
            FadeTransition.selfReference = clone;
        }

        if (PartyManager.selfReference == null) {
            PartyManager clone = Instantiate(partyManager).GetComponent<PartyManager>();
            PartyManager.selfReference = clone;
        }

        if (GameManager.selfReference == null) {
            GameManager clone = Instantiate(gameManager).GetComponent<GameManager>();
            GameManager.selfReference = clone;
        }

        if (AudioManager.selfReference == null) {
            AudioManager clone = Instantiate(audioManager).GetComponent<AudioManager>();
            AudioManager.selfReference = clone;
        }

        // For testing
        PlayerControl.selfReference.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, PlayerControl.selfReference.transform.position.z);
    }

    // Update is called once per frame
    void Update() {

    }
}
