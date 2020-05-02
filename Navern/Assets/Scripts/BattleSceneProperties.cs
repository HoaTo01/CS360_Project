using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is just a class to store each battle scene's properties (enemies in the scene, 
 * what kind of items it can drop,... */
[System.Serializable]
public class BattleSceneProperties {
    // Elements
    public string[] enemies;
    public int expGained;
    public string[] rewardItems;
}
