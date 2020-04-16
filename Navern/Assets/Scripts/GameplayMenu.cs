using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : MonoBehaviour {
    // Elements
    public GameObject gameplayMenu;
    public GameObject[] windows;
    public GameObject[] currentSelectArrows;
    public int currentArrowPos;

    public CharacterStats[] membersStats;

    public Text[] charactersName, hpText, mpText, levelText, expText,
                  agilityText, diceLevelText, coefficientText, facesCountText,
                  elementText, weaponName, ringName, necklaceName, shoesName;

    public Image[] charactersImage, weaponImage, ringImage, necklaceImage, shoesImage;

    public GameObject[] charStatsBox;

    // Start is called before the first frame update
    void Start() {

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
                updateStatsDisplay();

                // Set the current select arrow to the first button (Inventory).    
                currentSelectArrows[currentArrowPos].SetActive(false);
                currentArrowPos = 0;
                currentSelectArrows[currentArrowPos].SetActive(true);

                // Display the gameplay menu.
                gameplayMenu.SetActive(true);

                // Let the Game Manager knows that gamepplay menu is opened.
                GameManager.selfReference.gameplayMenuIsOpened = true;
            }
        }

        if (gameplayMenu.activeInHierarchy) {
            if (Input.GetButtonDown("No Button") && WindowsAreClosed()) {
                closeMenu();
            }

            // Control the gameplay menu windows
            ToggleWindowsByKeys();
        }
    }

    // Update the stats display in the Party UI.
    private void updateStatsDisplay() {
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

                windows[i].SetActive(!windows[i].activeInHierarchy);
            }

            else {
                windows[i].SetActive(false);
            }

            // If the player clicks Close, close the menu.
            if(windows[4].activeInHierarchy) {
                closeMenu();
            }
        }
    }

    // Toggle to open windows in the gameplay menu using mouse.
    public void ToggleWindowsByKeys() {
        // If the player presses the right key, current select arrow + 1 and goes to the corresponding button based on its code number.
        if (Input.GetKeyDown("right") && currentArrowPos < 5 && WindowsAreClosed()) {
            currentSelectArrows[currentArrowPos].SetActive(false);
            currentArrowPos++;
            currentSelectArrows[currentArrowPos].SetActive(true);
        }

        // If the player presses the right key, current select arrow - 1 and goes to the corresponding button based on its code number.
        if (Input.GetKeyDown("left") && currentArrowPos > 0 && WindowsAreClosed()) {
            currentSelectArrows[currentArrowPos].SetActive(false);
            currentArrowPos--;
            currentSelectArrows[currentArrowPos].SetActive(true);
        }

        // If the player presses the down key, current select arrow + 3 and goes to the corresponding button based on its code number.
        if (Input.GetKeyDown("down") && currentArrowPos < 3 && WindowsAreClosed()) {
            currentSelectArrows[currentArrowPos].SetActive(false);
            currentArrowPos += 3;
            currentSelectArrows[currentArrowPos].SetActive(true);
        }

        // If the player presses the up key, current select arrow - 3 and goes to the corresponding button based on its code number.
        if (Input.GetKeyDown("up") && currentArrowPos > 2 && WindowsAreClosed()) {
            currentSelectArrows[currentArrowPos].SetActive(false);
            currentArrowPos -= 3;
            currentSelectArrows[currentArrowPos].SetActive(true);
        }

        if (Input.GetButtonDown("Yes Button")) {
            windows[currentArrowPos].SetActive(true);
        }

        if (Input.GetButtonDown("No Button")) {
            windows[currentArrowPos].SetActive(false);
        }
        
        // If the player chooses Close, close the menu.
        if(windows[4].activeInHierarchy) {
            closeMenu();
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
        for(int i = 0; i< windows.Length; i++) {
            windows[i].SetActive(false);
        }

        gameplayMenu.SetActive(false);

        // Let the Game Manager knows that gamepplay menu is closed.
        GameManager.selfReference.gameplayMenuIsOpened = false;
    }
}
