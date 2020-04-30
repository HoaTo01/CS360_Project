using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTargetButton : MonoBehaviour {
    // Elements
    public string skillName;
    public int target;
    public Text targetName;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Press a choosing target button.
    public void Press() {
        BattleManager.selfReference.PlayerAttack(skillName, target);
    }
}
