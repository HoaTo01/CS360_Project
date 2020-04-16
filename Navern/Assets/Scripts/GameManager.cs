using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Elements
    public static GameManager selfReference;

    public bool gameplayMenuIsOpened, dialogIsOpened, isInTransition;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        //Prevent characters from moving in certain situations.
        movingControl();

    }

    //Prevent characters from moving in certain situations.
    private void movingControl() {
        if (gameplayMenuIsOpened || dialogIsOpened || isInTransition) {
            PlayerControl.selfReference.canMove = false;
        }

        else {
            PlayerControl.selfReference.canMove = true;
        }
    }
}
