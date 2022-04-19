using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TextboxManager : MonoBehaviour
{
    public delegate void TestDelegate();

    private Queue<string> lines;
    public bool displayingText;
    public TextMeshProUGUI textboxTxt;
    public Image textboxBG;
    public Button continueBtn;
    public Canvas canvas;
    public TestDelegate postFunction;

    /// POST FUNCTION DOCUMENTATION
    /// Whenever you call a textbox, pass in the function that you want it to execute 
    /// ex. TextboxManager.postFunction = nextEncounter;

    // Start is called before the first frame update
    void Start()
    {
        lines = new Queue<string>();
        setTextboxToggle(false);
        postFunction = test;
    }

    void Update() {

    }

    public void startTextbox(TextboxScript script)
    {

        Debug.Log("Running script " + script.name);
        setTextboxToggle(true);

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
        lines.Clear();
        setTextboxToggle(false);
        Debug.Log("End of text.");
        postFunction();
    }

    public void setTextboxToggle(bool toggle) {
        canvas.enabled = toggle;
        displayingText = toggle;
    }

    public void setPostFunction(TestDelegate postFunction) {
        this.postFunction = postFunction;
    }

    public void test() {
        Debug.Log("Post Function works!");
    }
}
