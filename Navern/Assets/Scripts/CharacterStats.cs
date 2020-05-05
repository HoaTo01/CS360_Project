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
    public double diceCoefficient;

    public int currentHP;
    public int currentMP;
    public int currentEXP;

    public string weaponEquipment;
    public string necklaceEquipment;
    public string ringEquipment;
    public string shoesEquipment;

    public Sprite weaponImage;
    public Sprite ringImage;
    public Sprite necklaceImage;
    public Sprite shoesImage;

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

    }

    // Add exp
    public void AddEXP(int expToAdd) {
        currentEXP += expToAdd;

        // Level up and reset the EXP.
        while (currentEXP > expToNextLevel[characterLevel - 1]) {
            currentEXP -= expToNextLevel[characterLevel - 1];
            // Increase the character's level and stats
            characterLevel++;

            maxHP += Mathf.CeilToInt(maxHP * 0.05f);
            currentHP = maxHP;
            maxMP += Mathf.CeilToInt(maxMP * 0.05f);
            currentMP = maxMP;
            agility += Mathf.CeilToInt(agility * 0.05f);
            numberOfDiceFaces++;

            if (characterLevel % 5 == 0 && diceLevel <= maxDiceLevel) {
                diceLevel++;
                diceCoefficient += 0.5f;
                numberOfDiceFaces++;
            }
        }


        // Check if character is at max level, exp cannot go up
        if (characterLevel == maxCharacterLevel) {
            currentEXP = 0;
        }
    }
}