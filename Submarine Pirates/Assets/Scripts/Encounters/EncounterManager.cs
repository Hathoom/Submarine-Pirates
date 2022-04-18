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
                encounter = new EncounterShop("Surface_Shop", level);
                break;
            
            case 1: // Shallows
                Encounter[] encounters = { 
                    new EncounterAttack("Fishing_Boat", 1, 10, 1), new EncounterDefend("Octopus", 1, 2, 5, 0, 0), 
                    new EncounterTreasure("Fancy_Lure", 100, 3), 
                    new EncounterHappiness("Feast", 0, 0, 0, 0, 5, 1, 0, 0, 10, 1, 0, 0), 
                    new EncounterTreasure("Shallow_Catch_Fish", 5, 1), new EncounterTreasure("Shallow_Catch_Fish", 5, 1), 
                    new EncounterTreasure("Shallow_Good_Omen", 10, 6), new EncounterShop("Merchant_Shop", level)};
                int encounterNum = Random.Range(0, encounters.Length);
                encounter = encounters[encounterNum];
                break;
            case 2: // deep sea
                Encounter[] deepEncounters = {new EncounterAttack("Deep_Sea_Passenger_Ship", 1, 200, 3), new EncounterDefend("Shark", 1, 10, 2, 0, 0),
                    new EncounterRival("Rival_Pirates", 1, 2, 300, 3, 15, 3, 0 , 0), new EncounterTreasure("Falling_Anchor", 250, 3),
                    new EncounterTreasure("Big_Treasure", 500, 3), new EncounterTreasure("Deep_Sea_Catch_Fish", 15, 1),
                    new EncounterHappiness("Maintainance", 3, 7, 0, 0, 0, 0, 0, 0, 20, 8, 0, 0), new EncounterTreasure("Deep_Sea_Food_Poisoning", 2, 5),
                    new EncounterTreasure("Deep_Sea_Find_Rum", 20, 6), new EncounterShop("Smuggler_Shop", level), new EncounterHappiness("Maintainance", 3, 7, 0, 0, 0, 0, 0, 0, 20, 8, 0, 0),
                    new EncounterTreasure("Deep_Sea_Catch_Fish", 15, 1), new EncounterShop("Smuggler_Shop", level)};
                encounterNum = Random.Range(0, deepEncounters.Length);
                encounter = deepEncounters[encounterNum];
                break;
            case 3: // the Abyss
                Encounter[] abyssEncounters = {new EncounterAttack("Abyss_Trading_Ship", 2, 600, 3), new EncounterDefend("Navel_Vessel", 2, 3, 15, -1, 4, 1, 5, 20),
                new EncounterRival("Abyss_Bounty_Hunter", 2, 2, 800, 3, 3, 20, -1, 4, 1, 5, 20), new EncounterTreasure("Abyss_Small_Treasure", 700, 3),
                new EncounterTreasure("Abyss_Large_Treasure", 1000, 3), 
                new EncounterHappiness("Staying_In_Shape", 3, 5, 0, 0, -16, 0, 0, 0, -16, 0, 20, 1),
                new EncounterHappiness("Staying_In_Shape", 3, 5, 0, 0, -16, 0, 0, 0, -16, 0, 20, 1), 
                new EncounterTreasure("Abyss_Food_Poisoning", 3, 5), new EncounterTreasure("Abyss_Food_Goes_Bad", 10, 1), 
                new EncounterAccident("Abyss_Accident", 5, 10), 
                new EncounterTreasure("Abyss_Rescue_Crewmate", 1, 4), 
                new EncounterShop("Scrapper_Shop", level), new EncounterShop("Scrapper_Shop", level)};
                encounterNum = Random.Range(0, abyssEncounters.Length);
                encounter = abyssEncounters[encounterNum];
                break;
            case 4: // Into the Trench
                Encounter[] intotheTrenchEncounters = {new EncounterAttack("Trench_Treasure_Hunters", 2, 1000, 3),
                    new EncounterDefend("Trench_Ghost_Ship", 2, 20, 5, -1, 4, 1, 6, 20), new EncounterRival("Trench_BlackBeard", 2, 3, 1500, 3, 3, 15, -1, 4, 1, 5, 25), 
                    new EncounterBossDefend("Trench_Sea_Monster", 3, 5, 15, 1, 10, 1, 4), new EncounterTreasure("Trench_Lost_Medallion", 1000, 3),
                    new EncounterTreasure("Trench_Lost_Medallion", 1000, 3), new EncounterTreasure("Trench_Sunken_Treasure", 1500, 3),
                    new EncounterTreasure("Trench_Shipwreck", 2000, 3), 
                    new EncounterHappiness("Crew_Camaraderie", -1, 4, 1, 5, 15, 1, 10, 6, 2000, 3, 0, 0),
                    new EncounterHappiness("Crew_Camaraderie", -1, 4, 1, 5, 15, 1, 10, 6, 2000, 3, 0, 0), 
                    new EncounterAccident("Trench_Hit_a_Mine", 5, 15),
                    new EncounterTreasure("Trench_Scurvy", 3, 5),
                    new EncounterShop("Jones_Shop", level)};
                encounterNum = Random.Range(0, intotheTrenchEncounters.Length);
                encounter = intotheTrenchEncounters[encounterNum];
                break;
        }

        encounter.startEncounter();
    }
}
