using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterAttack : Encounter
{
    public string encounterName;
    public int weaponsNeeded;

    public int reward;

    public int rewardType;

    //constructor
    public EncounterAttack(string encounterName, int weaponsNeeded, int reward, int rewardType) {
        
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
        this.reward = reward;
        this.reward = rewardType;
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
        // pass the weapons skill check
        if (gameManager.weaponPow >= weaponsNeeded) {
            AlterValue(reward, rewardType);
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/pass");
        }
        //fail the weapons skill check 
        else if (gameManager.weaponPow < weaponsNeeded) {
            //No punishment
            Debug.Log("Attack Failure text");
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/fail");
        }

        textboxManager.setPostFunction(gameManager.startTurn);
        textboxTrigger.triggerTextbox();
    }

    public void AlterValue(int num, int type)
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
            // add crew
            if (num > 0)
            {
                if (gameManager.maxSick + gameManager.usableCrew > gameManager.maxCrew)
                {
                    Debug.Log("You are at max crew: no reward");
                }
                else
                {
                    gameManager.usableCrewInc(num);
                }
            }
            // kill crew
            else if (num < 0)
            {
                for (int i = 0; i > num; i = i - 1)
                {
                    gameManager.killCrewMember(0);
                }
            }
        }
        else if (type == 5)
        {
            // no crew to make sick
            if (gameManager.usableCrew == 0)
            {
                Debug.Log("No more crew can get sick");
            }
            // not enough crew to make sick
            else if (gameManager.usableCrew < num)
            {
                num = num - gameManager.usableCrew;
            }
            // reduce usuable crew and make crew members sick
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
