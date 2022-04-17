using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public int crew;
    public int crewSick;
    public int damage;
    public int depth;
    public int depthChange;

    public GameObject SelectedObject;

    public GameObject DesinationObject;

    public Camera mainCamera;

    // Crewmates assigned
    public Bridge bridge;
    public Galley galley;
    public Engine engine;
    public Medbay medbay;
    public Maintenance maintenance;
    public Weapons weapons;
    public Barracks barracks;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    // When the user is ready to end their turn, this advances the game
    public void endTurn()
    {
        gamestate = "work";
        // CREWMATES DO WORK

        // Cooks in the galley
        foodInc(galley.crew * 5);

        // Moving down
        depthInc(depthChange);

        // Ship is repaired
        damageInc(-maintenance.crew);

        // SHIP CALCULATES VALUES

        // When there isn't enough food to feed people
        if (food < crew)
        {
            happinessInc(-10);
        }

        // Bridge
        if (bridge.crew < 1)
        {
            // Take damage
        }

        // Engine
        if (engine.crew < 1)
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
        }
    }*/

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

    // old code delete if we don't need it anymore
    // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //     // 2D object
        //     RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        //     if (hit2D.collider != null &&(hit2D.collider.gameObject.CompareTag("Storage") || hit2D.collider.
        //             gameObject.layer == LayerMask.NameToLayer("Storage"))) 
        //     {
        //         if (SelectedObject == null)
        //         {
        //             SelectedObject = hit2D.collider.gameObject;
        //             // if we select CrewBase, we'll want to send CrewMembers elsewhere
        //             if(SelectedObject.name == "CrewBase" && crew != 0)
        //             {
        //                 SelectedObject = hit2D.collider.gameObject;
        //             }
        //             else
        //             {
        //                 SelectedObject = null;
        //             }
        //         }
        //         else
        //         {
        //             // get the 2nd object selected
        //             DesinationObject = hit2D.collider.gameObject;
                    
        //             //send crew member from location to location
        //             // Do this if CrewBase is selected
        //             if (SelectedObject.name == "CrewBase")
        //             {
        //                 // move crew from the Base
        //                 MoveCrewfromBase();
        //             }
        //         }
        //     }
        //     //remove selection
        //     else
        //     {
        //         if (SelectedObject != null)
        //         {
        //             SelectedObject = null;
        //         }
        //     }
        // }
}
