using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterAccident : Encounter
{
    
    public string encounterName;
    public int hullDamage;

    public int healthDamage;


    //constructor
    public EncounterAccident(string encounterName, int hullDamage, int healthDamage) {
        
        // make sure hulldamage is positive, and healthdamage is negative
        this.encounterName = encounterName;
        this.hullDamage = hullDamage;
        this.healthDamage = healthDamage;
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.damageInc(hullDamage);
        gameManager.healthInc(healthDamage);

        gameManager.startTurn();
    }
}
