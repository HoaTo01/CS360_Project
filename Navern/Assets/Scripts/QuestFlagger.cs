using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFlagger : MonoBehaviour {
    // Elements
    public string questToFlag;
    public bool flagCompleted;

    public bool flagOnEnter;

    private bool canFlag;

    public bool deactivateOnFlag;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Flag quest after pressing Yes.
        if (canFlag && Input.GetButtonDown("Yes Button")) {
            canFlag = false;
            FlagQuest();
        }
    }

    // Flag a quest.
    public void FlagQuest() {
        if(flagCompleted) {
            QuestManager.selfReference.FlagQuestCompleted(questToFlag);
        }

        else {
            QuestManager.selfReference.FlagQuestNotCompleted(questToFlag);
        }

        gameObject.SetActive(!deactivateOnFlag);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            // Flag quest on enter if flagOnEnter is true.
            if (flagOnEnter) {
                FlagQuest();
            }

            else {
                canFlag = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            canFlag = false;
        }
    }
}
