using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterShop : Encounter
{
    public int shopLevel;

    public EncounterShop(string encounterName, int shopLevel) {
        // 0 - Surface Merchant
        // 1 - Fishing Vessal
        // 2 - ??
        // 3 - Scrappers
        // 4 - Davey Jones
        this.encounterName = encounterName;
        this.shopLevel = shopLevel;
    }

    public override void startEncounter() {
        Debug.Log("Defend encounter started");
        textboxManager.setPostFunction(executeEncounter);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/start");
        textboxTrigger.triggerTextbox();
    }

    public override void executeEncounter() {
        ShopManager shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();

        switch (shopLevel) {
            case 0:
                shopManager.startSurfaceShop();
                break;
        }
    }

    public override void endEncounter() {
        textboxManager.setPostFunction(gameManager.startTurn);
        textboxTrigger.loadTxtFile("Encounter/" + encounterName + "/end");
        textboxTrigger.triggerTextbox();
    }
}
