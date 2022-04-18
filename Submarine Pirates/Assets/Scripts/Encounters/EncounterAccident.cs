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

    public override void startEncounter() {
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/start");
        textboxTrigger.triggerTextbox();
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.damageInc(hullDamage);
        gameManager.healthInc(healthDamage);

        textboxManager.setPostFunction(gameManager.startTurn);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/end");
        textboxTrigger.triggerTextbox();
    }
}
