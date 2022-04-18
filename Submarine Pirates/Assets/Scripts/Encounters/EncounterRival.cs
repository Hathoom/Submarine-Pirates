using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterRival : Encounter
{
    public string encounterName;
    public int weaponsDefend;
    public int weaponsAttack;

    public int reward;
    public int rewardType;

    public int hullDamage;
    public int healthDamage;

    public int additionalPunishment;
    public int additionalPunishmentType;

    public bool worsePunishment = false;
    public int worseWeaponsNeeded;
    public int worseHDamage;
    public int worseHealth;

    //constructor
    public EncounterRival(string encounterName, int weaponsDefend, int weaponsAttack, 
                            int reward, int rewardType, int hullDamage, 
                            int healthDamage, 
                            int additionalPunishment, int additionalPunishmentType) {
        
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
        this.weaponsDefend = weaponsDefend;
        this.weaponsAttack = weaponsAttack;
        this.reward = reward;
        this.reward = rewardType;

        this.hullDamage = hullDamage;
        this.healthDamage= healthDamage;
    }

            // 2 potential punishments constructor
        public EncounterRival(string encounterName, int weaponsDefend, int weaponsAttack,
                                int reward, int rewardType, int hullDamage, 
                                int healthDamage, int additionalPunishment, 
                                int additionalPunishmentType, 
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
        this.weaponsAttack = weaponsAttack;
        this.weaponsDefend = weaponsDefend;

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
        Debug.Log("Attack encounter started");
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("attack1");
        textboxTrigger.triggerTextbox();
    }
    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // simple take one punishment
        if (worseWeaponsNeeded == 0)
        {
            // weapons skill check
            // pass the attack check
            if (gameManager.weaponPow >= weaponsAttack) {
                AlterOtherValue(reward, rewardType);
            }
            // succeed in defense
            else if (gameManager.weaponPow <= weaponsAttack && gameManager.weaponPow >= weaponsDefend) {
                Debug.Log("Successful rival Defend");
            }
            else if (gameManager.weaponPow <= weaponsDefend) {
                gameManager.damageInc(hullDamage);
                gameManager.healthInc(-(healthDamage));
                if (additionalPunishmentType != 0)
                {
                    AlterOtherValue(additionalPunishment, additionalPunishmentType);
                }
                //textboxTrigger.loadTxtFile("encounter_defend_1_fail");
                Debug.Log("Defend Failed Damage taken");
            }
        }
        else
        {
            // succeed in defense
            if (gameManager.weaponPow >= weaponsAttack) {
                AlterOtherValue(reward, rewardType);
            }
            // succeed in defense
            else if (gameManager.weaponPow <= weaponsAttack && gameManager.weaponPow >= weaponsDefend) {
                Debug.Log("Successful rival Defend");
            }
            // partial defense
            else if (gameManager.weaponPow <= weaponsDefend && gameManager.weaponPow >= worseWeaponsNeeded)
            {
                gameManager.damageInc(hullDamage);
                gameManager.healthInc(-(healthDamage));
                //textboxTrigger.loadTxtFile("encounter_defend_1_fail");
                Debug.Log("Defend partially failed");
            }
            else if (gameManager.weaponPow <= worseWeaponsNeeded) {
                gameManager.damageInc(worseHDamage);
                gameManager.healthInc(-(worseHealth));
                if (additionalPunishmentType != 0)
                {
                    AlterOtherValue(additionalPunishment, additionalPunishmentType);
                }
                Debug.Log("Defense lost bad");
            }
        }

        gameManager.startTurn();
    }

    public void AlterOtherValue(int num, int type)
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
