using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterDefend : Encounter
{
    public int weaponsNeeded;
    public int hullDamage;
    public int healthDamage;

    public int additionalPunishment;
    public int additionalPunishmentType;

    public bool worsePunishment = false;
    public int worseWeaponsNeeded;
    public int worseHDamage;
    public int worseHealth;

    //constructor
    public EncounterDefend(string encounterName, int weaponsNeeded, int hullDamage, 
                            int healthDamage, int additionalPunishment, 
                            int additionalPunishmentType) {
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
        this.weaponsNeeded = weaponsNeeded;
        this.hullDamage = hullDamage;
        this.healthDamage = healthDamage;
        
        this.additionalPunishment = additionalPunishment;
        this.additionalPunishmentType = additionalPunishmentType;
    }

        // 2 potential punishments constructor
        public EncounterDefend(string encounterName, int weaponsNeeded, 
                                int hullDamage, int healthDamage, 
                                int additionalPunishment, int additionalPunishmentType, 
                                int worseWeaponsNeeded, int worseHDamage, 
                                int worseHealth) {
        
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
        this.weaponsNeeded = weaponsNeeded;
        this.hullDamage = hullDamage;
        this.healthDamage = healthDamage;

        this.worsePunishment = true;
        this.worseWeaponsNeeded = worseWeaponsNeeded;
        this.worseHDamage = worseHDamage;
        this.worseHealth = worseHealth;

        this.additionalPunishment = additionalPunishment;
        this.additionalPunishmentType = additionalPunishmentType;
    }

    // Runs the script for the start of the encounter
    public override void startEncounter() {
        Debug.Log("Defend encounter started");
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/start");
        textboxTrigger.triggerTextbox();
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // if there is only 1 regular punishment
        if (worseWeaponsNeeded == 0)
        {
            // weapons skill check
            if (gameManager.weaponPow >= weaponsNeeded) {
                // Passed Defend
                textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/pass");
            } 
            else if (gameManager.weaponPow < weaponsNeeded) {
                gameManager.damageInc(hullDamage);
                gameManager.healthInc(-(healthDamage));
                if (additionalPunishmentType != 0)
                {
                    AdditionalPunishment(additionalPunishment, additionalPunishmentType);
                }
                //textboxTrigger.loadTxtFile("encounter_defend_1_fail");
                textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/fail");
            }
            textboxManager.setPostFunction(gameManager.startTurn);
            textboxTrigger.triggerTextbox();
        }
        // multiple levels of punishment
        else
        {
            // weapons skill check
            if (gameManager.weaponPow >= weaponsNeeded) {
                // Passed Defend
                textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/pass");
            } 
            // first tier failed
            else if (gameManager.weaponPow < weaponsNeeded && gameManager.weaponPow >= worseWeaponsNeeded) {
                gameManager.damageInc(hullDamage);
                gameManager.healthInc(-(healthDamage));
                
                textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/fail");
            }
            else if (gameManager.weaponPow < worseWeaponsNeeded)
            {
                gameManager.damageInc(worseHDamage);
                gameManager.healthInc(-(worseHealth));
                if (additionalPunishmentType != 0)
                {
                    AdditionalPunishment(additionalPunishment, additionalPunishmentType);
                }
                textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/fail");
            }
            textboxManager.setPostFunction(gameManager.startTurn);
            textboxTrigger.triggerTextbox();
        }
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
