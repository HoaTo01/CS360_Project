using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {
    // Elements
    public static PartyManager selfReference;
    public CharacterStats[] membersStats;

    // Start is called before the first frame update
    void Start() {
        selfReference = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }
}
