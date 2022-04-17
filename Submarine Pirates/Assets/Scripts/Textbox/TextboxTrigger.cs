using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextboxTrigger : MonoBehaviour
{
    public TextboxScript script;

    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            triggerTextbox();
        }
    }

    public void triggerTextbox()
    {
        FindObjectOfType<TextboxManager>().startTextbox(script);
    }
}
