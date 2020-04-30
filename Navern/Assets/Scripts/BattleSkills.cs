using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/* This is just a class to store skills data */
public class BattleSkills {
    public string skillName;

    public double damagePower;
    public int manaCost;

    public bool isAHealMove;

    public AttackEffect visualEffect;
}
