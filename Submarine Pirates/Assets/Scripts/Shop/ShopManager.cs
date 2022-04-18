using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject surfaceShop;
    public GameManager gameManager;
    public Canvas shopCanvas;
    public Transform ShopSpawnTransform;
    public EncounterManager encounterManager;

    // Start is called before the first frame update
    void Start()
    {
        toggleShopUI(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleShopUI(bool toggle) {
        shopCanvas.enabled = toggle;
    }

    public void startSurfaceShop() {
        toggleShopUI(true);
        Instantiate(surfaceShop, transform);
    }

    public void endShop() {
        Destroy(GameObject.FindWithTag("Shop"));
        toggleShopUI(false);
        encounterManager.encounter.endEncounter();
    }
}
