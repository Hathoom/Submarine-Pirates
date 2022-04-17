using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{

    private GameManager gameManager;
    public int primaryCost;
    public int secondaryCost;

    public int gain;

    //these will be 1-4 to determine what is the secondary costs
    // 0 will represent no secondary cost
    public int whatisSecondaryCost;

    public bool useGold;
    public bool useFood;
    public bool useCrew;
    public bool useHappiness;

    public bool buyGold;
    public bool buyFood;
    public bool buyCrew;
    public bool buyHappiness;

    // Start is called before the first frame update
    void Start()
    {
        //get the GameManager on creation
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnButtonClick()
    {
        
        // reduct from the gameManager the primary cost
        if(useGold)
        {
            //Gold not implemented on Gamemanager yet
        }
        else if(useFood)
        {
            gameManager.foodInc(-primaryCost);
        }
        else if(useCrew)
        {
            gameManager.crewInc(-primaryCost);
        }
        else if(useHappiness)
        {
            gameManager.happinessInc(-primaryCost);
        }

        // reduct any secondary cost
        if (whatisSecondaryCost != 0)
        {
            if(whatisSecondaryCost == 1)
            {
                // Gold is not implemented yet
            }   
            else if (whatisSecondaryCost == 2)
            {
                gameManager.foodInc(-secondaryCost);
            }
            else if (whatisSecondaryCost == 3)
            {
                gameManager.crewInc(-secondaryCost);
            }
            else if (whatisSecondaryCost == 4)
            {
                gameManager.happinessInc(-primaryCost);
            }
        }

        // increase any gains
        if (buyGold)
        {
            // implement later
        }
        else if (buyFood)
        {
            gameManager.foodInc(gain);
        }
        else if (buyCrew)
        {
            gameManager.crewInc(gain);
        }
        else if (buyHappiness)
        {
            gameManager.happinessInc(gain);
        }
    }

}
