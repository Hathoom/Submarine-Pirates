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

    public int damagePerCrew;
    public int healthPerCrew;

    //constructor
    public EncounterRival(string encounterName, int weaponsDefend, int weaponsAttack, int reward, int rewardType, int damagePerCrew, int healthPerCrew) {
        
        // rewardtype:
        // 0: nothing
        // 1: food
        // 2: fuel
        // 3: gold
        // 4: crew
        // 5: make crew sick
        // 6: happiness
        this.encounterName = encounterName;
        this.weaponsDefend = weaponsDefend;
        this.weaponsAttack = weaponsAttack;
        this.reward = reward;
        this.reward = rewardType;

        this.damagePerCrew = damagePerCrew;
        this.healthPerCrew = healthPerCrew;
    }


    // Runs the script for the start of the encounter
    public override void startEncounter() {
        Debug.Log("Attack encounter started");
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/start");
        textboxTrigger.triggerTextbox();
    }
    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // weapons skill check
        // pass the attack check
        if (gameManager.weaponPow >= weaponsAttack) {
            if (rewardType == 0)
            {
                
            }
            if (rewardType == 1)
            {
                gameManager.foodInc(reward);
            }
            else if (rewardType == 2)
            {
                gameManager.fuelInc(reward);
            }
            else if (rewardType == 3)
            {
                gameManager.goldInc(reward);
            }
            else if (rewardType == 4)
            {
                if (gameManager.maxSick + gameManager.usableCrew > gameManager.maxCrew)
                {
                    Debug.Log("You are at max crew: no reward");
                }
                else
                {
                    gameManager.usableCrewInc(reward);
                }
            }
            else if (rewardType == 5)
            {
                if (gameManager.usableCrew == 0)
                {
                    Debug.Log("No more crew can get sick");
                }
                else if (gameManager.usableCrew < reward)
                {
                    reward = reward - gameManager.usableCrew;
                }
                gameManager.usableCrewInc(-reward);
                gameManager.maxSickInc(reward);
            }
            else if (rewardType == 6)
            {
                gameManager.happinessInc(reward);
            }
            else
            {
                Debug.Log("ERROR WRONG REWARD TYPE ENTERED IN " + encounterName + " encounter!");
            }
        }
        // succeed in defense
        else if (gameManager.weaponPow <= weaponsAttack && gameManager.weaponPow >= weaponsDefend) {
            Debug.Log("Successful rival Defend");
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/pass");
        }
        else if (gameManager.weaponPow <= weaponsDefend) {
            gameManager.damageInc((weaponsDefend - gameManager.weaponPow) * damagePerCrew);
            gameManager.healthInc(-(weaponsDefend - gameManager.weaponPow) * healthPerCrew);
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/fail");
        }

        textboxManager.setPostFunction(gameManager.startTurn);
        textboxTrigger.triggerTextbox();
    }
}
