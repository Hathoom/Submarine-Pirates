using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Bridge : Room
{

    // Start is called before the first frame update
    void Start()
    {
        SetCrewNeeded();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public override void endTurn()
    {
        int level = gameManager.getLevel();
        int damage = gameManager.getDamage();

        // alter what needs to be altered
        int currentCrew = crew * 2 + sickCrew;
        
        int damageChance = (crewNeeded - currentCrew) * 20;

        if (damageChance >= Random.Range(1, 101)) {
            gameManager.damageInc(5);
            gameManager.healthInc(-10);
        }

        base.endTurn();
    }

    //check if you have the needed amount of crew to be safe or go down
    public bool CheckNeededCrew()
    {
        int currentCrew = crew * 2 + sickCrew;

        if (crewNeeded > currentCrew)
        {
            return false;
        }
        return true;
    }

    public void SetCrewNeeded()
    {
        int num = gameManager.getLevel() - 1;
        int damage = gameManager.getDamage();

        //Debug.Log("Damage / 5: " + damage / 5);

        if (2 < damage / 5)
        {
            num += 2;
        }
        else
        {
            num += (damage / 5);
        }

        num = num * 2;

        crewNeeded = num;

        Debug.Log("CrewNeeded: " + crewNeeded);
    }

    public void ActivateEmergencyLift()
    {
        //inform the gameManager to change the depth direction
        gameManager.setEmergencyLift(true);

        //set the depth direction to go up
        gameManager.setDepthDir(-1);
    }
}
