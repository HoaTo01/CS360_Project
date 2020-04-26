using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
