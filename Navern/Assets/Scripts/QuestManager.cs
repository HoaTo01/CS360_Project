using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    // Elements
    public string[] questFlagNames;
    public bool[] questFlagComplete;

    public static QuestManager selfReference;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;

        questFlagComplete = new bool[questFlagNames.Length];
    }

    // Update is called once per frame
    void Update() {

    }

    // Check if quest is completed or not.
    public bool checkCompleted(string quest) {
        int questCode = GetQuestCode(quest);

        if (questCode != 0) {
            return questFlagComplete[questCode];
        }

        return false;
    }

    // Flag a quest completed.
    public void FlagQuestCompleted(string quest) {
        questFlagComplete[GetQuestCode(quest)] = true;

        UpdateQuestObjects();
    }

    // Flag a quest incompleted.
    public void FlagQuestNotCompleted(string quest) {
        questFlagComplete[GetQuestCode(quest)] = false;

        UpdateQuestObjects();
    }

    //Get a quest's code.
    public int GetQuestCode(string quest) {
        for (int i = 0; i < questFlagNames.Length; i++) {
            if (questFlagNames[i] == quest) {
                return i;
            }
        }

        Debug.LogError("Quest " + quest + " does not exist.");

        return 0;
    }

    // Activate/Deactivate quest objects.
    public void UpdateQuestObjects() {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if (questObjects.Length > 0) {
            for (int i = 0; i < questObjects.Length; i++) {
                questObjects[i].CheckCompleted();
            }
        }
    }

    // Save quest's data.
    public void SaveQuestData() {
        for (int i = 0; i < questFlagNames.Length; i++) {
            if (questFlagComplete[i]) {
                PlayerPrefs.SetInt("QuestFlagger_" + questFlagNames[i], 1);
            }

            else {
                PlayerPrefs.SetInt("QuestFlagger_" + questFlagNames[i], 0);
            }
        }
    }

    // Load quest's data
    public void LoadQuestData() {
        for (int i = 0; i < questFlagNames.Length; i++) {
            int value = 0;

            if (PlayerPrefs.HasKey("QuestFlagger_" + questFlagNames[i])) {
                value = PlayerPrefs.GetInt("QuestFlagger_" + questFlagNames[i]);
            }

            if (value == 1) {
                questFlagComplete[i] = true;
            }

            else {
                questFlagComplete[i] = false;
            }
        }
    }
}
