using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour {
    // Elements
    public static Dice selfReference;

    public GameObject playerDice;
    public GameObject enemyDice;
    public Sprite[] diceSides;

    public bool isPlayer;
    public bool diceAreRunning;

    private const int numberOfSwitches = 15;
    public int playerNumberOfDiceFaces;
    public int enemyNumberOfDiceFaces;

    public int playerValue;
    public int enemyValue;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;
    }

    // Update is called once per frame
    void Update() {

    }

    // Coroutine that rolls the dice.
    public IEnumerator RollTheDice() {
        int playerRandomDiceSide = 0;
        int enemyRandomDiceSide = 0;

        // Set both of the dice to be visible.
        playerDice.SetActive(true);
        enemyDice.SetActive(true);

        // Let the game know the dice are running.
        diceAreRunning = true;

        // Loop to switch dice sides ramdomly
        for (int i = 0; i <= numberOfSwitches; i++) {
            playerRandomDiceSide = Random.Range(0, playerNumberOfDiceFaces);
            enemyRandomDiceSide = Random.Range(0, enemyNumberOfDiceFaces);

            // Set sprite to upper face of dice from array according to random value     
            SpriteRenderer playerDiceSprite = playerDice.GetComponent<SpriteRenderer>();
            SpriteRenderer enemyDiceSprite = enemyDice.GetComponent<SpriteRenderer>();
            playerDiceSprite.sprite = diceSides[playerRandomDiceSide];
            enemyDiceSprite.sprite = diceSides[enemyRandomDiceSide];

            // Wait for a while before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.8f);

        diceAreRunning = false;

        // Hide the dices after done rolling.
        playerDice.SetActive(false);
        enemyDice.SetActive(false);

        playerValue = playerRandomDiceSide + 1;
        enemyValue = enemyRandomDiceSide + 1;
    }
}
