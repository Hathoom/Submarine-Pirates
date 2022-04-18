using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterBossDefend : Encounter
{
    public int weaponsNeeded;

    public int imHullDamage;
    public int imHealthDamage;

    public int hullDamagePC;
    public int healthDamagePC;

    public int additionalPunishmentPC;
    public int additionalPunishmentType;

    //constructor
    public EncounterBossDefend(string encounterName, int weaponsNeeded, int imHullDamage, int imHealthDamage, int hullDamagePC, int healthDamagePC, int additionalPunishmentPC, int additionalPunishmentType) {
        // rewardtype:
        // 0: nothing
        // 1: food
        // 2: fuel
        // 3: gold
        // 4: crew
        // 5: make crew sick
        // 6: happiness
        // 7: hull damage
        // 8: ship health
        this.encounterName = encounterName;
        this.weaponsNeeded = weaponsNeeded;
        this.imHullDamage = imHullDamage;
        this.imHealthDamage = imHealthDamage;
        
        this.hullDamagePC = hullDamagePC;
        this.healthDamagePC = healthDamagePC;

        this.additionalPunishmentPC = additionalPunishmentPC;
        this.additionalPunishmentType = additionalPunishmentType;
    }

    public override void startEncounter() {
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/start");
        textboxTrigger.triggerTextbox();
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //take immediate punishment
        gameManager.damageInc(imHullDamage);
        gameManager.healthInc(-(imHealthDamage));

        // if there is only 1 regular punishment
        // weapons skill check
        if (gameManager.weaponPow >= weaponsNeeded) {
            // Passed Defend
            Debug.Log("Defend Passed");
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/pass");
        } 
        else if (gameManager.weaponPow < weaponsNeeded) {
            gameManager.damageInc((weaponsNeeded - gameManager.weaponPow) * hullDamagePC);
            gameManager.healthInc(-((weaponsNeeded - gameManager.weaponPow) * healthDamagePC));
            if (additionalPunishmentType != 0)
            {
                //number that represents how many do you lose.
                int num = -((weaponsNeeded - gameManager.weaponPow) * additionalPunishmentPC);
                AdditionalPunishment(num, additionalPunishmentType);
            }
            //textboxTrigger.loadTxtFile("encounter_defend_1_fail");
            Debug.Log("Defend Failed Damage taken");
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/fail");
        }
        textboxManager.setPostFunction(gameManager.startTurn);
        textboxTrigger.triggerTextbox();
    }

    public void AdditionalPunishment(int num, int type)
    {
        if (type == 0)
        {
            //nothing happens
        }
        else if (type == 1)
        {
            gameManager.foodInc(num);
        }
        else if (type == 2)
        {
            gameManager.fuelInc(num);
        }
        else if (type == 3)
        {
            gameManager.goldInc(num);
        }
        else if (type == 4)
        {
            gameManager.usableCrewInc(num);
        }
        else if (type == 5)
        {
            if (gameManager.usableCrew == 0)
            {
                Debug.Log("No more crew can get sick");
            }
            else if (gameManager.usableCrew < num)
            {
                num = num - gameManager.usableCrew;
            }
            gameManager.usableCrewInc(-num);
            gameManager.maxSickInc(num);
        }
        else if (type == 6)
        {
            gameManager.happinessInc(num);
        }
        else if (type == 7)
        {
            gameManager.damageInc(num);
        }
        else if (type == 8)
        {
            gameManager.healthInc(num);
        }
        else
        {
            Debug.Log("ERROR WRONG REWARD TYPE ENTERED IN " + encounterName + " encounter!");
        }
    }
}
