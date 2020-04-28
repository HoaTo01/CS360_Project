using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMenu : MonoBehaviour {
    // Elements
    public static GameplayMenu selfReference;

    public GameObject gameplayMenu;
    public GameObject[] windows;
    public GameObject[] currentSelectArrows;
    public int currentArrowPos;

    [Header("Party Info")]
    public CharacterStats[] membersStats;

    public Text[] charactersName, hpText, mpText, levelText, expText,
                  agilityText, diceLevelText, coefficientText, facesCountText,
                  elementText, weaponName, ringName, necklaceName, shoesName;

    public Image[] charactersImage, weaponImage, ringImage, necklaceImage, shoesImage;

    public GameObject[] charStatsBox;

    [Header("Inventory Elements")]
    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;

    public GameObject itemChoosingCharMenu;
    public Text[] itemChoosingCharNames;

    public Text coinsText;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;
    }

    // Update is called once per frame
    void Update() {
        // Open/close the gameplay menu
        if (Input.GetButtonDown("Enter")) {
            if (gameplayMenu.activeInHierarchy) {
                // Close the menu.
                closeMenu();
            }

            else {
                // Update the stats
                UpdateStatsDisplay();

                // Set the current select arrow to the first button (Inventory).    
                currentSelectArrows[currentArrowPos].SetActive(false);
                currentArrowPos = 0;
                currentSelectArrows[currentArrowPos].SetActive(true);

                // Display the gameplay menu.
                gameplayMenu.SetActive(true);

                // Let the Game Manager knows that gamepplay menu is opened.
                GameManager.selfReference.gameplayMenuIsOpened = true;
            }

            // Play the SFX.
            AudioManager.selfReference.PlaySFX(5);
        }

        if (gameplayMenu.activeInHierarchy) {
            if (Input.GetButtonDown("No Button") && WindowsAreClosed()) {
                closeMenu();

                // Play the SFX.
                AudioManager.selfReference.PlaySFX(5);
            }

            // Control the gameplay menu windows
            ToggleWindowsByKeys();
        }

        // If the player chooses Close, close the menu.
        if (windows[4].activeInHierarchy) {
            closeMenu();

            windows[4].SetActive(false);
        }

        // If the player chooses Exit Game, exit to the main menu.
        if(windows[5].activeInHierarchy) {
            SceneManager.LoadScene("Main Menu");

            Destroy(GameManager.selfReference.gameObject);
            Destroy(PlayerControl.selfReference.gameObject);
            Destroy(AudioManager.selfReference.gameObject);
            Destroy(gameObject);
        }
    }

    // Update the stats display in the Party UI.
    public void UpdateStatsDisplay() {
        membersStats = PartyManager.selfReference.membersStats;

        for (int i = 0; i < membersStats.Length; i++) {
            if (membersStats[i].gameObject.activeInHierarchy) {
                // Display the stats box for active characters.
                charStatsBox[i].SetActive(true);

                // Update the name of the characters
                charactersName[i].text = membersStats[i].characterName;

                // Update the sprite of the characters.
                charactersImage[i].sprite = membersStats[i].characterSprite;

                // Update the stats of the characters.
                hpText[i].text = "HP: " + membersStats[i].currentHP + "/" + membersStats[i].maxHP;
                mpText[i].text = "MP: " + membersStats[i].currentMP + "/" + membersStats[i].maxMP;
                levelText[i].text = "Level: " + membersStats[i].characterLevel;
                expText[i].text = "EXP: " + membersStats[i].currentEXP + "/" + membersStats[i].expToNextLevel[membersStats[i].characterLevel - 1];

                agilityText[i].text = "Agility: " + membersStats[i].agility;
                diceLevelText[i].text = "Dice Level: " + membersStats[i].diceLevel;
                coefficientText[i].text = "Coefficient: " + membersStats[i].diceCoefficient;
                facesCountText[i].text = "Faces Count: " + membersStats[i].numberOfDiceFaces;

                elementText[i].text = "Element: " + membersStats[i].element;

                // Update equipment of the characters.
                weaponName[i].text = membersStats[i].weaponEquipment;
                ringName[i].text = membersStats[i].ringEquipment;
                necklaceName[i].text = membersStats[i].necklaceEquipment;
                shoesName[i].text = membersStats[i].shoesEquipment;

                weaponImage[i].sprite = membersStats[i].weaponImage;
                ringImage[i].sprite = membersStats[i].ringImage;
                necklaceImage[i].sprite = membersStats[i].necklaceImage;
                shoesImage[i].sprite = membersStats[i].shoesImage;
            }

            else {
                charStatsBox[i].SetActive(false);
            }
        }
    }

    // Toggle to open windows in the gameplay menu using mouse.
    public void ToggleWindows(int windowCode) {
        for (int i = 0; i < windows.Length; i++) {
            if (i == windowCode) {
                // Set the arrow to the chosen button.
                currentSelectArrows[currentArrowPos].SetActive(false);
                currentArrowPos = windowCode;
                currentSelectArrows[currentArrowPos].SetActive(true);

                windows[currentArrowPos].SetActive(true);
            }

            else {
                windows[i].SetActive(false);
            }
        }

        itemChoosingCharMenu.SetActive(false);
    }

    // Toggle to open windows in the gameplay menu using mouse.
    public void ToggleWindowsByKeys() {
        // Update the inventory.
        ShowItems();

        // If the player presses the right key, current select arrow + 1 and goes to the corresponding button based on its code number.
        if (Input.GetKeyDown("right") && currentArrowPos < 5 && WindowsAreClosed()) {
            currentSelectArrows[currentArrowPos].SetActive(false);
            currentArrowPos++;
            currentSelectArrows[currentArrowPos].SetActive(true);

            // Play the SFX.
            PlaySFXButtons();
        }

        // If the player presses the right key, current select arrow - 1 and goes to the corresponding button based on its code number.
        if (Input.GetKeyDown("left") && currentArrowPos > 0 && WindowsAreClosed()) {
            currentSelectArrows[currentArrowPos].SetActive(false);
            currentArrowPos--;
            currentSelectArrows[currentArrowPos].SetActive(true);

            // Play the SFX.
            PlaySFXButtons();
        }

        // If the player presses the down key, current select arrow + 3 and goes to the corresponding button based on its code number.
        if (Input.GetKeyDown("down") && currentArrowPos < 3 && WindowsAreClosed()) {
            currentSelectArrows[currentArrowPos].SetActive(false);
            currentArrowPos += 3;
            currentSelectArrows[currentArrowPos].SetActive(true);

            // Play the SFX.
            PlaySFXButtons();
        }

        // If the player presses the up key, current select arrow - 3 and goes to the corresponding button based on its code number.
        if (Input.GetKeyDown("up") && currentArrowPos > 2 && WindowsAreClosed()) {
            currentSelectArrows[currentArrowPos].SetActive(false);
            currentArrowPos -= 3;
            currentSelectArrows[currentArrowPos].SetActive(true);

            // Play the SFX.
            PlaySFXButtons();
        }

        if (Input.GetButtonDown("Yes Button")) {
            windows[currentArrowPos].SetActive(true);

            // Play the SFX.
            PlaySFXButtons();
        }

        if (Input.GetButtonDown("No Button")) {
            for (int i = 0; i < windows.Length; i++) {
                windows[i].SetActive(false);
            }

            // Play the SFX.
            PlaySFXButtons();
        }
    }

    // Check if all windows are closed.
    private bool WindowsAreClosed() {
        for (int i = 0; i < windows.Length; i++) {
            if (windows[i].activeInHierarchy) {
                return false;
            }
        }

        return true;
    }

    // Close the gameplay menu.
    public void closeMenu() {
        for (int i = 0; i < windows.Length; i++) {
            windows[i].SetActive(false);
        }

        gameplayMenu.SetActive(false);

        // Let the Game Manager knows that gamepplay menu is closed.
        GameManager.selfReference.gameplayMenuIsOpened = false;
        itemChoosingCharMenu.SetActive(false);
    }

    // Show the items in the inventory.
    public void ShowItems() {
        // Sort the items in the inventory.
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

        // Update the gold coins amount in the inventory.
        coinsText.text = GameManager.selfReference.currentCoins.ToString() + " GC";
    }

    // Select an item.
    public void SelectItem(Item item) {
        activeItem = item;

        if (activeItem.isItem) {
            useButtonText.text = "Use";
        }

        if (activeItem.isWeapon || activeItem.isRing || activeItem.isNecklace || activeItem.areShoes) {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    // Drop item's button.
    public void DropItem() {
        if (activeItem != null) {
            GameManager.selfReference.RemoveItem(activeItem.itemName);
        }
    }

    // Open the choosing-character-to-use-item-on panel.
    public void OpenItemChoosingChar() {
        itemChoosingCharMenu.SetActive(true);

        for (int i = 0; i < itemChoosingCharNames.Length; i++) {
            itemChoosingCharNames[i].text = PartyManager.selfReference.membersStats[i].characterName;
            itemChoosingCharNames[i].transform.parent.gameObject.SetActive(PartyManager.selfReference.membersStats[i].gameObject.activeInHierarchy);
        }
    }

    // Close the choosing-character-to-use-item-on panel.
    public void CloseItemChoosingChar() {
        itemChoosingCharMenu.SetActive(false);
    }

    // Use/equip an item on a character.
    public void UseItem(int characterCode) {
        activeItem.Use(characterCode);
        CloseItemChoosingChar();
    }

    // Save the game.
    public void SaveGame() {
        GameManager.selfReference.SaveData();
        QuestManager.selfReference.SaveQuestData();

        windows[2].SetActive(false);
    }

    // Close save window.
    public void closeSaveWindow() {
        windows[2].SetActive(false);
    }

    // Play the SFX for buttons.
    public void PlaySFXButtons() {
        AudioManager.selfReference.PlaySFX(4);
    }
}
