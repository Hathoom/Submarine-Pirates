using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Room
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
        // alter what needs to be altered
        gameManager.happinessInc(crew * 5);
        Debug.Log(crew);

        base.endTurn();
    }
}
