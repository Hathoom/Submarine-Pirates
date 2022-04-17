using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterShop : Encounter
{
    public int shopLevel;

    public EncounterShop(int shopLevel) {
        // 0 - Surface Merchant
        // 1 - Fishing Vessal
        // 2 - ??
        // 3 - Scrappers
        // 4 - Davey Jones
        this.shopLevel = shopLevel;
    }

    public override void executeEncounter() {
        ShopManager shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();

        switch (shopLevel) {
            case 0:
                shopManager.startSurfaceShop();
                break;
        }
    }
}
