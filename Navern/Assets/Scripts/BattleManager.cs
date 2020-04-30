using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
    // Elements
    public static BattleManager selfReference;

    private bool battleActive;
    private bool playerIsAttacking;

    public GameObject battleScene;

    public Transform[] charactersPositions;
    public Transform[] enemiesPositions;

    public BattleCharacter[] mainCharactersPrefabs;
    public BattleCharacter[] monstersPrefabs;

    public List<BattleCharacter> activeBattleCharacters = new List<BattleCharacter>();

    public int currentTurn;
    public bool waitingTurnEnd;

    public GameObject optionsButtonsHolder;

    public BattleSkills[] skillsList;

    public GameObject enemyAttackEffect;

    public Text[] charactersNames, charactersHP, charactersMP;

    public GameObject targetWindow;
    public BattleTargetButton[] targetButtons;

    public GameObject magicWindow;
    public MagicSelect[] magicButtons;

    public BattleNotifications battleNotifications;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        // Testing
        if (Input.GetKeyDown(KeyCode.C)) {
            StartBattle(new string[] { "Corrupted Wizard", "Dead Warrior" });
        }

        // Turn processing.
        if (battleActive) {
            if (waitingTurnEnd) {
                if (activeBattleCharacters[currentTurn].isPlayer && !playerIsAttacking) {
                    optionsButtonsHolder.SetActive(true);
                }

                else if (!activeBattleCharacters[currentTurn].isPlayer) {
                    optionsButtonsHolder.SetActive(false);

                    // Enemies attack
                    StartCoroutine(EnemyMove());
                }

                else {
                    optionsButtonsHolder.SetActive(false);
                }
            }
        }

        // Close all the windows if press "No Button'
        if (Input.GetButtonDown("No Button")) {
            CloseAllWindows();
        }
    }

    // Start the battle.
    public void StartBattle(string[] enemies) {
        if (!battleActive) {
            battleActive = true;
            GameManager.selfReference.battleActive = true;

            // Set the camera to the battle scene.
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            // Load up the battle scene.
            battleScene.SetActive(true);

            // Play the background music.
            AudioManager.selfReference.PlayMusic(0);

            // Load the characters into the battle scene.
            for (int i = 0; i < charactersPositions.Length; i++) {
                if (PartyManager.selfReference.membersStats[i].gameObject.activeInHierarchy) {
                    for (int j = 0; j < mainCharactersPrefabs.Length; j++) {
                        if (mainCharactersPrefabs[j].characterName == PartyManager.selfReference.membersStats[i].characterName) {
                            BattleCharacter character = Instantiate(mainCharactersPrefabs[j], charactersPositions[i].position, charactersPositions[i].rotation);
                            character.transform.parent = charactersPositions[i];
                            activeBattleCharacters.Add(character);

                            // Get the correct stats.
                            CharacterStats characterStats = PartyManager.selfReference.membersStats[i];
                            activeBattleCharacters[i].currentHP = characterStats.currentHP;
                            activeBattleCharacters[i].maxHP = characterStats.maxHP;
                            activeBattleCharacters[i].currentMP = characterStats.currentMP;
                            activeBattleCharacters[i].maxMP = characterStats.maxMP;
                            activeBattleCharacters[i].agility = characterStats.agility;
                            activeBattleCharacters[i].diceCoefficient = characterStats.diceCoefficient;
                            activeBattleCharacters[i].numberOfDiceFaces = characterStats.numberOfDiceFaces;
                        }
                    }
                }
            }

            // Sort the battle characters in a way that the one with better agility goes first.
            activeBattleCharacters = activeBattleCharacters.OrderByDescending(o => o.agility).ToList();

            // Load the enemies.
            for (int i = 0; i < enemies.Length; i++) {
                if (enemies[i] != "") {
                    for (int j = 0; j < monstersPrefabs.Length; j++) {
                        if (monstersPrefabs[j].characterName == enemies[i]) {
                            BattleCharacter enemy = Instantiate(monstersPrefabs[j], enemiesPositions[i].position, enemiesPositions[i].rotation);
                            enemy.transform.parent = enemiesPositions[i];
                            activeBattleCharacters.Add(enemy);
                        }
                    }
                }
            }

            // Set the current turn to 0 and start waiting for turn to end.
            waitingTurnEnd = true;
            currentTurn = 0;

            UpdateStats();
        }
    }

    // Move on to the next turn.
    public void NextTurn() {
        currentTurn++;

        if (currentTurn >= activeBattleCharacters.Count) {
            currentTurn = 0;
        }

        waitingTurnEnd = true;

        // Update the battle info.
        UpdateBattle();

        // Update stats info.
        UpdateStats();
    }

    // Update the battle info.
    public void UpdateBattle() {
        bool allEnemiesAreDead = true;
        bool allCharactersAreDead = true;

        for (int i = 0; i < activeBattleCharacters.Count; i++) {
            if (activeBattleCharacters[i].currentHP < 0) {
                activeBattleCharacters[i].currentHP = 0;
            }

            if (activeBattleCharacters[i].currentHP > activeBattleCharacters[i].maxHP) {
                activeBattleCharacters[i].currentHP = activeBattleCharacters[i].maxHP;
            }

            if (activeBattleCharacters[i].currentHP == 0) {
                // Handle the dead characters.
            }

            else {
                if (activeBattleCharacters[i].isPlayer) {
                    allCharactersAreDead = false;
                }

                else {
                    allEnemiesAreDead = false;
                }
            }
        }

        if (allCharactersAreDead || allEnemiesAreDead) {
            if (allCharactersAreDead) {
                // End the battle with the player's loss
            }

            else {
                // End the battle with the playeer's victory.
            }

            battleScene.SetActive(false);
            battleActive = false;
            GameManager.selfReference.battleActive = false;
        }

        else {
            // Skip the dead players.
            while (activeBattleCharacters[currentTurn].currentHP == 0) {
                currentTurn++;

                if (currentTurn >= activeBattleCharacters.Count) {
                    currentTurn = 0;
                }
            }
        }
    }

    // Execute an enemy's move and go to the next turn.
    public IEnumerator EnemyMove() {
        waitingTurnEnd = false;

        yield return EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    // Let the enemy attack.
    private IEnumerator EnemyAttack() {
        List<int> characters = new List<int>();

        for (int i = 0; i < activeBattleCharacters.Count; i++) {
            if (activeBattleCharacters[i].isPlayer && activeBattleCharacters[i].currentHP > 0) {
                characters.Add(i);
            }
        }

        //Target to attack of the enemy
        int target = characters[Random.Range(0, characters.Count)];
        BattleSkills skillUsed = null;

        // Select a skill to use.
        int selectedAttack = Random.Range(0, activeBattleCharacters[currentTurn].skillsAvailable.Length);

        for (int i = 0; i < skillsList.Length; i++) {
            if (skillsList[i].skillName == activeBattleCharacters[currentTurn].skillsAvailable[selectedAttack]) {
                skillUsed = skillsList[i];
            }
        }

        // Roll the dice to get the dice values.
        Dice.selfReference.playerNumberOfDiceFaces = activeBattleCharacters[target].numberOfDiceFaces;
        Dice.selfReference.enemyNumberOfDiceFaces = activeBattleCharacters[currentTurn].numberOfDiceFaces;
        yield return StartCoroutine(Dice.selfReference.RollTheDice());

        yield return new WaitForSeconds(0.5f);

        // Play the enemy's attack effect.
        Instantiate(enemyAttackEffect, activeBattleCharacters[currentTurn].transform.position, activeBattleCharacters[currentTurn].transform.rotation);

        //Play the skill's animation.
        Instantiate(skillUsed.visualEffect, activeBattleCharacters[target].transform.position, activeBattleCharacters[target].transform.rotation);

        // Inflict damage/heal a target
        int damageInflicted = inflictDamage(target, skillUsed.damagePower, skillUsed.isAHealMove, Dice.selfReference.enemyValue, Dice.selfReference.playerValue);
        activeBattleCharacters[target].currentHP -= damageInflicted;
        DamagePopup.Create(activeBattleCharacters[target].transform.position, damageInflicted, false);

        // Update stats to the screen.
        UpdateStats();
    }

    // Inflict damage to a target (Heal a target if damage is negative).
    public int inflictDamage(int target, double skillDamagePower, bool isAHealMove, int userDiceValue, int targetDiceValue) {
        // Get the user's dice coefficient.
        double userDiceCoefficient = activeBattleCharacters[currentTurn].diceCoefficient;

        // Get the target's dice coefficient.
        double targetDiceCoefficient = activeBattleCharacters[target].diceCoefficient;

        double damageCalculation = skillDamagePower * userDiceValue * userDiceCoefficient - targetDiceValue * targetDiceCoefficient;
        int damageInflicted = Mathf.FloorToInt((float)damageCalculation);

        if (damageInflicted <= 0 && isAHealMove == false) {
            damageInflicted = 1;
        }

        return damageInflicted;
    }

    // Update the stats of battle characters on the screen.
    public void UpdateStats() {
        for (int i = 0; i < charactersNames.Length; i++) {
            if (activeBattleCharacters.Count > i) {
                if (activeBattleCharacters[i].isPlayer) {
                    BattleCharacter characterStats = activeBattleCharacters[i];

                    charactersNames[i].gameObject.SetActive(true);
                    charactersNames[i].text = characterStats.characterName;
                    charactersHP[i].text = Mathf.Clamp(characterStats.currentHP, 0, int.MaxValue) + "/" + characterStats.maxHP;
                    charactersMP[i].text = Mathf.Clamp(characterStats.currentMP, 0, int.MaxValue) + "/" + characterStats.maxMP;
                }

                else {
                    charactersNames[i].gameObject.SetActive(false);
                }
            }

            else {
                charactersNames[i].gameObject.SetActive(false);
            }
        }
    }

    // Let a player attack (coroutine function).
    private IEnumerator PlayerAttackCo(string skillName, int target) {
        // Close the target window
        targetWindow.SetActive(false);

        // Let the game know the player is attacking.
        playerIsAttacking = true;

        BattleSkills skillUsed = null;

        for (int i = 0; i < skillsList.Length; i++) {
            if (skillsList[i].skillName == skillName) {
                skillUsed = skillsList[i];
            }
        }

        // Roll the dice to get the dice values.
        Dice.selfReference.playerNumberOfDiceFaces = activeBattleCharacters[target].numberOfDiceFaces;
        Dice.selfReference.enemyNumberOfDiceFaces = activeBattleCharacters[currentTurn].numberOfDiceFaces;
        yield return StartCoroutine(Dice.selfReference.RollTheDice());

        yield return new WaitForSeconds(0.5f);

        // Play the attack animation of the battle character.
        activeBattleCharacters[currentTurn].GetComponent<Animator>().SetTrigger("Attack");

        //Play the skill's animation.
        Instantiate(skillUsed.visualEffect, activeBattleCharacters[target].transform.position, activeBattleCharacters[target].transform.rotation);

        // Inflict damage/heal a target
        int damageInflicted = inflictDamage(target, skillUsed.damagePower, skillUsed.isAHealMove, Dice.selfReference.playerValue, Dice.selfReference.enemyValue);
        activeBattleCharacters[target].currentHP -= damageInflicted;
        DamagePopup.Create(activeBattleCharacters[target].transform.position, damageInflicted, false);

        // Update stats to the screen.
        UpdateStats();

        yield return new WaitForSeconds(0.5f);

        // Let the game know the player is not attacking.
        playerIsAttacking = false;

        NextTurn();
    }

    // Let a player attack (using previous coroutine function).
    public void PlayerAttack(string skillName, int target) {
        StartCoroutine(PlayerAttackCo(skillName, target));
    }

    // Open the target window.
    public void OpenTargetWindow(string skillName) {
        targetWindow.SetActive(true);

        List<int> enemies = new List<int>();

        for (int i = 0; i < activeBattleCharacters.Count; i++) {
            if (!activeBattleCharacters[i].isPlayer) {
                enemies.Add(i);
            }
        }

        for (int i = 0; i < targetButtons.Length; i++) {
            if (enemies.Count > i) {
                targetButtons[i].gameObject.SetActive(true);

                targetButtons[i].skillName = skillName;
                targetButtons[i].target = enemies[i];
                targetButtons[i].targetName.text = activeBattleCharacters[enemies[i]].characterName;
            }

            else {
                targetButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // Open the magic window.
    public void OpenMagicWindow() {
        magicWindow.SetActive(true);

        for (int i = 0; i < magicButtons.Length; i++) {
            if (activeBattleCharacters[currentTurn].skillsAvailable.Length > i) {
                magicButtons[i].gameObject.SetActive(true);

                magicButtons[i].skillName = activeBattleCharacters[currentTurn].skillsAvailable[i];
                magicButtons[i].skillNameText.text = magicButtons[i].skillName;

                for (int j = 0; j < skillsList.Length; j++) {
                    if (skillsList[j].skillName == magicButtons[i].skillName) {
                        magicButtons[i].skillCost = skillsList[j].manaCost;
                        magicButtons[i].skillCostText.text = magicButtons[i].skillCost.ToString();
                    }
                }
            }

            else {
                magicButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // Run from a battle.
    public void Run() {
        battleActive = false;
        battleScene.SetActive(false);
    }

    // Close all battle windows.
    public void CloseAllWindows() {
        targetWindow.SetActive(false);
        magicWindow.SetActive(false);
        ItemsInBattle.selfReference.itemsWindow.SetActive(false);
    }
}
