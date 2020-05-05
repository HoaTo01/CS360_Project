using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour {
    // Elements
    public static BattleReward selfReference;

    public Text expGainedText;
    public Text itemsAcquiredText;

    public GameObject rewardTextBox;

    public string[] rewardItems;
    public int[] expGained;

    public bool flagQuestCompleted;
    public string questToFlag;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;
    }

    // Update is called once per frame
    void Update() {
        if ((Input.GetButtonDown("No Button") || Input.GetButtonDown("Yes Button")) && rewardTextBox.activeInHierarchy) {
            CloseRewardTextBox();
        }
    }

    // Open the reward text box.
    public void OpenRewardTextBox(int[] expGained, string[] rewardItems) {
        this.expGained = expGained;
        this.rewardItems = rewardItems;

        expGainedText.text = "";
        itemsAcquiredText.text = "";

        // Set the text for the exp gained.
        for (int i = 0; i < this.expGained.Length; i++) {
            if (PartyManager.selfReference.membersStats[i].gameObject.activeInHierarchy) {
                expGainedText.text += PartyManager.selfReference.membersStats[i].characterName + " gains " + this.expGained[i] + " EXP";
            }

            if (i < this.expGained.Length - 1) {
                expGainedText.text += "\n";
            }
        }

        // Set the text for items acquired.
        for (int i = 0; i < this.rewardItems.Length; i++) {
            itemsAcquiredText.text += rewardItems[i];

            if (i < this.expGained.Length - 1) {
                itemsAcquiredText.text += "\n";
            }
        }

        rewardTextBox.SetActive(true);
    }

    // Close the reward text box.
    public void CloseRewardTextBox() {
        // Add the gained exp to each member.
        for (int i = 0; i < PartyManager.selfReference.membersStats.Length; i++) {
            if (PartyManager.selfReference.membersStats[i].gameObject.activeInHierarchy) {
                PartyManager.selfReference.membersStats[i].AddEXP(expGained[i]);
            }
        }

        // Add the item to the inventory.
        for (int i = 0; i < rewardItems.Length; i++) {
            GameManager.selfReference.AddItem(rewardItems[i]);
        }

        rewardTextBox.SetActive(false);
        GameManager.selfReference.battleActive = false;

        if(flagQuestCompleted) {
            QuestManager.selfReference.FlagQuestCompleted(questToFlag);
        }
    }
}
