using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleNotifications : MonoBehaviour {
    // Elements
    public float popUpTime;
    private float popUpTimeCounter;
    public Text notificationsText;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (popUpTimeCounter > 0) {
            popUpTimeCounter -= Time.deltaTime;

            if (popUpTimeCounter <= 0) {
                gameObject.SetActive(false);
            }
        }
    }

    // Activate the notifications pop-up.
    public void Activate() {
        gameObject.SetActive(true);
        popUpTimeCounter = popUpTime;
    }
}
