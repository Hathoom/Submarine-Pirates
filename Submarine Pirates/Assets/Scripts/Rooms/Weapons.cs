using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Room
{
    public int weaponPow = 0;

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
        weaponPow = weaponPow + (crew * 2);
        if (sickCrew > 0)
        {
            for (int i = 0; i < crew;  i++)
            {
                if (Random.Range(0, 101) >= 50)
                {
                    weaponPow++;
                }
            }

            gameManager.weaponPowInc(weaponPow);
        }
        
        base.endTurn();
    }
}
