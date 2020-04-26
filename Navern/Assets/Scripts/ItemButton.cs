using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {
    // Elements
    public Image itemImage;
    public Text amountText;
    public int buttonValue;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Process the logic when press an item button in the inventory.
    public void Press() {
        if (GameplayMenu.selfReference.gameplayMenu.activeInHierarchy) {
            if (GameManager.selfReference.itemsInInventory[buttonValue] != "") {
                GameplayMenu.selfReference.SelectItem(GameManager.selfReference.GetItemDetails(GameManager.selfReference.itemsInInventory[buttonValue]));
            }

            if (GameManager.selfReference.itemsInInventory[buttonValue] == "") {
                GameplayMenu.selfReference.itemName.text = "";
                GameplayMenu.selfReference.itemDescription.text = "";
            }
        }

        if (Shop.selfReference.shopMenu.activeInHierarchy) {
            if (Shop.selfReference.buyMenu.activeInHierarchy) {
                Shop.selfReference.SelectBuyItem(GameManager.selfReference.GetItemDetails(Shop.selfReference.ItemsForSale[buttonValue]));
            }

            if (Shop.selfReference.sellMenu.activeInHierarchy) {
                Shop.selfReference.SelectSellItem(GameManager.selfReference.GetItemDetails(GameManager.selfReference.itemsInInventory[buttonValue]));
            }
        }
    }
}
