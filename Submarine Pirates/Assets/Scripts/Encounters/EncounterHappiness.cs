using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterHappiness : Encounter
{
    public string encounterName;

    public int failPenalty;
    public int successReward;
    public int exceptionalReward;

    
    public int failPenaltyType;
    public int successRewardType;
    public int exceptionalRewardType;

    //constructor
    public EncounterHappiness(string encounterName, int failPenalty, int failPenaltyType, int successReward, int successRewardType,
                                                int exceptionalReward, int exceptionalRewardType) 
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
            if (failPenaltyType == 0)
            {
                //nothing happens
            }
            else if (failPenaltyType == 1)
            {
                gameManager.foodInc(failPenalty);
            }
            else if (failPenaltyType == 2)
            {
                gameManager.fuelInc(failPenalty);
            }
            else if (failPenaltyType == 3)
            {
                gameManager.goldInc(failPenalty);
            }
            else if (failPenaltyType == 4)
            {
                gameManager.usableCrewInc(failPenalty);
            }
            else if (failPenaltyType == 5)
            {
                if (gameManager.usableCrew == 0)
                {
                    Debug.Log("No more crew can get sick");
                }
                else if (gameManager.usableCrew < failPenalty)
                {
                    failPenalty = failPenalty - gameManager.usableCrew;
                }
                gameManager.usableCrewInc(-failPenalty);
                gameManager.maxSickInc(failPenalty);
            }
            else if (failPenaltyType == 6)
            {
                gameManager.happinessInc(failPenalty);
            }
            else if (failPenaltyType == 7)
            {
                gameManager.damageInc(failPenalty);
            }
            else if (failPenaltyType == 8)
            {
                gameManager.healthInc(failPenalty);
            }
            else
            {
                Debug.Log("ERROR WRONG REWARD TYPE ENTERED IN " + encounterName + " encounter!");
            }
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/fail");
        }
        // pass test
        else if (happiness >= 50 && happiness <= 75)
        {
            if (successRewardType == 0)
            {
                //nothing happens
            }
            else if (successRewardType == 1)
            {
                gameManager.foodInc(successReward);
            }
            else if (successRewardType == 2)
            {
                gameManager.fuelInc(successReward);
            }
            else if (successRewardType == 3)
            {
                gameManager.goldInc(successReward);
            }
            else if (successRewardType == 4)
            {
                gameManager.usableCrewInc(successReward);
            }
            else if (successRewardType == 5)
            {
                if (gameManager.usableCrew == 0)
                {
                    Debug.Log("No more crew can get sick");
                }
                else if (gameManager.usableCrew < successReward)
                {
                    successReward = successReward - gameManager.usableCrew;
                }
                gameManager.usableCrewInc(-successReward);
                gameManager.maxSickInc(successReward);
            }
            else if (successRewardType == 6)
            {
                gameManager.happinessInc(successReward);
            }
            else if (successRewardType == 7)
            {
                gameManager.damageInc(successReward);
            }
            else if (successRewardType == 8)
            {
                gameManager.healthInc(successReward);
            }
            else
            {
                Debug.Log("ERROR WRONG REWARD TYPE ENTERED IN " + encounterName + " encounter!");
            }
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/pass");
        }
        // exceptional
        else if (happiness > 75)
        {
            if (exceptionalRewardType == 0)
            {
                //nothing happens
            }
            else if (exceptionalRewardType == 1)
            {
                gameManager.foodInc(exceptionalReward);
            }
            else if (exceptionalRewardType == 2)
            {
                gameManager.fuelInc(exceptionalReward);
            }
            else if (exceptionalRewardType == 3)
            {
                gameManager.goldInc(exceptionalReward);
            }
            else if (exceptionalRewardType == 4)
            {
                gameManager.usableCrewInc(exceptionalReward);
            }
            else if (exceptionalRewardType == 5)
            {
                if (gameManager.usableCrew == 0)
                {
                    Debug.Log("No more crew can get sick");
                }
                else if (gameManager.usableCrew < exceptionalReward)
                {
                    exceptionalReward = exceptionalReward - gameManager.usableCrew;
                }
                gameManager.usableCrewInc(-exceptionalReward);
                gameManager.maxSickInc(exceptionalReward);
            }
            else if (exceptionalRewardType == 6)
            {
                gameManager.happinessInc(exceptionalReward);
            }
            else if (exceptionalRewardType == 7)
            {
                gameManager.damageInc(exceptionalReward);
            }
            else if (exceptionalRewardType == 8)
            {
                gameManager.healthInc(exceptionalReward);
            }
            else
            {
                Debug.Log("ERROR WRONG REWARD TYPE ENTERED IN " + encounterName + " encounter!");
            }
            textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/extra");
        }

        textboxManager.setPostFunction(gameManager.startTurn);
        textboxTrigger.triggerTextbox();
    }
}
