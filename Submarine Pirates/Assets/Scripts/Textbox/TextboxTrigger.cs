using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextboxTrigger : MonoBehaviour
{
    public TextboxScript script;
    public delegate void TestDelegate();
    public TextboxManager textboxManager;

    public void triggerTextbox()
    {
        textboxManager.startTextbox(script);
    }

    public void loadTxtFile(string fname) {
        Debug.Log("Running file " + fname);

        string line;
        script = new TextboxScript();
        fname = "Assets/Textbox Files/" + fname + ".txt";

        if (!System.IO.File.Exists(fname))
            Debug.Log("File " + fname + " does not exist.");

        StreamReader reader = new StreamReader(fname);
        
        // Reads in the lines for the file
        while ((line = reader.ReadLine()) != null) {
            script.lines.Add(line);
        }
    }

    // Used if you have a txt file you've already loaded and you want to add onto it with another
    public void loadTxtFileAdd(string fname) {
        string line;
        fname = "Assets/Textbox Files/" + fname + ".txt";
        StreamReader reader = new StreamReader(fname);
        
        // Reads in the lines for the file
        while ((line = reader.ReadLine()) != null) {
            script.lines.Add(line);
        }
    }
}
