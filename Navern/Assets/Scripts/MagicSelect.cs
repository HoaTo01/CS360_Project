using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicSelect : MonoBehaviour {
    // Elements
    public string skillName;
    public int skillCost;
    public Text skillNameText;
    public Text skillCostText;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Press a magic button.
    public void Press() {
        BattleManager.selfReference.OpenMagicInfoWindow(skillName);
    }

    // Pres the use button in the magic info window.
    public void UseMagicPress() {
        BattleManager.selfReference.UseMagic(skillName);
    }
}
