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
        //fname = "Assets/Textbox Files/" + fname + ".txt";
        TextAsset f = Resources.Load<TextAsset>(fname);

        //if (!System.IO.File.Exists(fname))
        //    Debug.Log("File " + fname + " does not exist.");

        //StreamReader reader = new StreamReader(fname);

        string[] str_list = f.text.Split(char.Parse("\n"));

        for (int i = 0; i < str_list.Length; i++) {
            script.lines.Add(str_list[i]);
        }
        
        // Reads in the lines for the file
        //while ((line = reader.ReadLine()) != null) {
        //    script.lines.Add(line);
        //}
    }

    // Used if you have a txt file you've already loaded and you want to add onto it with another
    public void loadTxtFileAdd(string fname) {
        string line;
        TextAsset f = Resources.Load<TextAsset>(fname);

        string[] str_list = f.text.Split(char.Parse("\n"));

        for (int i = 0; i < str_list.Length; i++) {
            script.lines.Add(str_list[i]);
        }
    }
}
