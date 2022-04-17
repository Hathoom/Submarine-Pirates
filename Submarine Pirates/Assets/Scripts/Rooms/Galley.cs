using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Galley : Room
{
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void endTurn() {
        // alter what needs to be altered
        gameManager.foodInc(crew * 5);
        gameManager.foodInc(sickCrew * 3);
        base.endTurn();
    }
}
