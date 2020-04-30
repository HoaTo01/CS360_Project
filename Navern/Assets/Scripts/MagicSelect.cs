using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicSelect : MonoBehaviour {
    // Elements
    public string skillName;
    public int skillCost;
    public Text skillNameText;
    public Text skillCostText;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Press a magic button.
    public void Press() {
        if (BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].currentMP >= skillCost) {
            BattleManager.selfReference.magicWindow.SetActive(false);
            BattleManager.selfReference.OpenTargetWindow(skillName);
            BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].currentMP -= skillCost;
        }

        else {
            // Not enough MP.
            BattleManager.selfReference.battleNotifications.notificationsText.text = BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].characterName + " does not have enough MP.";
            BattleManager.selfReference.battleNotifications.Activate();
            BattleManager.selfReference.magicWindow.SetActive(false);
        }
    }
}
