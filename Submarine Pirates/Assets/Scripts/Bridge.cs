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

        int crewNeeded =  level - 1;

        if (2 < damage / 5)
        {
            crewNeeded += 2;
        }
        else
        {
            crewNeeded += (damage / 5);
        }
        
        int damageChance = (crewNeeded - crew) * 20;

        if (damageChance >= Random.Range(0, 100)) {
            gameManager.damageInc(5);
            gameManager.healthInc(-10);
        }

        base.endTurn();
    }
}
