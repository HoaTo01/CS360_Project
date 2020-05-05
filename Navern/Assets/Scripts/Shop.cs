using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    // Elements
    public static Shop selfReference;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text coinstText;

    public string[] ItemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectedItem;
    public Text buyItemName, buyItemDescription, buyItemPrice;
    public Text sellItemName, sellItemDescription, sellItemValue;

    public float sellValueCoefficient = 0.3f;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;
    }

    // Update is called once per frame
    void Update() {

    }

    // Open the shop menu.
    public void OpenShop() {
        shopMenu.SetActive(true);
        GameManager.selfReference.shopIsOpened = true;

        // Play the SFX.
        AudioManager.selfReference.PlaySFX(5);

        // Open the buy menu when shop menu is opened.
        OpenBuyMenu();

        coinstText.text = GameManager.selfReference.currentCoins.ToString() + " GC";
    }

    // Close the shop menu.
    public void CloseShop() {
        shopMenu.SetActive(false);
        GameManager.selfReference.shopIsOpened = false;

        // Play the SFX.
        AudioManager.selfReference.PlaySFX(5);
    }

    // Open the buy menu and close the sell menu.
    public void OpenBuyMenu() {
        // Select the first item when open the buy menu.
        buyItemButtons[0].Press();

        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++) {
            buyItemButtons[i].buttonValue = i;

            if (ItemsForSale[i] != "") {
                buyItemButtons[i].itemImage.gameObject.SetActive(true);
                buyItemButtons[i].itemImage.sprite = GameManager.selfReference.GetItemDetails(ItemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }

            else {
                buyItemButtons[i].itemImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }

    // Open the buy menu and close the sell menu.
    public void OpenSellMenu() {
        // Select the first item when open the sell menu.
        sellItemButtons[0].Press();

        sellMenu.SetActive(true);
        buyMenu.SetActive(false);

        // Show items in the sell menu.
        ShowItemsAfterSell();
    }

    // Show items in the sell menu.
    private void ShowItemsAfterSell() {
        // Sort the items in the inventory.
        GameManager.selfReference.SortItems();

        for (int i = 0; i < sellItemButtons.Length; i++) {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.selfReference.itemsInInventory[i] != "") {
                sellItemButtons[i].itemImage.gameObject.SetActive(true);
                sellItemButtons[i].itemImage.sprite = GameManager.selfReference.GetItemDetails(GameManager.selfReference.itemsInInventory[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.selfReference.itemsAmount[i].ToString();
            }

            else {
                sellItemButtons[i].itemImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }

    // Select an item in the buy menu.
    public void SelectBuyItem(Item item) {
        selectedItem = item;

        if (selectedItem != null) {
            buyItemName.text = selectedItem.itemName;
            buyItemDescription.text = selectedItem.description;
            buyItemPrice.text = "Price: " + selectedItem.itemValue + " GC";
        }

        if (selectedItem == null) {
            buyItemName.text = "Shopkeeper:";
            buyItemDescription.text = "What would you like to buy?";
            buyItemPrice.text = "Price: ";
        }
    }

    // Select an item in the sell menu.
    public void SelectSellItem(Item item) {
        selectedItem = item;

        if (selectedItem != null) {
            sellItemName.text = selectedItem.itemName;
            sellItemDescription.text = selectedItem.description;
            sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.itemValue * sellValueCoefficient).ToString() + " GC";
        }

        if (selectedItem == null) {
            sellItemName.text = "Shopkeeper:";
            sellItemDescription.text = "What would you like to sell?";
            sellItemValue.text = "Value: ";
        }
    }

    // Buy an item when press the Buy Button.
    public void BuyItem() {
        if (selectedItem != null) {
            if (GameManager.selfReference.currentCoins >= selectedItem.itemValue) {
                GameManager.selfReference.currentCoins -= selectedItem.itemValue;

                GameManager.selfReference.AddItem(selectedItem.itemName);
            }

            else {
                buyItemName.text = "Shopkeeper:";
                buyItemDescription.text = "You don't have enough money!";
                buyItemPrice.text = "Price: ";
            }

            coinstText.text = GameManager.selfReference.currentCoins.ToString() + " GC";
        }
    }

    // Sell an item when press the Sell Button.
    public void SellItem() {
        if (selectedItem != null) {
            GameManager.selfReference.currentCoins += Mathf.FloorToInt(selectedItem.itemValue * sellValueCoefficient);

            GameManager.selfReference.RemoveItem(selectedItem.itemName);
        }

        coinstText.text = GameManager.selfReference.currentCoins.ToString() + " GC";

        // Show the items in the in the inventory after selling an item.
        ShowItemsAfterSell();
        sellItemButtons[0].Press();
    }
}
