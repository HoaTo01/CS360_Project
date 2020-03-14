using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    // Elements
    public string characterName;
    public int characterLevel;
    public int maxCharacterLevel = 30;
    public int baseEXP = 1000;
    public int[] expToNextLevel;
    public string element;

    public int maxHP;
    public int maxMP;
    public int agility;
    public int diceLevel;
    public int maxDiceLevel = 10;
    public int numberOfDiceFaces;
    public float diceCoefficient;

    public int currentHP;
    public int currentMP;
    public int currentEXP;

    public string swordEquipment;
    public string braceletEquipment;
    public string ringEquipment;
    public string shoesEquipment;

    public Sprite characterSprite;

    // Start is called before the first frame update
    void Start() {
        // Initialize the exp of each level
        expToNextLevel = new int[maxCharacterLevel];
        expToNextLevel[0] = baseEXP;

        for (int i = 1; i <= 29; i++) {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.3f);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.K)) {
            AddEXP(10000);
        }
    }

    // Add exp
    public void AddEXP(int expToAdd) {
        currentEXP += expToAdd;

        if (currentEXP > expToNextLevel[characterLevel - 1]) {
            currentEXP -= expToNextLevel[characterLevel - 1];
            // Increase the character's level and stats
            characterLevel++;

            maxHP += Mathf.FloorToInt(maxHP * 0.2f);
            currentHP = maxHP;
            maxMP += Mathf.FloorToInt(maxMP * 0.2f);
            currentMP = maxMP;
            agility += Mathf.FloorToInt(agility * 0.2f);
            numberOfDiceFaces++;

            if (characterLevel % 5 == 0 && diceLevel <= maxDiceLevel) {
                diceLevel++;
                diceCoefficient += 0.5f;
            }
        }

        // Check if character is at max level, exp cannot go up
        if (characterLevel == maxCharacterLevel) {
            currentEXP = 0;
        }
    }
}