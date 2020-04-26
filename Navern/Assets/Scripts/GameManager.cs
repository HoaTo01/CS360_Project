using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // Elements
    public static GameManager selfReference;

    // Can-move control elements
    public bool gameplayMenuIsOpened, dialogIsOpened, isInTransition, shopIsOpened;

    // Inventory's elements
    [Header("Inventory Elements")]
    public string[] itemsInInventory;
    public int[] itemsAmount;
    public Item[] referenceItems;

    public int currentCoins;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        //Prevent characters from moving in certain situations.
        MovingControl();
    }

    //Prevent characters from moving in certain situations.
    private void MovingControl() {
        if (gameplayMenuIsOpened || dialogIsOpened || isInTransition || shopIsOpened) {
            PlayerControl.selfReference.canMove = false;
        }

        else {
            PlayerControl.selfReference.canMove = true;
        }
    }

    public Item GetItemDetails(string itemName) {
        for (int i = 0; i < referenceItems.Length; i++) {
            if (object.Equals(referenceItems[i].itemName, itemName)) {
                return referenceItems[i];
            }
        }

        return null;
    }

    // Sort the items in the inventory.
    public void SortItems() {
        bool itemAfterEmptySlot = true;

        while (itemAfterEmptySlot) {
            itemAfterEmptySlot = false;

            for (int i = 0; i < itemsInInventory.Length - 1; i++) {
                if (itemsInInventory[i] == "") {
                    itemsInInventory[i] = itemsInInventory[i + 1];
                    itemsInInventory[i + 1] = "";

                    itemsAmount[i] = itemsAmount[i + 1];
                    itemsAmount[i + 1] = 0;

                    if (itemsInInventory[i] != "") {
                        itemAfterEmptySlot = true;
                    }
                }
            }
        }
    }

    // Add an item.
    public void AddItem(string item) {
        int itemPosition = 0;
        bool foundSlot = false;
        bool itemExists = false;

        // Find a slot
        for (int i = 0; i < itemsInInventory.Length; i++) {
            if (itemsInInventory[i] == item) {
                itemPosition = i;
                i = itemsInInventory.Length;
                foundSlot = true;
                itemExists = true;
            }
        }

        if (!itemExists) {
            for (int i = 0; i < itemsInInventory.Length; i++) {
                if (itemsInInventory[i] == "") {
                    itemPosition = i;
                    i = itemsInInventory.Length;
                    foundSlot = true;
                }
            }
        }

        // If found a slot, add the item.
        if (foundSlot) {
            for (int i = 0; i < referenceItems.Length; i++) {
                if (referenceItems[i].itemName == item) {
                    itemExists = true;

                    i = referenceItems.Length;
                }
            }

            if (itemExists) {
                itemsInInventory[itemPosition] = item;
                itemsAmount[itemPosition]++;
            }

            else {
                Debug.LogError(item + "does not exist.");
            }

            // Update the inventory.
            GameplayMenu.selfReference.ShowItems();
        }
    }

    // Remove an item.
    public void RemoveItem(string item) {
        bool foundItem = false;
        int itemPosition = 0;

        // Loop through to check if the item exists in the inventory.
        for (int i = 0; i < itemsInInventory.Length; i++) {
            if (itemsInInventory[i] == item) {
                foundItem = true;
                itemPosition = i;

                i = itemsInInventory.Length;
            }
        }

        // If item exists, remove item.
        if (foundItem) {
            itemsAmount[itemPosition]--;

            if (itemsAmount[itemPosition] <= 0) {
                itemsInInventory[itemPosition] = "";
            }

            GameplayMenu.selfReference.ShowItems();
        }

        else {
            Debug.LogError("Couldn't find " + item);
        }
    }

    // Save the game's data.
    public void SaveData() {
        // Save the position.
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerControl.selfReference.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerControl.selfReference.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerControl.selfReference.transform.position.z);

        // Save character's info.
        for (int i = 0; i < PartyManager.selfReference.membersStats.Length; i++) {
            if (PartyManager.selfReference.membersStats[i].gameObject.activeInHierarchy) {
                PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_active", 1);
            }

            else {
                PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_active", 0);
            }

            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_CurrentHP", PartyManager.selfReference.membersStats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_MaxHP", PartyManager.selfReference.membersStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_CurrentMP", PartyManager.selfReference.membersStats[i].currentMP);
            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_MaxMP", PartyManager.selfReference.membersStats[i].maxMP);
            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_CurrentEXP", PartyManager.selfReference.membersStats[i].currentEXP);
            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_CharacterLevel", PartyManager.selfReference.membersStats[i].characterLevel);
            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Agility", PartyManager.selfReference.membersStats[i].agility);
            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_DiceLevel", PartyManager.selfReference.membersStats[i].diceLevel);
            PlayerPrefs.SetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_DiceFaces", PartyManager.selfReference.membersStats[i].numberOfDiceFaces);
            PlayerPrefs.SetFloat("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_DiceCoefficient", (float)PartyManager.selfReference.membersStats[i].diceCoefficient);
            PlayerPrefs.SetString("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Weapon", PartyManager.selfReference.membersStats[i].weaponEquipment);
            PlayerPrefs.SetString("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Ring", PartyManager.selfReference.membersStats[i].ringEquipment);
            PlayerPrefs.SetString("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Necklace", PartyManager.selfReference.membersStats[i].necklaceEquipment);
            PlayerPrefs.SetString("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Shoes", PartyManager.selfReference.membersStats[i].shoesEquipment);
        }

        // Save inventory's data.
        for (int i = 0; i < itemsInInventory.Length; i++) {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsInInventory[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, itemsAmount[i]);
        }

        PlayerPrefs.SetInt("GoldCoins", currentCoins);
    }

    // Load the game's data.
    public void LoadData() {
        // Load the character's position.
        PlayerControl.selfReference.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        // Load the character's info.
        for (int i = 0; i < PartyManager.selfReference.membersStats.Length; i++) {
            if (PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_active") == 1) {
                PartyManager.selfReference.membersStats[i].gameObject.SetActive(true);
            }

            else {
                PartyManager.selfReference.membersStats[i].gameObject.SetActive(false);
            }

            PartyManager.selfReference.membersStats[i].currentHP = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_CurrentHP");
            PartyManager.selfReference.membersStats[i].maxHP = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_MaxHP");
            PartyManager.selfReference.membersStats[i].currentMP = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_CurrentMP");
            PartyManager.selfReference.membersStats[i].maxMP = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_MaxMP");
            PartyManager.selfReference.membersStats[i].currentEXP = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_CurrentEXP");
            PartyManager.selfReference.membersStats[i].characterLevel = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_CharacterLevel");
            PartyManager.selfReference.membersStats[i].agility = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Agility");
            PartyManager.selfReference.membersStats[i].diceLevel = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_DiceLevel");
            PartyManager.selfReference.membersStats[i].numberOfDiceFaces = PlayerPrefs.GetInt("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_DiceFaces");
            PartyManager.selfReference.membersStats[i].diceCoefficient = PlayerPrefs.GetFloat("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_DiceCoefficient");
            PartyManager.selfReference.membersStats[i].weaponEquipment = PlayerPrefs.GetString("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Weapon");
            PartyManager.selfReference.membersStats[i].ringEquipment = PlayerPrefs.GetString("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Ring");
            PartyManager.selfReference.membersStats[i].necklaceEquipment = PlayerPrefs.GetString("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Necklace");
            PartyManager.selfReference.membersStats[i].shoesEquipment = PlayerPrefs.GetString("Player_" + PartyManager.selfReference.membersStats[i].characterName + "_Shoes");
        }

        // Load inventory's data
        for (int i = 0; i < itemsInInventory.Length; i++) {
            itemsInInventory[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            itemsAmount[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }

        currentCoins = PlayerPrefs.GetInt("GoldCoins");
    }
}
