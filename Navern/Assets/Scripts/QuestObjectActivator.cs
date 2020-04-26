using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour {
    // Elements
    public GameObject objectToActivate;
    public string questToCheck;

    public bool activateIfCompleted;
    private bool firstCheck;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Check if the item has been checked once or not.
        if (!firstCheck) {
            firstCheck = true;

            CheckCompleted();
        }
    }

    // Check if the quest is completed and set the quest object active/inactive
    public void CheckCompleted() {
        if (QuestManager.selfReference.checkCompleted(questToCheck)) {
            objectToActivate.SetActive(activateIfCompleted);
        }
    }
}
