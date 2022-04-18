using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public GameManager gameManager;
    public Encounter encounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateEncounter() {
        Debug.Log("generateEncounter");
        int level = gameManager.level;

        switch (level) {
            case 0: // Surface
                encounter = new EncounterShop(level);
                break;
            
            case 1: // Shallows
                Encounter[] encounters = {new EncounterShop(0), new EncounterDefend("Test Level 1 Defend", 1, 3, 10)};
                int encounterNum = Random.Range(0, encounters.Length);
                encounter = encounters[encounterNum];
                break;
        }

        encounter.startEncounter();
    }
}
