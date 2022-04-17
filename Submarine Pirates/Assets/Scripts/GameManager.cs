using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    /// Gamestates:
    /// crew_assign
    /// work
    /// encounter
    public string gamestate;

    // Resources
    public int happiness;
    public int health;
    public int food;
    public int fuel;
    public int maxCrew;
    public int crew;
    public int maxSick;
    public int crewSick;
    public int damage;
    public int depth;
    public int depthChange;

    public int level;

    public Camera mainCamera;

    // UI
    public TextMeshProUGUI foodTxt;
    public TextMeshProUGUI damageTxt;
    public TextMeshProUGUI fuelTxt;
    public Slider happinessSlider;
    public Slider healthSlider;
    public TextMeshProUGUI crewCount;

    // Crewmates assigned
    public Bridge bridge;
    public Galley galley;
    public Engine engine;
    public Medbay medbay;
    public Maintenance maintenance;
    public Weapons weapons;
    public Barracks barracks;

    private GameObject[] objectsOnField;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            crewReset();
        }

        foodTxt.text = "Food: " + food;
        damageTxt.text = "Hull Damage: " + damage;
        fuelTxt.text = "Fuel: " + fuel;
        happinessSlider.value = happiness;
        healthSlider.value = health;
        crewCount.text = "x" + crew;
    }
    public void crewReset()
    {
        //get all crew not assigned on the map
        objectsOnField = GameObject.FindGameObjectsWithTag("Draggable");

        foreach(GameObject GObject in objectsOnField)
        {
            Destroy(GObject);
        }

        objectsOnField = null;

        barracks.RemoveAllCrew();
        bridge.RemoveAllCrew();
        engine.RemoveAllCrew();
        galley.RemoveAllCrew();
        maintenance.RemoveAllCrew();
        medbay.RemoveAllCrew();
        weapons.RemoveAllCrew();
        

        crew = maxCrew;
        crewSick = maxSick;
    }

    
    // When the user is ready to end their turn, this advances the game
    public void endTurn()
    {
        gamestate = "work";
        // CREWMATES DO WORK

        barracks.endTurn();
        bridge.endTurn();
        engine.endTurn();
        galley.endTurn();
        maintenance.endTurn();
        medbay.endTurn();
        weapons.endTurn();

        crewReset();

        
        /*
        // Crew eats food
        foodInc(-crew);
        // Fuel is used
        fuelInc(-1);
        // Decreases health based on damage
        healthInc(-damage);

        // Ship barracks raises happiness
        happinessInc(barracks.crew * 5);

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
        }*/
    }

    // Decides what the encounter is
    public void nextEncounter() 
    {
        gamestate = "encounter";
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

    public void maxCrewInc(int amount)
    {
        maxCrew += amount;
        if (maxCrew < 0) maxCrew = 0;
    }

    public void maxSickInc(int amount)
    {
        maxSick += amount;
        if (maxSick < 0) maxSick = 0;
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

        if (depth >= 10)
        {
            level = 2;
        }
        else if (depth >= 20)
        {
            level = 3;
        }
        else if (depth >= 30)
        {
            level = 4;
        }
    }

    // gamemanger getters

    public int getLevel()
    {
        return level;
    }

    public int getDamage()
    {
        return damage;
    }
}
