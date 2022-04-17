using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Medbay : Room
{
    //public int sickCrew;

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
        // alter what needs to be altered
        for (int i = 0; i < crew;  i++)
        {
            if (sickCrew > 0)
            {
                if (Random.Range(0, 101) >= 50)
                {
                    sickCrew = sickCrew - 1;
                    gameManager.maxCrewInc(1);
                    gameManager.maxSickInc(-1);
                }
            }
            else
            {
                break;
            }
        }

        base.endTurn();
    }
}
