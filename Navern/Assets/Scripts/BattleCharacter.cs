using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour {
    // Elements
    public bool isPlayer;
    public bool isABoss;
    public string[] skillsAvailable;

    public string characterName;

    public int currentHP, maxHP, currentMP, maxMP, agility, numberOfDiceFaces, characterLevel;
    public double diceCoefficient;

    public bool isDead;

    private bool shouldFade;
    public float fadeSpeed = 1f;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(shouldFade) {
            sprite.color = new Color(255, 255, 255,Mathf.MoveTowards(sprite.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(sprite.color.a== 0) {
                gameObject.SetActive(false);
            }
        }
    }

    // Fade the enemy away.
    public void FadeEnemy() {
        shouldFade = true;
    }
}
