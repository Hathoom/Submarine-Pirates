using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TextboxManager : MonoBehaviour
{
    private Queue<string> lines;
    public TextMeshProUGUI textboxTxt;
    public Button continueBtn;

    // Start is called before the first frame update
    void Start()
    {
        lines = new Queue<string>();
    }

    void Update() {
        
    }

    public void startTextbox(TextboxScript script)
    {
        Debug.Log("Running script " + script.name);

        lines.Clear();

        foreach (string line in script.lines)
        {
            lines.Enqueue(line);
        }

        displayNextLine();
    }

    public void displayNextLine()
    { 
        if (lines.Count == 0)
        {
            endTextbox();
            return;
        }

        string line = lines.Dequeue();
        textboxTxt.text = line;
    }

    public void endTextbox()
    {
        Debug.Log("End of text.");
    }
}
