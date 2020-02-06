using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneTile : MonoBehaviour {
    // Elements
    public string transitEntranceName;

    // Start is called before the first frame update
    void Start() {
        // Start at a specific position when enter a new scene
        if (PlayerControl.selfReference) {
            if (transitEntranceName == PlayerControl.selfReference.transitEntranceName) {
                PlayerControl.selfReference.transform.position = transform.position;
            }
        }

        // Fade from black to new area
        if(FadeTransition.selfReference) {
            FadeTransition.selfReference.fadeFromBlack();
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
