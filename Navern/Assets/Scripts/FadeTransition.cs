using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour {
    // Elements
    public static FadeTransition selfReference;

    public Image transitionScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    private float fadeSpeed = 10f;

    // Start is called before the first frame update
    void Start() {
        if (selfReference == null) {
            selfReference = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        // Fade the transition screen to black
        if (isFadeToBlack) {
            transitionScreen.color = new Color(transitionScreen.color.r, transitionScreen.color.g, transitionScreen.color.b,
                                              Mathf.MoveTowards(transitionScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (transitionScreen.color.a == 1f) {
                isFadeToBlack = false;
            }
        }

        // Fade the transition screen from black
        if (isFadeFromBlack) {
            transitionScreen.color = new Color(transitionScreen.color.r, transitionScreen.color.g, transitionScreen.color.b,
                                              Mathf.MoveTowards(transitionScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (transitionScreen.color.a == 0f) {
                isFadeFromBlack = false;
            }
        }
    }

    // Call when needed to fade to black
    public void fadeToBlack() {
        isFadeToBlack = true;
        isFadeFromBlack = false;
    }

    // Call when needed to fade from black
    public void fadeFromBlack() {
        isFadeFromBlack = true;
        isFadeToBlack = false;
    }
}
