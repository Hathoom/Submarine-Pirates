using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShallowsShop : Shop
{
    public int fuelCost;
    public int fuelAmount;
    public int foodCost;
    public int foodAmount;
    public int rumCost;
    public int rumAmount;

    public TextMeshProUGUI fuelTxt;
    public TextMeshProUGUI foodTxt;
    public TextMeshProUGUI rumTxt;

    void Update() {
        fuelTxt.text = "Cost: " + fuelCost + " - Amount: " + fuelAmount;
        foodTxt.text = "Cost: " + foodCost + " - Amount: " + foodAmount;
        rumTxt.text = "Cost: " + rumCost + " - Amount: " + rumAmount;
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

    public void buyRum() {
        if (gameManager.gold >= rumCost) {
            gameManager.goldInc(-rumCost);
            gameManager.happinessInc(rumAmount * 5);
        }
    }
}
