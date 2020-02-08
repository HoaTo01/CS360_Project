using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {
    // Elements
    public GameObject player;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start() {
        // Instantiate all essential objects
        if (PlayerControl.selfReference== null) {
            PlayerControl clone = Instantiate(player).GetComponent<PlayerControl>();
            PlayerControl.selfReference = clone;
        }

        if(FadeTransition.selfReference== null) {
            FadeTransition clone = Instantiate(canvas).GetComponent<FadeTransition>();
            FadeTransition.selfReference = clone;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
