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
        this.encounterName = encounterName;
        this.weaponsNeeded = weaponsNeeded;
        this.reward = reward;
        this.reward = rewardType;
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // weapons skill check
        // pass the weapons skill check
        if (gameManager.weaponPow == weaponsNeeded) {
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
            else
            {
                Debug.Log("ERROR WRONG REWARD TYPE ENTERED IN " + encounterName + " encounter!");
            }
        }
        //fail the weapons skill check 
        else if (gameManager.weaponPow < weaponsNeeded) {
            //No punishment
            Debug.Log("Attack Failure text");
        }

        gameManager.startTurn();
    }
}
