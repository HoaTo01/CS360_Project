using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    // Elements
    public bool isItem, isWeapon, isRing, isNecklace, areShoes;

    [Header("Item's Descriptions")]
    public string itemName;
    public string description;
    public int itemValue;

    public Sprite itemSprite;

    [Header("Item's Details")]
    public string charsCanUse;
    public double amountEffect;
    public bool affectCurrentHP, affectCurrentMP, affectMaxHP, affectMaxMP, affectAgility, affectCoefficient;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Use an item.
    public void Use(int characterCode) {
        CharacterStats selectedChar = PartyManager.selfReference.membersStats[characterCode];

        if (isItem) {
            ApplyEffect(selectedChar);
        }

        if (isWeapon) {
            if (selectedChar.weaponEquipment != "") {
                GameManager.selfReference.AddItem(selectedChar.weaponEquipment);
                // Remove the effect of the equipped item.
                RemoveApplyEffect(selectedChar, GameManager.selfReference.GetItemDetails(selectedChar.weaponEquipment));
            }

            selectedChar.weaponEquipment = itemName;
            selectedChar.weaponImage = itemSprite;
            // Apply the effect of the equipped item.
            ApplyEffect(selectedChar);
        }

        if (isRing) {
            if (selectedChar.ringEquipment != "") {
                GameManager.selfReference.AddItem(selectedChar.ringEquipment);
                // Remove the effect of the equipped item.
                RemoveApplyEffect(selectedChar, GameManager.selfReference.GetItemDetails(selectedChar.weaponEquipment));
            }

            selectedChar.ringEquipment = itemName;
            selectedChar.ringImage = itemSprite;
            // Apply the effect of the equipped item.
            ApplyEffect(selectedChar);
        }

        if (isNecklace) {
            if (selectedChar.necklaceEquipment != "") {
                GameManager.selfReference.AddItem(selectedChar.necklaceEquipment);
                // Remove the effect of the equipped item.
                RemoveApplyEffect(selectedChar, GameManager.selfReference.GetItemDetails(selectedChar.necklaceEquipment));
            }

            selectedChar.necklaceEquipment = itemName;
            selectedChar.necklaceImage = itemSprite;
            // Apply the effect of the equipped item.
            ApplyEffect(selectedChar);
        }

        if (areShoes) {
            if (selectedChar.shoesEquipment != "") {
                GameManager.selfReference.AddItem(selectedChar.shoesEquipment);
                // Remove the effect of the equipped item.
                RemoveApplyEffect(selectedChar, GameManager.selfReference.GetItemDetails(selectedChar.shoesEquipment));
            }

            selectedChar.shoesEquipment = itemName;
            selectedChar.shoesImage = itemSprite;
            // Apply the effect of the equipped item.
            ApplyEffect(selectedChar);
        }

        GameManager.selfReference.RemoveItem(itemName);
        GameplayMenu.selfReference.UpdateStatsDisplay();
    }

    // Apply the effect of an item to a character.
    public void ApplyEffect(CharacterStats selectedChar) {
        if (affectCurrentHP) {
            selectedChar.currentHP += (int)amountEffect;

            if (selectedChar.currentHP > selectedChar.maxHP) {
                selectedChar.currentHP = selectedChar.maxHP;
            }
        }

        if (affectCurrentMP) {
            selectedChar.currentMP += (int)amountEffect;

            if (selectedChar.currentMP > selectedChar.maxMP) {
                selectedChar.currentMP = selectedChar.maxMP;
            }
        }

        if (affectMaxHP) {
            selectedChar.maxHP += (int)amountEffect;

            selectedChar.currentHP += (int)amountEffect;
        }

        if (affectMaxMP) {
            selectedChar.maxMP += (int)amountEffect;

            selectedChar.currentMP += (int)amountEffect;
        }

        if (affectAgility) {
            selectedChar.agility += (int)amountEffect;
        }

        if (affectCoefficient) {
            selectedChar.diceCoefficient += amountEffect;
        }
    }

    // Remove the effect of an item to a character.
    public void RemoveApplyEffect(CharacterStats selectedChar, Item item) {
        if (item.affectCurrentHP) {
            selectedChar.currentHP -= (int)item.amountEffect;

            if (selectedChar.currentHP < 0) {
                selectedChar.currentHP = 0;
            }
        }

        if (item.affectCurrentMP) {
            selectedChar.currentMP -= (int)item.amountEffect;

            if (selectedChar.currentMP < 0) {
                selectedChar.currentMP = 0;
            }
        }

        if (item.affectMaxHP) {
            selectedChar.maxHP -= (int)item.amountEffect;

            selectedChar.currentHP -= (int)item.amountEffect;

            if (selectedChar.currentHP < 0) {
                selectedChar.currentHP = 0;
            }
        }

        if (item.affectMaxMP) {
            selectedChar.maxMP -= (int)item.amountEffect;

            selectedChar.currentMP -= (int)item.amountEffect;

            if (selectedChar.currentMP < 0) {
                selectedChar.currentMP = 0;
            }
        }

        if (item.affectAgility) {
            selectedChar.agility -= (int)item.amountEffect;
        }

        if (item.affectCoefficient) {
            selectedChar.diceCoefficient -= item.amountEffect;
        }
    }
}
