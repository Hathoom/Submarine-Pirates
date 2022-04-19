using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterHappiness : Encounter
{

    public int failPenalty;
    public int successReward;
    public int exceptionalReward;

    
    public int failPenaltyType;
    public int successRewardType;
    public int exceptionalRewardType;

    public int additionalfailPenalty;
    public int additionalfailPenaltyType;

    public int additionalSuccessReward;
    public int additionalSuccessRewardType;

    public int additionalExceptionalReward;
    public int additionalExceptionalRewardType;

    //constructor
    public EncounterHappiness(string encounterName, int failPenalty, 
                                int failPenaltyType, int additionalfailPenalty,
                                int additionalfailPenaltyType, int successReward, 
                                int successRewardType, int additionalSuccessReward,
                                int additionalSuccessRewardType, int exceptionalReward, 
                                int exceptionalRewardType, int additionalExceptionalReward,
                                int additionalExceptionalRewardType) 
    {
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
        
        this.failPenalty = failPenalty;
        this.failPenaltyType = failPenaltyType;

        this.successReward = successReward;
        this.successRewardType = successRewardType;

        this.exceptionalReward = exceptionalReward;
        this.exceptionalRewardType = exceptionalRewardType;

        this.additionalfailPenalty = additionalfailPenalty;
        this.additionalfailPenaltyType = additionalfailPenaltyType;

        this.additionalSuccessReward = additionalSuccessReward;
        this.additionalSuccessRewardType = additionalSuccessRewardType;

        this.additionalExceptionalReward = additionalExceptionalReward;
        this.additionalExceptionalRewardType = additionalExceptionalRewardType;
    }

    public override void startEncounter() {
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/start");
        textboxTrigger.triggerTextbox();
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Happiness skill test
        int happiness = gameManager.happiness;

        // fail test
        if(happiness < 50)
        {
            AlterValue(failPenalty, failPenaltyType);
            AlterValue(additionalfailPenalty, additionalfailPenaltyType);
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/fail");
        }
        // pass test
        else if (happiness >= 50 && happiness <= 75)
        {
            AlterValue(successReward, successRewardType);
            AlterValue(additionalSuccessRewardType, additionalSuccessRewardType);
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/pass");
        }
        // exceptional
        else if (happiness > 75)
        {
            AlterValue(exceptionalReward, exceptionalRewardType);
            AlterValue(additionalExceptionalReward, additionalExceptionalRewardType);
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/extra");
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
