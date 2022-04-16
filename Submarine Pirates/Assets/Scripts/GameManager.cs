using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int happiness;
    public int health;
    public int food;
    public int fuel;
    public int crew;
    public int crewSick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void happinessInc(int amount) {
        happiness += amount;
    }

    public void healthInc(int amount)
    {
        health += amount;
    }

    public void foodInc(int amount)
    {
        food += amount;
    }

    public void fuelInc(int amount)
    {
        fuel += amount;
    }

    public void crewInc(int amount)
    {
        crew += amount;
    }

    public void crewSickInc(int amount)
    {
        crewSick += amount;
    }
}
