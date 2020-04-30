using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour {
    // Elements
    public float effectLength;
    public int sfx;

    // Start is called before the first frame update
    void Start() {
        // Play the SFX.
        AudioManager.selfReference.PlaySFX(sfx);
    }

    // Update is called once per frame
    void Update() {
        // Destroy an object after an amount of time.
        Destroy(gameObject, effectLength);
    }
}
