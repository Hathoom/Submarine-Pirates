using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


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
    public int maxCrew;          // The max crew you can have on your ship
    public int usableCrew;       // The number of crew you can send out
    public int crew;
    public int maxSick;
    public int crewSick;
    public int crewDiedThisTurn;
    public int damage;
    public int depth;
    public int depthDir; // 1 - down, 0 - no movement, -1 - up
    public int gold;

    public int level;

    public int govHuntTurns = 5;

    public bool hasReachedLevel2 = false;
    public bool hasReachedLevel3 = false;

    public bool emergencyLift = false;

    public int weaponPow;
    

    public Camera mainCamera;

    // UI
    public TextMeshProUGUI foodTxt;
    public TextMeshProUGUI damageTxt;
    public TextMeshProUGUI fuelTxt;
    public TextMeshProUGUI goldTxt;
    public TextMeshProUGUI depthTxt;
    public Slider happinessSlider;
    public Slider healthSlider;
    public TextMeshProUGUI crewCount;
    public TextMeshProUGUI sickCrewCount;
    public TextMeshProUGUI govTurnsLeft;

    // Textbox
    public TextboxTrigger textboxTrigger;
    public TextboxManager textboxManager;

    // Shop
    public ShopManager shopManager;

    // Encounters
    public EncounterManager encounterManager;

    // Crewmates assigned
    public Bridge bridge;
    public Galley galley;
    public Engine engine;
    public Medbay medbay;
    public Maintenance maintenance;
    public Weapons weapons;
    public Barracks barracks;

    private GameObject[] objectsOnField;

    public Background background;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SetupStartup();
    }

    // Update is called once per frame
    void Update()
    {
        foodTxt.text = food.ToString();
        damageTxt.text = damage.ToString();
        fuelTxt.text = fuel.ToString();
        goldTxt.text = "Gold: " + gold;
        depthTxt.text = "Depth: " + depth + "0";
        happinessSlider.value = happiness;
        healthSlider.value = health;
        crewCount.text = "x" + crew;
        sickCrewCount.text = "x" + crewSick;
        govTurnsLeft.text = "Turns Left: " + govHuntTurns;
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
        

        crew = usableCrew;
        crewSick = maxSick;
    }

    public void startTurn() {
        Debug.Log("New turn started");

        //at start of turn, figure out how much crew is needed to run the ship
        bridge.SetCrewNeeded();

        //reset the WeaponPow
        weaponPowInc(-weaponPow);
    }
    
    // When the user is ready to end their turn, this advances the game
    public void endTurn()
    {
        
        if(!bridge.CheckNeededCrew())
        {
            Debug.Log("You ended the turn without a proper bridge staff!");
        }

        gamestate = "work";

        // Kill existing sick crewmembers
        // this is here because rooms incrementing values can cause crewmembers to get sick
        // as well as events making crewmembers sick
        turnEndKillSick();

        // decriment happiness based on number of sick crewmembers remaining
        // done before any new crewmembers could become sick
        happinessInc(-maxSick);

        // CREWMATES DO WORK

        barracks.endTurn();
        bridge.endTurn();
        engine.endTurn();
        galley.endTurn();
        maintenance.endTurn();
        medbay.endTurn();
        weapons.endTurn();

        Debug.Log(weaponPow);

        crewReset();
        
        // Crewmates don't have enough to eat
        if (food >= usableCrew + maxSick) {
            textboxTrigger.loadTxtFile("food_pass");
        } else {
            textboxTrigger.loadTxtFile("food_fail");
        }
        // Crew eats food
        foodInc(-usableCrew - maxSick);
        // Decreases health based on damage
        healthInc(-damage);

        //Check for starving
        starveCheck();

        // inform user how much crew they lost and reset lost value
        Debug.Log("You lost: " + crewDiedThisTurn + " CrewMembers!");
        crewDiedThisTurn = 0;

        //decriment the number of turns you have to run from government
        govHuntTurnsInc(-1);

        //check if the level needs to be changed
        setLevel();

        // Generate encounter
        textboxManager.setPostFunction(encounterManager.generateEncounter);

        // Encounter a death state if one of the death states is met
        // DEATH STATES

        if (happiness <= 0)
        {
            Debug.Log("Crew Mutinies! Game over.");
            textboxTrigger.loadTxtFile("death_mutiny");
            textboxManager.setPostFunction(youLose);
        }

        if (health <= 0)
        {
            Debug.Log("Ship is destroyed");
            textboxTrigger.loadTxtFile("death_health");
            textboxManager.setPostFunction(youLose);
        }

        if (crew <= 0)
        {
            Debug.Log("All your crew is dead, you are stuck in the ocean");
            textboxTrigger.loadTxtFile("death_crew_gone");
            textboxManager.setPostFunction(youLose);
        }

        if (fuel <= 0) {
            Debug.Log("You have no more fuel and can't move game over.");
            textboxTrigger.loadTxtFile("death_fuel");
            textboxManager.setPostFunction(youLose);
        }

        if (govHuntTurns <= 0)
        {
            Debug.Log("The Government has found you");
            textboxTrigger.loadTxtFile("death_gov");
            textboxManager.setPostFunction(youLose);
        }

        // Activate a normal encounter
        textboxTrigger.triggerTextbox();
    }

    // Decides what the encounter is
    public void nextEncounter() 
    {
        gamestate = "encounter";
    }

    public void youLose()
    {
        SceneManager.LoadScene("EndScene");
    }

    //check for starving
    public void starveCheck()
    {
        // lose happiness
        if (food < 0)
        {
            happinessInc(-5);
        }
        if(food <= -10)
        {
            //make a crewmember sick
            if (usableCrew > 1)
            {
                usableCrewInc(-1);
                maxSickInc(+1);
            }
        }
        if(food <= -20)
        {
            //make a sick crewmember or regular crewmember dead.
            if (maxSick > 1)
            {
                killCrewMember(1);
            }
            else
            {
                killCrewMember(0);
            }
            food = -10;
        }
    }

    //turn end Kill Sick Crew
    public void turnEndKillSick()
    {
        int unsafeSickCrew = maxSick - medbay.GetSickCrew();
        if (unsafeSickCrew > 0)
        {
            for (int i = 0; i < unsafeSickCrew; i++)
            {
                if(Random.Range(1, 101) <= 25)
                {
                    killCrewMember(1);
                }
            }
        }
    }

    // kill a crew member
    public void killCrewMember(int type)
    {
        // if type == 0 healthy crewmember
        // if type == 1 sick crewmember

        if (type == 0)
        {
            usableCrewInc(-1);
            happinessInc(-10);
        }
        else if (type == 1)
        {
            maxSickInc(-1);
            happinessInc(-5);
        }
        crewDiedThisTurnInc(1);
    }



    // incrimenters
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

    public void usableCrewInc(int amount)
    {
        usableCrew += amount;
        if (usableCrew < 0) usableCrew = 0;
        if (usableCrew > maxCrew) usableCrew = maxCrew;
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

    public void crewDiedThisTurnInc(int amount)
    {
        crewDiedThisTurn += amount;
        if (crewDiedThisTurn < 0) crewDiedThisTurn = 0;
    }

    public void damageInc(int amount)
    {
        damage += amount;
        if (damage < 0) damage = 0;
    }

    public void depthInc(int amount)
    {
        //alter the depth
        amount = amount * depthDir;

        //increase depth movement if emergencyLift is active
        if (emergencyLift)
        {
            amount = amount * 2;
        }
    
        depth += amount;

        //if you go fast crew may get sick
        if (usableCrew > 0)
        {
            if (amount >= 40 && amount < 60 || amount > -60 && amount <=-40)
            {
                if (Random.Range(1, 101) <= 33)
                {
                    if (usableCrew > 0)
                    {
                        usableCrewInc(-1);
                        maxSickInc(+1);
                    }
                }
            }
            else if (amount >= 60 && amount < 80 || amount > -80 && amount <=-60)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Random.Range(1, 101) <= 66)
                    {
                        if (usableCrew > 0)
                        {
                            usableCrewInc(-1);
                            maxSickInc(+1);
                        }
                    }
                }
            }
        }
    }

    public void goldInc(int amount)
    {
        gold += amount;
        if (gold < 0) gold = 0;
    }

    public void govHuntTurnsInc(int amount)
    {
        govHuntTurns = govHuntTurns + amount;
        if (govHuntTurns < 0) govHuntTurns = 0;
    }

    public void weaponPowInc(int amount)
    {
        weaponPow += amount;
        if (weaponPow < 0) weaponPow = 0;
    }

    public void setDepthDir(int setDepthDir) {
        depthDir = setDepthDir;

        //turn off emergency lift if the player decides they want to go further down.
        if (depthDir == 1 && emergencyLift)
        {
            setEmergencyLift(false);
        }
    }

    public void setEmergencyLift(bool value)
    {
        emergencyLift = value;

        // if you activate the emergency lift you should always go up
        if (emergencyLift)
        {
            setDepthDir(-1);
        }
    }

    public void setLevel()
    {
        //if you are at a new level, then change the level you are at
        if (depth <= 0) level = 0;
        else if (depth > 0 && depth < 100) level = 1;
        else if (depth >= 100 && depth <= 199)
        {
            level = 2;
            if(!hasReachedLevel2)
            {
                hasReachedLevel2 = true;
                govHuntTurns = 10;
            }
        }
        else if (depth >= 200 && depth <= 299)
        {
            level = 3;
            if(!hasReachedLevel3)
            {
                hasReachedLevel3 = true;
                govHuntTurns = 15;
            }
        }
        else if (depth >= 300)
        {
            level = 4;
            // counter the above reduction, and increase # of free turns by 1
            govHuntTurnsInc(2);
        }

        background.SetBackground(level +1);
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

    public void SetupStartup()
    {
        happiness = 50;
        health = 100;
        food = 15;
        fuel = 30;
        maxCrew = 10;
        usableCrew = 10;
        crew = usableCrew;
        maxSick = 0;
        crewSick = maxSick;
        damage = 0;
        depth = 0;
        depthDir = 0;
        gold = 1000;
        level = 0;
        govHuntTurns = 6;
        hasReachedLevel2 = false;
        hasReachedLevel3 = false;
        emergencyLift = false;
        weaponPow = 0;
    }
}
