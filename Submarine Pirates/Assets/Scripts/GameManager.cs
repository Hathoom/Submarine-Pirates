using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Resources
    public int happiness;
    public int health;
    public int food;
    public int fuel;
    public int crew;
    public int crewSick;
    public int damage;
    public int depth;
    public int depthChange;

    // Crewmates assigned
    public int crewBridge;
    public int crewGalley;
    public int crewEngine;
    public int crewMedbay;
    public int crewMaintenance;
    public int crewWeapons;
    public int crewBarracks;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // When the user is ready to end their turn, this advances the game
    public void endTurn()
    {
        // CREWMATES DO WORK

        // Cooks in the galley
        foodInc(crewGalley * 5);

        // Moving down
        depthInc(depthChange);

        // Ship is repaired
        damageInc(-crewMaintenance);

        // SHIP CALCULATES VALUES

        // When there isn't enough food to feed people
        if (food < crew)
        {
            happinessInc(-10);
        }

        // Bridge
        if (crewBridge < 1)
        {
            // Take damage
        }

        // Engine
        if (crewEngine < 1)
        {
            damageInc(5);
        }

        // MedBay
        crewSickInc(-crewMedbay);

        // Crew eats food
        foodInc(-crew);
        // Fuel is used
        fuelInc(-1);
        // Decreases health based on damage
        healthInc(-damage);

        // Ship barracks raises happiness
        happinessInc(crewBarracks * 5);

        // DEATH STATES

        if (happiness <= 0)
        {
            // Crew Mutinies! Game over.
        }

        if (health <= 0)
        {
            // Ship is destroyed
        }

        if (crew <= 0)
        {
            // All your crew is dead, you are stuck in the ocean
        }
    }


    public void happinessInc(int amount)
    {
        happiness += amount;
        if (happiness < 0) happiness = 0;
    }

    public void healthInc(int amount)
    {
        health += amount;
        if (health < 0) health = 0;
    }

    public void foodInc(int amount)
    {
        food += amount;
        if (food < 0) food = 0;
    }

    public void fuelInc(int amount)
    {
        fuel += amount;
        if (fuel < 0) fuel = 0;
    }

    public void crewInc(int amount)
    {
        crew += amount;
        if (crew < 0) crew = 0;
    }

    public void crewSickInc(int amount)
    {
        crewSick += amount;
        if (crewSick < 0) crewSick = 0;
    }

    public void damageInc(int amount)
    {
        damage += amount;
        if (damage < 0) damage = 0;
    }

    public void depthInc(int amount)
    {
        depth += amount;
        if (depth < 0) depth = 0;
    }
}
