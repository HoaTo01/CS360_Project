using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));

        GameManager.selfReference.LoadData();
        QuestManager.selfReference.LoadQuestData();
    }
}
