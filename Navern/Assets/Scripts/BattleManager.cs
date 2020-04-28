using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    // Elements
    public static BattleManager selfReference;

    private bool battleActive;

    public GameObject battleScene;

    public Transform[] charactersPositions;
    public Transform[] enemiesPositions;

    public BattleCharacter[] mainCharactersPrefabs;
    public BattleCharacter[] monstersPrefabs;

    public List<BattleCharacter> activeBattleCharacters = new List<BattleCharacter>();

    // Start is called before the first frame update
    void Start() {
        selfReference = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            StartBattle(new string[] { "Corrupted Wizard" });
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

            // Load the enemies.
            for (int i = 0; i < enemies.Length; i++) {
                if (enemies[i] != "") {
                    for (int j = 0; j < monstersPrefabs.Length; i++) {
                        if (monstersPrefabs[j].characterName == enemies[i]) {
                            BattleCharacter enemy = Instantiate(monstersPrefabs[j], enemiesPositions[i].position, enemiesPositions[i].rotation);
                            enemy.transform.parent = enemiesPositions[i];
                            activeBattleCharacters.Add(enemy);
                        }
                    }
                }
            }
        }
    }
}
