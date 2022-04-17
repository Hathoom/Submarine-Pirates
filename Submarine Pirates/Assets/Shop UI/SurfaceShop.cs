using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurfaceShop : Shop
{
    public int fuelCost;
    public int fuelAmount;
    public int foodCost;
    public int foodAmount;
    public int healthCost;
    public int healthAmount;
    public int crewCost;
    public int crewAmount;
    public int barracksCost;
    public int barracksAmount;

    public TextMeshProUGUI fuelTxt;
    public TextMeshProUGUI foodTxt;
    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI crewTxt;
    public TextMeshProUGUI barracksTxt;

    void Update() {
        fuelTxt.text = "Cost: " + fuelCost + " - Amount: " + fuelAmount;
        foodTxt.text = "Cost: " + foodCost + " - Amount: " + foodAmount;
        healthTxt.text = "Cost: " + healthCost + " - Amount: " + healthAmount;
        crewTxt.text = "Cost: " + crewCost + " - Amount: " + crewAmount;
        barracksTxt.text = "Cost: " + barracksCost + " - Amount: " + barracksAmount;
    }

    public void buyFuel() {
        if (gameManager.gold >= fuelCost) {
            gameManager.goldInc(-fuelCost);
            gameManager.fuelInc(fuelAmount);
        }
    }

    public void buyFood() {
        if (gameManager.gold >= foodCost) {
            gameManager.goldInc(-foodCost);
            gameManager.foodInc(foodAmount);
        }
    }

    public void buyHealth() {
        if (gameManager.gold >= healthCost) {
            gameManager.goldInc(-healthCost);
            gameManager.healthInc(healthAmount);
        }
    }

    public void buyCrew() {
        if (gameManager.gold >= crewCost && gameManager.usableCrew + 1 <= gameManager.maxCrew) {
            gameManager.goldInc(-crewCost);
            gameManager.usableCrewInc(crewAmount);
        }
    }

    public void upgradeBarracks() {
        if (gameManager.gold >= barracksCost) {
            gameManager.goldInc(-barracksCost);
            gameManager.maxCrewInc(2);
        }
    }
}
