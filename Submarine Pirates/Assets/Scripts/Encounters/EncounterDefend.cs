using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterDefend : Encounter
{
    public int weaponsNeeded;
    public int damagePerCrew;
    public int healthPerCrew;

    //constructor
    public EncounterDefend(string encounterName, int weaponsNeeded, int damagePerCrew, int healthPerCrew) {
        
        // 0 - Surface Merchant
        // 1 - Fishing Vessal
        // 2 - ??
        // 3 - Scrappers
        // 4 - Davey Jones
        this.weaponsNeeded = weaponsNeeded;
        this.damagePerCrew = damagePerCrew;
        this.healthPerCrew = healthPerCrew;
    }

    // Runs the script for the start of the encounter
    public override void startEncounter() {
        Debug.Log("Defend encounter started");
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("encounter_defend_1_start");
        textboxTrigger.triggerTextbox();
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // weapons skill check
        if (gameManager.weaponPow >= weaponsNeeded) {
            textboxTrigger.loadTxtFile("encounter_defend_1_pass");
        } else if (gameManager.weaponPow < weaponsNeeded) {
            gameManager.damageInc((weaponsNeeded - gameManager.weaponPow) * damagePerCrew);
            gameManager.healthInc(-(weaponsNeeded - gameManager.weaponPow) * healthPerCrew);
            textboxTrigger.loadTxtFile("encounter_defend_1_fail");
        }

        textboxManager.setPostFunction(gameManager.startTurn);
        textboxTrigger.triggerTextbox();
    }
}
