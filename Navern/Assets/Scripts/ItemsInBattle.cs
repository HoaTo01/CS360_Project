﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsInBattle : MonoBehaviour {
    // Elements
    public static ItemsInBattle selfReference;

    public GameObject itemsWindow;

    public ItemButton[] itemButtons;

    public Item selectedItem;
    public Text itemName, itemDescription;

    public GameObject itemChoosingCharMenu;
    public Text[] itemChoosingCharNames;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;
    }

    // Update is called once per frame
    void Update() {

    }

    // Open Items Window in battle.
    public void OpenItemsWindow() {
        itemsWindow.SetActive(true);
        itemButtons[0].Press();

        ShowItems();
    }

    // Show the items.
    public void ShowItems() {
        GameManager.selfReference.SortItems();

        for (int i = 0; i < itemButtons.Length; i++) {
            itemButtons[i].buttonValue = i;

            if (GameManager.selfReference.itemsInInventory[i] != "") {
                itemButtons[i].itemImage.gameObject.SetActive(true);
                itemButtons[i].itemImage.sprite = GameManager.selfReference.GetItemDetails(GameManager.selfReference.itemsInInventory[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.selfReference.itemsAmount[i].ToString();
            }

            else {
                itemButtons[i].itemImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    // Close the items window.
    public void closeItemsWindow() {
        itemsWindow.SetActive(false);
    }

    // Select an item.
    public void SelectItem(Item item) {
        selectedItem = item;

        if (selectedItem == null) {
            itemName.text = "";
            itemDescription.text = "Which item do you want to use?";
        }

        else if (selectedItem.isWeapon || selectedItem.isRing || selectedItem.isNecklace || selectedItem.areShoes) {
            itemName.text = "";
            itemDescription.text = "You cannot equip an item in a battle.";

            selectedItem = null;
        }

        else if (selectedItem != null) {
            itemName.text = selectedItem.itemName;
            itemDescription.text = selectedItem.description;
        }
    }

    // Open the choosing-character-to-use-item-on panel.
    public void OpenItemChoosingChar() {
        if (selectedItem != null) {
            itemChoosingCharMenu.SetActive(true);

            for (int i = 0; i < itemChoosingCharNames.Length; i++) {
                itemChoosingCharNames[i].text = PartyManager.selfReference.membersStats[i].characterName;
                itemChoosingCharNames[i].transform.parent.gameObject.SetActive(PartyManager.selfReference.membersStats[i].gameObject.activeInHierarchy);
            }
        }
    }

    // Close the choosing-character-to-use-item-on panel.
    public void CloseItemChoosingChar() {
        itemChoosingCharMenu.SetActive(false);
    }

    // Use an item on a character.
    public void UseItem(int characterCode) {
        selectedItem.Use(characterCode);
        CloseItemChoosingChar();
        closeItemsWindow();

        ShowItems();

        // Update the battle stats.
        CharacterStats characterStats = PartyManager.selfReference.membersStats[characterCode];
        BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].currentHP = characterStats.currentHP;
        BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].maxHP = characterStats.maxHP;
        BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].currentMP = characterStats.currentMP;
        BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].maxMP = characterStats.maxMP;
        BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].agility = characterStats.agility;
        BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].diceCoefficient = characterStats.diceCoefficient;
        BattleManager.selfReference.activeBattleCharacters[BattleManager.selfReference.currentTurn].numberOfDiceFaces = characterStats.numberOfDiceFaces;

        BattleManager.selfReference.NextTurn();
    }
}
