using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    // Elements
    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;
    public GameObject arrowDown;

    public string[] dialogLines;
    public int currentLinePos;

    public static DialogManager selfReference;

    private bool textIsRunning;

    private string questToFlag;
    private bool flagQuestCompleted;
    private bool shouldFlagQuest;

    // Start is called before the first frame update
    void Start() {
        if (selfReference == null) {
            selfReference = this;
        }
    }

    // Update is called once per frame
    void Update() {
        // Move to another line of the dialog
        if (dialogBox.activeInHierarchy) {
            if (Input.GetButtonDown("Yes Button") && textIsRunning == false) {

                currentLinePos++;

                if (currentLinePos >= dialogLines.Length) {
                    dialogBox.SetActive(false);

                    // Let the Game Manager knows that the dialog box is closed.
                    GameManager.selfReference.dialogIsOpened = false;

                    // Flag quest after dialog if shouldFlagQuest = true.
                    if (shouldFlagQuest) {
                        shouldFlagQuest = false;

                        if (flagQuestCompleted) {
                            QuestManager.selfReference.FlagQuestCompleted(questToFlag);
                        }

                        else {
                            QuestManager.selfReference.FlagQuestNotCompleted(questToFlag);
                        }
                    }
                }

                else {
                    CheckForName();
                    checkIfMoreDialog();

                    // If dialogLines[currentLinePos - 1] is a name line, ignore it
                    if (dialogLines[currentLinePos - 1].StartsWith("name-")) {
                        if (arrowDown.activeInHierarchy) {
                            StartCoroutine(TypeDialog(dialogLines[currentLinePos], dialogLines[currentLinePos + 1]));
                            currentLinePos++;
                        }

                        else {
                            StartCoroutine(TypeDialog("", dialogLines[currentLinePos]));
                        }
                    }

                    else {
                        if (dialogLines[currentLinePos - 1].Contains("\n")) {
                            StartCoroutine(TypeDialog("", dialogLines[currentLinePos]));
                        }

                        else {
                            StartCoroutine(TypeDialog(dialogLines[currentLinePos - 1], dialogLines[currentLinePos]));
                        }
                    }
                }

                // Play the SFX.
                AudioManager.selfReference.PlaySFX(4);
            }
        }
    }

    // Show the dialog
    public void ShowDialog(string[] dialogLines, bool isPerson) {
        this.dialogLines = dialogLines;

        // Allow Unity to type a break line in the inspector
        for (int i = 0; i < dialogLines.Length; i++) {
            dialogLines[i] = dialogLines[i].Replace("___", "\n");
        }

        currentLinePos = 0;

        CheckForName();
        checkIfMoreDialog();

        dialogBox.SetActive(true);

        StartCoroutine(TypeDialog(dialogLines[currentLinePos], ""));

        nameBox.SetActive(isPerson);

        // Let the Game Manager knows that dialog box is opened.
        GameManager.selfReference.dialogIsOpened = true;
    }

    // Check the dialog lines for names to use
    public void CheckForName() {
        if (dialogLines[currentLinePos].StartsWith("name-")) {
            nameText.text = dialogLines[currentLinePos].Replace("name-", "");

            currentLinePos++;
        }
    }

    // Check if there's more dialog of a character
    public void checkIfMoreDialog() {
        if (currentLinePos < dialogLines.Length - 1) {
            if (dialogLines[currentLinePos + 1].StartsWith("name-") || currentLinePos == dialogLines.Length - 1) {
                arrowDown.SetActive(false);
            }

            else {
                arrowDown.SetActive(true);
            }
        }
    }

    // Text running through the dialog box
    public IEnumerator TypeDialog(string dialogLine1, string dialogLine2) {
        // if the dialog just started, run through both dialog lines
        if (dialogLines[currentLinePos - 1].StartsWith("name-")) {
            textIsRunning = true;

            dialogText.text = "";
            string dialogLine = dialogLine1 + dialogLine2;

            foreach (char letter in dialogLine.ToCharArray()) {
                dialogText.text += letter;
                yield return null;
            }

            textIsRunning = false;
        }

        // If the dialog didn't just start, run through only the second dialogLine
        else {
            textIsRunning = true;

            dialogText.text = dialogLine1;

            foreach (char letter in dialogLine2.ToCharArray()) {
                dialogText.text += letter;

                yield return null;
            }

            textIsRunning = false;
        }
    }

    // Know a quest should be activated.
    public void ShouldActivateQuest(string quest, bool flagCompleted) {
        questToFlag = quest;
        flagQuestCompleted = flagCompleted;

        shouldFlagQuest = true;
    }
}
