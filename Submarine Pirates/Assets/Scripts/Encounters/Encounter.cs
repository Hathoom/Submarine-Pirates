using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter : MonoBehaviour
{
    public TextboxTrigger textboxTrigger = GameObject.Find("TextboxTrigger").GetComponent<TextboxTrigger>();
    public TextboxManager textboxManager = GameObject.Find("TextboxManager").GetComponent<TextboxManager>();
    public GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    public string encounterName;

    public virtual void executeEncounter() {
        
    }

    public virtual void startEncounter() {

    }

    public virtual void endEncounter() {
        
    }
}
