using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour {
    //Elements
    private bool inArea;

    public BattleSceneProperties[] potentialBattles;

    public bool activateOnEnter, activateOnTalk, activateOnStay, activateOnExit;

    public float timeBetweenBattles = 3f;
    private float timeBetweenBattlesCounter;

    public bool deactivateAfterStart;

    // Start is called before the first frame update
    void Start() {
        timeBetweenBattlesCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
    }

    // Update is called once per frame
    void Update() {

    }

    // LateUpdate is called once per frame after Update is called.
    private void LateUpdate() {
        if (inArea && PlayerControl.selfReference.canMove && !activateOnTalk) {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
                timeBetweenBattlesCounter -= Time.deltaTime;
            }

            if (timeBetweenBattlesCounter <= 0) {
                timeBetweenBattlesCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);

                StartCoroutine(StartBattleCo());
            }
        }

        if (inArea && activateOnTalk && Input.GetButtonDown("Yes Button")) {
            if (!DialogManager.selfReference.dialogBox.activeInHierarchy) {
                StartCoroutine(StartBattleCo());
            }
        }
    }

    // Start the battle (coroutine function).
    public IEnumerator StartBattleCo() {
        FadeTransition.selfReference.fadeToBlack();
        GameManager.selfReference.battleActive = true;

        // Select a random battle.
        int selectedBattle = Random.Range(0, potentialBattles.Length);

        // Set the reward items for the battle.
        BattleManager.selfReference.rewardItems = potentialBattles[selectedBattle].rewardItems;

        yield return new WaitForSeconds(1f);

        BattleManager.selfReference.StartBattle(potentialBattles[selectedBattle].enemies);

        // Get a random enemy to calculate the true exp gained for each character.
        int randomEnemy = Random.Range(0, potentialBattles[selectedBattle].enemies.Length);
        BattleCharacter randomEnemyReference = null;

        for (int i = 0; i < BattleManager.selfReference.activeBattleCharacters.Count; i++) {
            if (BattleManager.selfReference.activeBattleCharacters[i].characterName == potentialBattles[selectedBattle].enemies[randomEnemy]) {
                randomEnemyReference = BattleManager.selfReference.activeBattleCharacters[i];

                i = BattleManager.selfReference.activeBattleCharacters.Count;
            }
        }

        // Set the true exp gained for each character of the battle.
        for (int i = 0; i < BattleManager.selfReference.expGained.Length; i++) {
            if (PartyManager.selfReference.membersStats[i].gameObject.activeInHierarchy) {
                BattleManager.selfReference.expGained[i] = (potentialBattles[selectedBattle].expGained * randomEnemyReference.characterLevel) / PartyManager.selfReference.membersStats[i].characterLevel;
            }
        }

        FadeTransition.selfReference.fadeFromBlack();

        if (deactivateAfterStart) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (activateOnEnter) {
                StartCoroutine(StartBattleCo());
            }

            else {
                inArea = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (activateOnExit) {
                StartCoroutine(StartBattleCo());
            }

            else {
                inArea = false;
            }
        }
    }
}
