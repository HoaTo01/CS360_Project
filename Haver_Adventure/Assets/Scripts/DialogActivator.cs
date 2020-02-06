using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour {
    // Elements
    public string[] dialogLines;
    private bool canActivate = false;

    public bool isPerson = true;

    // Start is called before the first frame update
    void Start() {
 
    }

    // Update is called once per frame
    void Update() {
        if (canActivate && Input.GetButtonDown("Yes Button") && !DialogManager.selfReference.dialogBox.activeInHierarchy) {
            DialogManager.selfReference.ShowDialog(dialogLines, isPerson);
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
