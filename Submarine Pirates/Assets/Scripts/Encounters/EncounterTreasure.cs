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
        // 7: hull damage
        // 8: ship health
        this.encounterName = encounterName;
        this.reward = reward;
        this.rewardType = rewardType;
    }

    public override void startEncounter() {
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/start");
        textboxTrigger.triggerTextbox();
    }

    public override void executeEncounter() {
        Debug.Log("Treasure Encounte Executed");
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        AlterValue(reward, rewardType);

        // If the file for end doesnt exist, it just does startTurn
        if (System.IO.File.Exists("Encounter/" + encounterName + "/end")) {
            textboxManager.setPostFunction(gameManager.startTurn);
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/end");
            textboxTrigger.triggerTextbox();
        } else {
            gameManager.startTurn();
        }
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
