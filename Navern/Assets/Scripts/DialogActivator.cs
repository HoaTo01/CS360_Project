﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour {
    // Elements
    public string[] dialogLines;
    private bool canActivate = false;

    public bool isPerson = true;

    public bool shouldActivateQuest;
    public string questToFlag;
    public bool flagCompleted;

    // Start is called before the first frame update
    void Start() {
 
    }

    // Update is called once per frame
    void Update() {
        if (canActivate && Input.GetButtonDown("Yes Button") && !DialogManager.selfReference.dialogBox.activeInHierarchy) {
            DialogManager.selfReference.ShowDialog(dialogLines, isPerson);
            DialogManager.selfReference.ShouldActivateQuest(questToFlag, flagCompleted);

            // Play the SFX.
            AudioManager.selfReference.PlaySFX(4);
        }
    }

    // Enable the player to activate the dialog box when near the NPC
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            canActivate = true;
        }
    }

    // Disable the player to activate the dialog box when go away from the NPC
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            canActivate = false;
        }
    }
}
