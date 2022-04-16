using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public GameObject SelectedObject;

    public GameObject DesinationObject;

    public Camera mainCamera;

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
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // When the user is ready to end their turn, this advances the game
    public void endTurn()
    {
        // When there isn't enough food to feed people
        if (food < crew)
        {
            happinessInc(-10);
        }

        // Crew eats food
        foodInc(-crew);

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
