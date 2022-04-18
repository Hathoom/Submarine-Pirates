using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTreasure : Encounter
{
    
    public string encounterName;
    public int reward;

    public int rewardType;


    //constructor
    public EncounterTreasure(string encounterName, int reward, int rewardType) {
        
        // rewardtype:
        // 0: nothing
        // 1: food
        // 2: fuel
        // 3: gold
        // 4: crew
        // 5: make crew sick
        // 6: happiness
        this.encounterName = encounterName;
        this.reward = reward;
        this.reward = rewardType;
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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

        gameManager.startTurn();
    }
}
